using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelTypeData", menuName = "ScriptableObjects/ModelTypeScriptableObject", order = 1)]
public class ModelTypeScriptableObject : ScriptableObject
{
    // TODO come back to secure access if needed
    public List<ModelTypeStruct> modelTypes = new List<ModelTypeStruct>();
}

[Serializable]
public struct ModelTypeStruct
{
    public EModelType ModelType;
    public GameObject Prefab;
}
