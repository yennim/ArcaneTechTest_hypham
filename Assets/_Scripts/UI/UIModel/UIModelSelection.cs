using System.Collections.Generic;
using UnityEngine;

public class UIModelSelection : MonoBehaviour
{
    [SerializeField] private ButtonDragAndDropModel buttonPrefab;

    [SerializeField] private Transform container;

    private void Start()
    {
        InstantiateButtonModels();
    }

    private void ClearContainer()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            GameObject child = container.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    public void InstantiateButtonModels()
    {
        ClearContainer();

        if (ProjectManager.Instance != null && ProjectManager.Instance.ModelsData != null)
        {
            foreach (ModelTypeStruct model in ProjectManager.Instance.ModelsData.modelTypes)
            {
                ButtonDragAndDropModel button = Instantiate(buttonPrefab, container);
                button.InitializeButton(model);
            }
        }
        else
        {
            Debug.LogError("No ProjectManager gameobject or no ModelsData on launch");
        }
    }
}
