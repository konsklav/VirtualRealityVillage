using UnityEngine;

public class SkyboxTransition : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Sunlight Settings")]
    public Light sunLight;
    public float dayIntensity = 1.2f;  // Full sunlight intensity
    public float nightIntensity = 0.2f; // Dim moonlight intensity

    [Header("Timing Settings")]
    public float transitionDuration = 5f; // Time for fading the light
    private float timer = 0f;
    private bool isDay = true;
    private bool transitioning = false;

    void Start()
    {
        RenderSettings.skybox = daySkybox;
        sunLight.intensity = dayIntensity;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10f && timer < 15f) // First transition (Light dims)
        {
            transitioning = true;
            float t = (timer - 10f) / transitionDuration;
            sunLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, t);
        }
        else if (timer >= 15f && timer < 25f) // Hold at night
        {
            if (isDay)
            {
                isDay = false;
                RenderSettings.skybox = nightSkybox; // Skybox switches at second 15
                DynamicGI.UpdateEnvironment();
            }
            transitioning = false;
        }
        else if (timer >= 25f && timer < 30f) // Second transition (Light brightens)
        {
            transitioning = true;
            float t = (timer - 25f) / transitionDuration;
            sunLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, t);
        }
        else if (timer >= 30f) // Hold at day and reset cycle
        {
            if (!isDay)
            {
                isDay = true;
                RenderSettings.skybox = daySkybox; // Skybox switches at second 30
                DynamicGI.UpdateEnvironment();
            }
            transitioning = false;
            timer = 0f; // Reset the timer for the next cycle
        }
    }
}
