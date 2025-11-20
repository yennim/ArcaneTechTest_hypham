using UnityEngine;

public class ModelController : MonoBehaviour
{
    // TODO: secure access ?
    public Model model;

    private bool isSelected;

    private void OnEnable()
    {
        ProjectManager.Instance.OnModelSelected += RefreshSelectedState;
    }

    private void OnDisable()
    {
        ProjectManager.Instance.OnModelSelected -= RefreshSelectedState;
    }

    private void RefreshSelectedState(ModelController modelController)
    {
        isSelected = (modelController == this);
    }

    public void Select(ModelController modelController)
    {
        ProjectManager.Instance.SetSelectedModel(this);
    }
}
