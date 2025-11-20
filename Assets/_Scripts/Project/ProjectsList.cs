using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectsList
{
    [field: SerializeField] // Could be used to have a list of projects per user or per category eventually
    private string id;
    public string Id => id;

    // TODO: come back for accesses
    public List<Project> list;

    public ProjectsList()
    {
        id = Guid.NewGuid().ToString();
        list = new List<Project>();
    }

    public ProjectsList(string id, List<Project> list)
    {
        this.id = id;
        this.list = list;
    }
}
