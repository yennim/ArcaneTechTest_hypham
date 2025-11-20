using System;
using System.IO;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private const string SAVE_PATH = "savedProjects.json";
    private const string NEW_PROJECT_NAME = "New Project";

    private Project currentProject;
    private ProjectsList projects;

    [SerializeField] private UIProjects uiProjects;
    [SerializeField] private Transform modelsContainer;

    // I decided to only consider one project at a time, no multiple tabs to edit projects. Makes it easier to find the instance.
    public static ProjectManager Instance;

    public event Action<ModelController> OnModelSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
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

        currentProject = new Project(NEW_PROJECT_NAME);

        Debug.Log(projects.list.Count);
        uiProjects.Initialize(projects, currentProject);
    }

    // Called on EditableText.nputField OnClick. TODO: improve not referencing directly in editor?
    public void EditProjectName(string name)
    {
        currentProject.Name = name;
    }

    public void SaveProject()
    {
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
        uiProjects.RefreshCurrentProjectName(currentProject.Name);

        // TODO: instantiate each model
    }

    public void AddModel(ModelTypeStruct modelType)
    {
        Instantiate(modelType.Prefab, modelsContainer);
    }

    public void SetSelectedModel(ModelController modelController)
    {
        OnModelSelected?.Invoke(modelController);
    }
}
