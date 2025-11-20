using TMPro;
using UnityEngine;

public class ButtonProject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Project Project { get; private set; }
    
    public void InitializeButton(Project project)
    {
        Project = project;
        text.text = project.Name;
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void OnProjectClick()
    {
        ProjectManager.Instance.LoadProjectById(Project.Id);
    }
}
