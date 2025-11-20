using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private Project currentProject;
    private List<Project> projects;

    [SerializeField] private Transform modelsContainer;

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
        // load saved projects
        projects = new List<Project>();

        currentProject = new Project(name);
    }
    public void EditProjectName(string name)
    {
        currentProject.Name = name;
    }

    public void SaveProject()
    {
        projects.Add(currentProject);
    }
    
    public void LoadProject(string id)
    {
        currentProject = projects.Find(p => p.Id == id);
    }

    public void AddModel(ModelTypeStruct modelType)
    {
        Instantiate(modelType.Prefab, modelsContainer);
    }

    public void SetSelectedModel(ModelController modelController)
    {
        OnModelSelected?.Invoke(modelController);
    }
}
