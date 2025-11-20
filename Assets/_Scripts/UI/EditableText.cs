using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditableText : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TMP_InputField inputField;

    public void EditText()
    {
        button.gameObject.SetActive(false);
        inputField.text = text.text;
        inputField.gameObject.SetActive(true);
    }

    public void SaveEdit()
    {
        text.text = inputField.text;
        inputField.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }

    public void SetText(string name)
    {
        text.text = name;
    }
}