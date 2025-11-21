using System;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class UIEditModelOverlay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Scaling")]
    [SerializeField] private Slider scaleOverall;
    [SerializeField] private TMP_InputField scale_x;
    [SerializeField] private TMP_InputField scale_y;
    [SerializeField] private TMP_InputField scale_z;

    [Header("Colour")]
    [SerializeField] private FlexibleColorPicker colorPicker;

    private ModelController SelectedController => ProjectManager.Instance.SelectedModel;
   
    private void OnEnable()
    {
        ProjectManager.Instance.OnModelSelected += UpdateCurrentController;
        ProjectManager.Instance.OnModelFaceIndexSelected += OpenColorPicker;

        scaleOverall.onValueChanged.AddListener(UpdateModelScaleOverall);
        scale_x.onValueChanged.AddListener(UpdateModelScaleX);
        scale_y.onValueChanged.AddListener(UpdateModelScaleY);
        scale_z.onValueChanged.AddListener(UpdateModelScaleZ);

        colorPicker.onColorChange.AddListener(ChangeColor);
    }

    private void OnDisable()
    {
        if (ProjectManager.Instance)
        {
            ProjectManager.Instance.OnModelSelected -= UpdateCurrentController;
            ProjectManager.Instance.OnModelFaceIndexSelected -= OpenColorPicker;
        }

        scaleOverall.onValueChanged.RemoveListener(UpdateModelScaleOverall);
        scale_x.onValueChanged.RemoveListener(UpdateModelScaleX);
        scale_y.onValueChanged.RemoveListener(UpdateModelScaleY);
        scale_z.onValueChanged.RemoveListener(UpdateModelScaleZ);

        colorPicker.onColorChange.RemoveListener(ChangeColor);
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

    private void OpenColorPicker()
    {
        Debug.Log("face selected, color picker should activate");
        colorPicker.gameObject.SetActive(true);
    }

    private void CloseColorPicker()
    {
        colorPicker.gameObject.SetActive(false);
    }

    private void UpdateCurrentController(ModelController modelController)
    {
        if (modelController != null)
        {
            scaleOverall.value = 1;
            scale_x.text = modelController.GetScaleX().ToString();
            scale_y.text = modelController.GetScaleY().ToString();
            scale_z.text = modelController.GetScaleZ().ToString();

            ActivateCanvasGroup();
        }
        else
        {
            DeactivateCanvasGroup();
        }

        CloseColorPicker();
    }

    #region Scaling
    private void UpdateModelScaleOverall(float multiplier)
    {
        SelectedController.UpdateScaleOverall(multiplier);
    }

    private void UpdateModelScaleX(string scaleX) 
    {
        float parsedScale;
        if (float.TryParse(scaleX, out parsedScale))
        {
            SelectedController.UpdateScaleX(parsedScale);
        }
        else
        {
            scale_x.text = SelectedController.GetScaleX().ToString();
            Debug.Log("Value of input X cannot be parsed as a float");
        }
    }

    private void UpdateModelScaleY(string scaleY)
    {
        float parsedScale;
        if (float.TryParse(scaleY, out parsedScale))
        {
            SelectedController.UpdateScaleY(parsedScale);
        }
        else
        {
            scale_y.text = SelectedController.GetScaleY().ToString();
            Debug.Log("Value of input Y cannot be parsed as a float");
        }
    }
    private void UpdateModelScaleZ(string scaleZ)
    {
        float parsedScale;
        if (float.TryParse(scaleZ, out parsedScale))
        {
            SelectedController.UpdateScaleZ(parsedScale);
        }
        else
        {
            scale_z.text = SelectedController.GetScaleZ().ToString();
            Debug.Log("Value of input Z cannot be parsed as a float");
        }
    }
    #endregion

    #region Colour
    private void ChangeColor(Color color)
    {
        //SelectedController.SetSelectedFaceColour(color);
        SelectedController.SetMaterialColour(color);
    }
    #endregion
}
