using TMPro;
using UnityEngine;

public class ButtonModel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public ModelTypeStruct Model { get; private set; }
    
    public void InitializeButton(ModelTypeStruct modelTypeStruct)
    {
        Model = modelTypeStruct;
        text.text = modelTypeStruct.Prefab.gameObject.name;
    }

    public void OnModelClick()
    {
        ProjectManager.Instance.AddModel(Model);
    }
}
