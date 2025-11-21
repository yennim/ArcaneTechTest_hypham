using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDragAndDropModel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LayerMask droppableLayerMasks;
    [SerializeField] private float previewFloatingDistance = 11f;

    private Camera mainCamera;
    private ModelController modelPreview;

    public ModelTypeStruct Model { get; private set; }
    
    public void InitializeButton(ModelTypeStruct modelTypeStruct)
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found, tag one as the main camera.");
        }

        Model = modelTypeStruct;
        text.text = modelTypeStruct.name;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, droppableLayerMasks))
        {
            InstantiateOrReactivate(hit.point);
        }
        else
        {
            InstantiateOrReactivate(ray.GetPoint(previewFloatingDistance));
        }
    }

    private void InstantiateOrReactivate(Vector3 position)
    {
        if (modelPreview)
        {
            modelPreview.gameObject.SetActive(true);
            modelPreview.transform.position = position;
        }
        else
        {
            modelPreview = Instantiate(Model.Prefab, position, new Quaternion(0, 0.382683426f, 0, 0.923879564f)); // ugly but copied from inspector        
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (modelPreview)
        {
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, droppableLayerMasks))
            {
                modelPreview.transform.position = hit.point;
            }
            else
            {
                modelPreview.transform.position = ray.GetPoint(previewFloatingDistance);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, droppableLayerMasks))
        {
            if (!modelPreview.IsOverlapping())
            {
                ProjectManager.Instance.AddModel(Model, hit.point);
            }
            else
            {
                Debug.LogError("overlapping ! Cancelling the object.");
            }
        }

        modelPreview.gameObject.SetActive(false);
    }
}
