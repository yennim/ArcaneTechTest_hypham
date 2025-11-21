using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEditModelOverlay : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Slider scaleOverall;
    [SerializeField] private TMP_InputField scale_x;
    [SerializeField] private TMP_InputField scale_y;
    [SerializeField] private TMP_InputField scale_z;

    private ModelController currentController;
   
    private void OnEnable()
    {
        ProjectManager.Instance.OnModelSelected += UpdateCurrentController;
        scaleOverall.onValueChanged.AddListener(UpdateModelScaleOverall);
        scale_x.onValueChanged.AddListener(UpdateModelScaleX);
        scale_y.onValueChanged.AddListener(UpdateModelScaleY);
        scale_z.onValueChanged.AddListener(UpdateModelScaleZ);
    }

    private void OnDisable()
    {
        if (ProjectManager.Instance)
        {
            ProjectManager.Instance.OnModelSelected -= UpdateCurrentController;
        }
        scaleOverall.onValueChanged.RemoveListener(UpdateModelScaleOverall);
        scale_x.onValueChanged.RemoveListener(UpdateModelScaleX);
        scale_y.onValueChanged.RemoveListener(UpdateModelScaleY);
        scale_z.onValueChanged.RemoveListener(UpdateModelScaleZ);
    }

    private void ActivateCanvasGroup()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    private void DeactivateCanvasGroup()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    private void UpdateCurrentController(ModelController modelController)
    {
        if (modelController != null)
        {
            currentController = modelController;
            scaleOverall.value = 1;
            scale_x.text = currentController.GetScaleX().ToString();
            scale_y.text = currentController.GetScaleY().ToString();
            scale_z.text = currentController.GetScaleZ().ToString();

            ActivateCanvasGroup();
        }
        else
        {
            DeactivateCanvasGroup();
        }
    }

    public void UpdateModelScaleOverall(float multiplier)
    {
        currentController.UpdateScaleOverall(multiplier);
    }

    public void UpdateModelScaleX(string scaleX) 
    {
        float parsedScale;
        if (float.TryParse(scaleX, out parsedScale))
        {
            currentController.UpdateScaleX(parsedScale);
        }
        else
        {
            scale_x.text = currentController.GetScaleX().ToString();
            Debug.Log("Value of input X cannot be parsed as a float");
        }
    }

    public void UpdateModelScaleY(string scaleY)
    {
        float parsedScale;
        if (float.TryParse(scaleY, out parsedScale))
        {
            currentController.UpdateScaleY(parsedScale);
        }
        else
        {
            scale_y.text = currentController.GetScaleY().ToString();
            Debug.Log("Value of input Y cannot be parsed as a float");
        }
    }
    public void UpdateModelScaleZ(string scaleZ)
    {
        float parsedScale;
        if (float.TryParse(scaleZ, out parsedScale))
        {
            currentController.UpdateScaleZ(parsedScale);
        }
        else
        {
            scale_z.text = currentController.GetScaleZ().ToString();
            Debug.Log("Value of input Z cannot be parsed as a float");
        }
    }
}
