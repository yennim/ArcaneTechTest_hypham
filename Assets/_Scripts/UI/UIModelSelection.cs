using System.Collections.Generic;
using UnityEngine;

public class UIModelSelection : MonoBehaviour
{
    [SerializeField] private ButtonModel buttonPrefab;

    [SerializeField] private ModelTypeScriptableObject modelsData;

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

        if (modelsData != null)
        {
            foreach (ModelTypeStruct model in modelsData.modelTypes)
            {
                ButtonModel button = Instantiate(buttonPrefab, container);
                button.InitializeButton(model);
            }
        }
    }
}
