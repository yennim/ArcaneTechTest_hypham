using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static TreeEditor.TreeEditorHelper;
using UnityEngine.UIElements;

public class ButtonDragAndDropModel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LayerMask droppableLayerMasks;

    private Camera mainCamera;
    private GameObject modelPreview;

    public ModelTypeStruct Model { get; private set; }
    
    public void InitializeButton(ModelTypeStruct modelTypeStruct)
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found, tag one as the main camera.");
        }

        Model = modelTypeStruct;
        text.text = modelTypeStruct.Prefab.gameObject.name;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, droppableLayerMasks))
        {
            ProjectManager.Instance.AddModel(Model, hit.point);
        }
    }
}
