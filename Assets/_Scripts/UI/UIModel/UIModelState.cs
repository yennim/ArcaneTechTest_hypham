using UnityEngine;

public class UIModelState : MonoBehaviour
{
    private Transform mainCameraTransform;
    void OnEnable()
    {
        if (mainCameraTransform == null)
        {
            if (Camera.main != null)
            {
                mainCameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogError("No main camera found in the scene for UIEditModel to follow.");
                enabled = false;
            }
        }

        transform.LookAt(mainCameraTransform.position);
    }
}
