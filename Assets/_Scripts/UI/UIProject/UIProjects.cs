using System.Collections.Generic;
using UnityEngine;

public class UIProjects : MonoBehaviour
{
    [SerializeField] private ButtonProject buttonPrefab;
    [SerializeField] private EditableText editableText;
    [SerializeField] private Transform container;

    private List<ButtonProject> projectsButtons;

    // TODO: Remove if no need to clear it but might use for now so leaving the ghost code
    private void ClearContainer()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            GameObject child = container.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    public void Initialize(ProjectsList projects, Project currentProject)
    {
        InstantiateProjectsList(projects);
        editableText.SetText(currentProject.Name);
    }

    public void InstantiateProjectsList(ProjectsList projects)
    {
        if (projects != null && projects.list != null)
        {
            foreach (Project project in projects.list)
            {
                ButtonProject button = Instantiate(buttonPrefab, container);
                button.InitializeButton(project);
            }
        }
    }
    
    public void RefreshCurrentProjectName(string name)
    {
        editableText.SetText(name);
    }

    public void AddProject(Project project)
    {
        ButtonProject button = Instantiate(buttonPrefab, container);
        button.InitializeButton(project);
    }
}
