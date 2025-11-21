using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectManager : MonoBehaviour
{
    private const string SAVE_PATH = "/savedProjects.json";
    private const string NEW_PROJECT_NAME = "New Project";

    private Camera mainCamera;
    private Project currentProject;
    private ProjectsList projects;
    private List<ModelController> activeModelControllers;

    [SerializeField] private LayerMask modelLayer;
    [SerializeField] private UIProjects uiProjects;
    [SerializeField] private Transform modelsContainer;

    [SerializeField] private ModelTypeScriptableObject modelsData;
    public ModelTypeScriptableObject ModelsData { get { return modelsData; } }

    public ModelController SelectedModel { get; private set; }

    // I decided to only consider one project at a time, no multiple tabs to edit projects. Makes it easier to find the instance.
    private static ProjectManager instance;
    public static ProjectManager Instance {
        get
        {
            if (instance == null)
            {
                instance = (ProjectManager) FindObjectsByType(typeof(ProjectManager), FindObjectsSortMode.None).FirstOrDefault();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public event Action OnChangeProject;
    public event Action<ModelController> OnModelSelected;
    public event Action<int> OnModelFaceIndexSelected;

    # region LIFECYCLE
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag a camera as 'MainCamera'.");
            enabled = false;
        }

        Debug.Log(Application.persistentDataPath + SAVE_PATH);
        string path = Application.persistentDataPath + SAVE_PATH;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            try
            {
                projects = JsonUtility.FromJson<ProjectsList>(json);
            }
            catch (Exception e) 
            {
                Debug.LogError("Something went wrong when converting the saved json file. " + e.Message);
                projects = new ProjectsList();
            }
        }
        else
        {
            projects = new ProjectsList();
        }

        StartNewProject();
    }

    private void Update()
    {
        HandleClickRaycast();
    }
    #endregion

    #region SAVE & LOAD
    public void StartNewProject()
    {
        currentProject = new Project(NEW_PROJECT_NAME);
        activeModelControllers = new List<ModelController>();
        ClearContainer(modelsContainer);

        uiProjects.Initialize(projects, currentProject);

        OnChangeProject?.Invoke();
    }
    public void SaveProject()
    {
        foreach (ModelController controller in activeModelControllers)
        {
            controller.RefreshModel();
        }

        if (projects == null || projects.list == null)
        {
            Debug.LogError("Projects or Projects.list is null");
        }
        else
        {
            int projectIndex = projects.list.FindIndex(p => p.Id == currentProject.Id);
            if (projectIndex < 0)
            {
                projects.list.Add(currentProject);
                uiProjects.AddProject(currentProject);
            }
            else
            {
                projects.list[projectIndex] = currentProject;
            }

            string savedJson = JsonUtility.ToJson(projects);
            Debug.Log(savedJson);
            File.WriteAllText(Application.persistentDataPath + SAVE_PATH, savedJson);
        }
    }

    public void LoadProjectById(string id)
    {
        currentProject = projects.list.Find(p => p.Id == id);
        uiProjects.EditCurrentProjectName(currentProject.Name);
        activeModelControllers = new List<ModelController>();

        ClearContainer(modelsContainer);

        foreach (Model model in currentProject.Models)
        {
            if (model != null)
            {
                LoadModel(model);
            }
        }

        OnChangeProject?.Invoke();
    }

    public void LoadModel(Model model)
    {
        int modelTypeIndex = modelsData.modelTypes.FindIndex(mt => mt.ModelType == model.Type);
        if (modelTypeIndex < 0)
        {
            Debug.LogError($"Stored model type {model.Type} is not found in Scriptable Object");
        }
        else if (modelsData.modelTypes[modelTypeIndex].Prefab == null)
        {
            Debug.LogError($"Prefab for {model.Type} is missing.");
            return;
        }
        else
        {
            ModelController prefab = modelsData.modelTypes[modelTypeIndex].Prefab;

            // TODO : handle edge case to add the component if not found
            ModelController modelController = Instantiate(prefab, modelsContainer);
            modelController.Load(model);

            modelController.Initialize(model);

            activeModelControllers.Add(modelController);
        }
    }

    private void ClearContainer(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            GameObject child = container.GetChild(i).gameObject;
            Destroy(child);
        }
    }
    #endregion

    #region PROJECT EDITING
    public void EditProjectName(string name)
    {
        currentProject.Name = name;
    }

    public void AddModel(ModelTypeStruct modelType)
    {
        ModelController modelController = Instantiate(modelType.Prefab, modelsContainer);
        Model model = new Model(modelType.ModelType);

        modelController.Initialize(model);
        currentProject.Models.Add(model);

        activeModelControllers.Add(modelController);
    }

    public void AddModel(ModelTypeStruct modelType, Vector3 position)
    {
        ModelController modelController = Instantiate(modelType.Prefab, position, Quaternion.identity, modelsContainer);
        modelController.transform.localRotation = Quaternion.identity; // because I set up the room with a rotation at first

        Model model = new Model(modelType.ModelType);

        modelController.Initialize(model);
        currentProject.Models.Add(model);

        activeModelControllers.Add(modelController);
    }

    public void SetSelectedModel(ModelController modelController)
    {
        SelectedModel = modelController;
        OnModelSelected?.Invoke(modelController);
    }

    private void HandleClickRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, modelLayer))
                {
                    Debug.Log($"Selected gameobject : {hit.collider.gameObject.name}");

                    HandleSelectModelOrFace(hit);
                }
                else
                {
                    SetSelectedModel(null);
                }
            }
        }
    }

    private void HandleSelectModelOrFace(RaycastHit hit)
    {
        ModelController selectedModel = hit.collider.gameObject.GetComponentInParent<ModelController>();

        if (selectedModel)
        {
            if (selectedModel == SelectedModel)
            {
                HandleSelectFace(hit);
            }
            else
            {
                SetSelectedModel(selectedModel);
            }
        }
    }

    private void HandleSelectFace(RaycastHit hit)
    {
        if (hit.collider is MeshCollider meshCollider && !meshCollider.convex)
        {
            Debug.Log($"collider is mesh and not convex");
            int index = hit.triangleIndex;
            if (index < 0)
            {
                Debug.Log("triagle index not found");
            }
            else
            {
                Debug.Log($"index: {index}");
                SelectedModel.SetSelectedFaceIndex(index);
                OnModelFaceIndexSelected?.Invoke(index);
            }
        }
    }
    #endregion

}
