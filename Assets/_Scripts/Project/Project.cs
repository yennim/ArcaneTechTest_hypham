using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Project
{
    [field: SerializeField]
    private string id;
    public string Id => id;

    [field: SerializeField]
    private string name;
    public string Name { 
        get { return name; } 
        set 
        {
            if (!string.IsNullOrEmpty(value))
            {
                name = value;
            }
            else 
            {
                name = "My Project";
            }
        }
    }

    // TODO: secure its access
    public List<Model> Models;

    public Project (string name)
    {
        id = Guid.NewGuid().ToString();
        Name = name;
        Models = new List<Model> ();
    }

    public Project (string id, string name, List<Model> models)
    {
        this.id = id;
        Name = name;
        Models = models;
    }


}
