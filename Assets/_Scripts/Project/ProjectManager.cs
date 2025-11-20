using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private Project currentProject;
    private List<Project> projects;

    [SerializeField] private Transform room;
    [SerializeField] private List<string> projectNames;

    // I decided to only consider one project at a time, no multiple tabs to edit projects. Makes it easier to find the instance.
    public static ProjectManager Instance;

    public event Action<ModelController> OnModelSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        currentProject = new Project(name);
    }

    public void SaveProject()
    {

    }
    
    public void LoadProject(string id)
    {

    }

    public void AddModel(ModelTypeStruct modelType)
    {
        Instantiate(modelType.Prefab, room);
    }

    public void SetSelectedModel(ModelController modelController)
    {
        OnModelSelected?.Invoke(modelController);
    }
}
