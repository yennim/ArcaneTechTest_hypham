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
    public Model(EModelType type)
    {
        id = Guid.NewGuid().ToString();
        Type = type;
    }

    public Model (string id, EModelType type, Vector3 position, Quaternion rotation, Vector3 scale, List<SerializableColor> indexedColors)
    {
        this.id = id;
        Type = type;
        Position = position;
        Rotation = rotation;
        Scale = scale;
        IndexedColors = indexedColors;
    }
}