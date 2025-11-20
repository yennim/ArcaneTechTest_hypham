using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Model
{
    [field: SerializeField]
    private string id;
    public string Id => id;

    // TODO: secure accesses if necessary
    public EModelType Type;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public List<SerializableColor> IndexedColors;
}