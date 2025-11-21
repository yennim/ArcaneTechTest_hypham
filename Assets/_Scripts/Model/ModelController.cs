using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private bool isSelected;
    private Renderer modelRenderer;
    private Vector3 initialScale;

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

    public void Initialize(Model model)
    {
        Model = model;
        modelRenderer = GetComponent<Renderer>();
    }

    public void Load(Model model)
    {
        modelRenderer = GetComponent<Renderer>();

        transform.localPosition = model.Position;
        transform.localRotation = model.Rotation;
        transform.localScale = model.Scale;

        for (int i = 0; i < model.IndexedColors.Count && i < modelRenderer.materials.Length; i++) {
            modelRenderer.materials[i].SetColor("_Color",  model.IndexedColors[i].ConvertToUnityColor());
        }
    }

    public void RefreshModel()
    {
        Model.Position = transform.localPosition;
        Model.Rotation = transform.localRotation;
        Model.Scale = transform.localScale;
        Model.IndexedColors = GetCurrentIndexedColors();
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

    #region EDIT
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
}
