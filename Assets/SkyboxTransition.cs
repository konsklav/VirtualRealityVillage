using UnityEngine;
using System.Collections;

public class SkyboxTransition : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox; // Skybox material for daytime
    public Material nightSkybox; // Skybox material for nighttime

    [Header("Sunlight Settings")]
    public Light sunLight; // Reference to the directional light (sun)
    public float dayIntensity = 1.2f; // Light intensity during the day
    public float nightIntensity = 0.2f; // Light intensity during the night

    [Header("Transition Settings")]
    public float transitionDuration = 10f; // Time taken to transition between day and night
    public float waitTime = 20f; // Time to wait before switching (day/night cycle length)

    private bool isDay = true; // Tracks whether it's currently day

    void Start()
    {
        // Set initial skybox to daytime
        RenderSettings.skybox = daySkybox;
        sunLight.intensity = dayIntensity;

        // Start the cycle of transitioning between day and night
        StartCoroutine(CycleDayNight());
    }

    IEnumerator CycleDayNight()
    {
        while (true) // Infinite loop for continuous cycling
        {
            yield return new WaitForSeconds(waitTime); // Wait before transitioning to night

            yield return StartCoroutine(ChangeLightIntensity(dayIntensity, nightIntensity)); // Fade sunlight

            RenderSettings.skybox = nightSkybox; // Switch to night skybox
            DynamicGI.UpdateEnvironment(); // Update global illumination

            yield return new WaitForSeconds(waitTime); // Wait before transitioning back to day

            yield return StartCoroutine(ChangeLightIntensity(nightIntensity, dayIntensity)); // Fade sunlight back

            RenderSettings.skybox = daySkybox; // Switch to day skybox
            DynamicGI.UpdateEnvironment(); // Update global illumination
        }
    }

    IEnumerator ChangeLightIntensity(float from, float to)
    {
        float elapsedTime = 0f; // Timer to track transition progress

        while (elapsedTime < transitionDuration) // Loop until transition is complete
        {
            sunLight.intensity = Mathf.Lerp(from, to, elapsedTime / transitionDuration); // Gradually change light intensity
            elapsedTime += Time.deltaTime; // Increment timer
            yield return null; // Wait for the next frame
        }

        sunLight.intensity = to; // Ensure final intensity is set
    }
}
