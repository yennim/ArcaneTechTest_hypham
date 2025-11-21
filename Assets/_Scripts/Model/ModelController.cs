using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private bool isSelected;
    private Renderer modelRenderer;
    private Mesh mesh;
    private Vector3 initialScale;
    private int selectedTriangleIndex = -1;

    [SerializeField] private GameObject modelObject;
    [SerializeField] private UIEditModel uiEditModel;

    // TODO: secure access
    public Model Model { get; private set; }

    private void OnEnable()
    {
        ProjectManager.Instance.OnModelSelected += RefreshSelectedState;
        uiEditModel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (ProjectManager.Instance)
        {
            ProjectManager.Instance.OnModelSelected -= RefreshSelectedState;
        }
    }

    private void RefreshSelectedState(ModelController modelController)
    {
        isSelected = (modelController == this);
        uiEditModel.gameObject.SetActive(isSelected);
        initialScale = modelObject.transform.localScale;
    }

    public void SetSelectedFaceIndex(int faceIndex)
    {
        selectedTriangleIndex = faceIndex;
    }

    public void Initialize(Model model)
    {
        Model = model;
        modelRenderer = modelObject.GetComponent<Renderer>();
        MeshFilter meshFilter = modelObject.GetComponent<MeshFilter>();
        if (meshFilter)
        {
            mesh = meshFilter.mesh;
        }
    }

    public void Load(Model model)
    {
        modelRenderer = modelObject.GetComponent<Renderer>();
        
        transform.localPosition = model.Position;
        transform.localRotation = model.Rotation;
        transform.localScale = model.Scale;

        modelRenderer.material.color = model.Color.ConvertToUnityColor();

        /*
        MeshFilter meshFilter = modelObject.GetComponent<MeshFilter>();
        if (meshFilter)
        {
            mesh = meshFilter.mesh;
        }
        for (int i = 0; i < model.IndexedColors.Count && i < modelRenderer.materials.Length; i++) {
            modelRenderer.materials[i].SetColor("_Color",  model.IndexedColors[i].ConvertToUnityColor());
        }
        */
    }

    public void RefreshModel()
    {
        Model.Position = transform.localPosition;
        Model.Rotation = transform.localRotation;
        Model.Scale = transform.localScale;
        Model.IndexedColors = GetCurrentIndexedColors();
        Model.Color = new SerializableColor(modelRenderer.material.color);
    }

    public List<SerializableColor> GetCurrentIndexedColors()
    {
        // TODO: need to make sure it's not shared material ? 

        List<SerializableColor> indexedColors = new List<SerializableColor>();
        foreach (var material in modelRenderer.materials)
        {
            indexedColors.Add(new SerializableColor(material.GetColor("_Color")));
        }

        return indexedColors;
    }

    #region SCALE
    public void UpdateLocalScale(Vector3 newLocalScale)
    {
        modelObject.transform.localScale = newLocalScale;
    }

    public float GetScaleX()
    {
        return modelObject.transform.localScale.x;
    }
    public float GetScaleY()
    {
        return modelObject.transform.localScale.x;
    }
    public float GetScaleZ()
    {
        return modelObject.transform.localScale.x;
    }

    public void UpdateScaleOverall(float multiplier)
    {
        float newScaleY = initialScale.y * multiplier;

        modelObject.transform.localScale = initialScale * multiplier;
        modelObject.transform.localPosition = new Vector3(modelObject.transform.localPosition.x, newScaleY / 2f, modelObject.transform.localPosition.z);
    }

    public void UpdateScaleX(float newScaleX)
    {
        modelObject.transform.localScale = new Vector3(newScaleX, modelObject.transform.localScale.y, modelObject.transform.localScale.z);
        initialScale = modelObject.transform.localScale;
    }

    public void UpdateScaleY(float newScaleY)
    {
        modelObject.transform.localScale = new Vector3(modelObject.transform.localScale.x, newScaleY, modelObject.transform.localScale.z);
        modelObject.transform.localPosition = new Vector3(modelObject.transform.localPosition.x, newScaleY / 2f, modelObject.transform.localPosition.z);
        initialScale = modelObject.transform.localScale;
    }

    public void UpdateScaleZ(float newScaleZ)
    {
        modelObject.transform.localScale = new Vector3(modelObject.transform.localScale.x, modelObject.transform.localScale.y, newScaleZ);
        initialScale = modelObject.transform.localScale;
    }
    #endregion

    #region Colour
    public void SetMaterialColour(Color color)
    {
        modelRenderer.material.color = color;
    }

}
