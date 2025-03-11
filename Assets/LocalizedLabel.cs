using UnityEngine;
using TMPro;

public class LocalizedLabel : MonoBehaviour
{
    public string textKey; // The key that matches the XML entry
    private TMP_Text labelText;

    void Start()
    {
        labelText = GetComponent<TMP_Text>();

        // Apply the localized text when the scene loads
        UpdateLabel();
    }

    public void UpdateLabel()
    {
        if (LanguageManager.Instance != null)
        {
            labelText.text = LanguageManager.Instance.GetLabelText(textKey);
        }
    }
}
