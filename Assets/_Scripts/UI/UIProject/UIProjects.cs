using System.Collections.Generic;
using UnityEngine;

public class UIProjects : MonoBehaviour
{
    [SerializeField] private ButtonProject buttonPrefab;
    [SerializeField] private EditableText editableText;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject projectsMenu;

    private List<ButtonProject> projectsButtons;

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
        projectsButtons = new List<ButtonProject>();
        InstantiateProjectsList(projects);
        editableText.SetText(currentProject.Name);
    }

    public void InstantiateProjectsList(ProjectsList projects)
    {
        ClearContainer();

        if (projects != null && projects.list != null)
        {
            foreach (Project project in projects.list)
            {
                ButtonProject button = Instantiate(buttonPrefab, container);
                button.InitializeButton(project);
                projectsButtons.Add(button);
            }
        }
    }
    
    public void EditCurrentProjectName(string name)
    {
        ProjectManager.Instance.EditProjectName(name);

        editableText.SetText(name);

        int index = projectsButtons.FindIndex(b => b.Project.Name == name);
        if (index > -1) {
            ButtonProject button = projectsButtons[index];
            button.RefreshText();
        }
    }

    public void AddProject(Project project)
    {
        ButtonProject button = Instantiate(buttonPrefab, container);
        button.InitializeButton(project);
    }

    public void StartNewProject()
    {
        ProjectManager.Instance.StartNewProject();
        projectsMenu.SetActive(false);
    }

    public void Save()
    {
        ProjectManager.Instance.SaveProject();
        projectsMenu.SetActive(false);
    }

    public void OpenProjects()
    {
        if (projectsMenu.activeSelf)
        {
            projectsMenu.SetActive(false);
        }
        else
        {
            projectsMenu.SetActive(true);
        }
    }
}
