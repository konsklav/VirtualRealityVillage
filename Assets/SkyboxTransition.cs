using UnityEngine;
using System.Collections;

public class SkyboxTransition : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Sunlight Settings")]
    public Light sunLight;
    public float dayIntensity = 1.2f;
    public float nightIntensity = 0.2f;

    [Header("Transition Settings")]
    public float transitionDuration = 10f; // Transition now takes 10s
    public float waitTime = 50f; // Wait time between transitions is 50s

    private bool isDay = true;

    void Start()
    {
        RenderSettings.skybox = daySkybox;
        sunLight.intensity = dayIntensity;
        StartCoroutine(CycleDayNight());
    }

    IEnumerator CycleDayNight()
    {
        while (true)
        {
            // **Start with a 50-second wait before the first transition**
            yield return new WaitForSeconds(waitTime);

            // Dim light over 10s
            yield return StartCoroutine(ChangeLightIntensity(dayIntensity, nightIntensity));

            // Change skybox to night
            RenderSettings.skybox = nightSkybox;
            DynamicGI.UpdateEnvironment();

            // Wait at night for 50s
            yield return new WaitForSeconds(waitTime);

            // Brighten light over 10s
            yield return StartCoroutine(ChangeLightIntensity(nightIntensity, dayIntensity));

            // Change skybox to day
            RenderSettings.skybox = daySkybox;
            DynamicGI.UpdateEnvironment();
        }
    }

    IEnumerator ChangeLightIntensity(float from, float to)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            sunLight.intensity = Mathf.Lerp(from, to, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sunLight.intensity = to; // Ensure final value is set correctly
    }
}
