using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    // Singleton instance to ensure only one LanguageManager exists
    public static LanguageManager Instance;

    // Dictionary to store key-value pairs of localized text
    private Dictionary<string, string> localizedText;

    // Stores the currently selected language (default is English)
    private string selectedLanguage = "English";

    private void Awake()
    {
        // Ensure that only one instance of LanguageManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }
    }

    // Loads language data from an XML file
    public void LoadLanguage(string language)
    {
        selectedLanguage = language;
        localizedText = new Dictionary<string, string>(); // Initialize dictionary

        // Load the language file from the Resources/Languages folder
        TextAsset textAsset = Resources.Load<TextAsset>($"Languages/{language}");
        if (textAsset == null)
        {
            Debug.LogError($"Language file not found: {language}.xml");
            return; // Exit if the file is missing
        }

        // Parse the XML content
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList elements = xmlDoc.GetElementsByTagName("string");

        // Store key-value pairs from the XML file into the dictionary
        foreach (XmlNode node in elements)
        {
            string id = node.Attributes["id"].Value; // Unique key for the text
            string value = node.InnerText; // Translated text
            localizedText[id] = value; // Add to dictionary
        }
    }

    // Retrieves the translated text for a given key
    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : $"[{key}]"; // Return key if not found
    }

    // Sets the language and reloads the localization data
    public void SetLanguage(string language)
    {
        selectedLanguage = language;
        LoadLanguage(language);
    }

    // Returns the currently selected language
    public string GetSelectedLanguage()
    {
        return selectedLanguage;
    }
}
