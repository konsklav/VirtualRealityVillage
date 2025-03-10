using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    private Dictionary<string, string> localizedText;
    private string selectedLanguage = "English"; // Default language

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLanguage(string language)
    {
        selectedLanguage = language;
        localizedText = new Dictionary<string, string>();

        TextAsset textAsset = Resources.Load<TextAsset>($"Languages/{language}"); // Ensure XML files are inside "Resources/Languages"
        if (textAsset == null)
        {
            Debug.LogError($"Language file not found: {language}.xml");
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList elements = xmlDoc.GetElementsByTagName("string");

        foreach (XmlNode node in elements)
        {
            string id = node.Attributes["id"].Value;
            string value = node.InnerText;
            localizedText[id] = value;
        }
    }

    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : $"[{key}]";
    }

    public void SetLanguage(string language)
    {
        selectedLanguage = language;
        LoadLanguage(language);
    }

    public string GetSelectedLanguage()
    {
        return selectedLanguage;
    }
}
