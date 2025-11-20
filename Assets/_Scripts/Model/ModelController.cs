using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private bool isSelected;
    private Renderer modelRenderer;
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
        ProjectManager.Instance.OnModelSelected -= RefreshSelectedState;
    }

    private void RefreshSelectedState(ModelController modelController)
    {
        isSelected = (modelController == this);
        uiEditModel.gameObject.SetActive(isSelected);
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

    public void Select(ModelController modelController)
    {
        ProjectManager.Instance.SetSelectedModel(this);
    }
}
