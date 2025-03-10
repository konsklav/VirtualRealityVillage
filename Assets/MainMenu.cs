using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public TMP_Text startButtonText;
    public TMP_Text titleText;

    private void Start()
    {
        Time.timeScale = 0; // Pause the game when menu is open
        InitializeLanguage();
    }

    public void StartGame()
    {
        Time.timeScale = 1; // Resume game when clicking Start
        SceneManager.LoadScene("SampleScene");
    }

    public void OnLanguageChange()
    {
        string selectedLanguage = languageDropdown.options[languageDropdown.value].text;
        PlayerPrefs.SetString("SelectedLanguage", selectedLanguage);
        PlayerPrefs.Save();

        LanguageManager.Instance.SetLanguage(selectedLanguage);
        ApplyTranslations();
    }

    private void InitializeLanguage()
    {
        // Load saved language or default to English
        string savedLanguage = PlayerPrefs.GetString("SelectedLanguage", "English");
        int dropdownIndex = languageDropdown.options.FindIndex(option => option.text == savedLanguage);
        if (dropdownIndex >= 0)
            languageDropdown.value = dropdownIndex;

        LanguageManager.Instance.LoadLanguage(savedLanguage);
        ApplyTranslations();
    }

    private void ApplyTranslations()
    {
        titleText.text = LanguageManager.Instance.GetText("menu_select_language");
        startButtonText.text = LanguageManager.Instance.GetText("menu_start");
    }
}
