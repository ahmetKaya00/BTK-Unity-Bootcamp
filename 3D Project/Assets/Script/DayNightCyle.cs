using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCyle : MonoBehaviour
{
    [SerializeField] private Light Light;
    [SerializeField] private float transitionDuration = 15f;
    [SerializeField] private float pauseDuration = 5f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float minIntensity = 0f;

    void Start()
    {
        StartCoroutine(CyleNight());
    }

    private IEnumerator CyleNight()
    {
        while (true) {
            yield return StartCoroutine(ChangeLight(minIntensity, maxIntensity, transitionDuration));

            yield return new WaitForSeconds(pauseDuration);

            yield return StartCoroutine(ChangeLight(maxIntensity, minIntensity, transitionDuration));

            yield return new WaitForSeconds(pauseDuration);

        }
    }

    private IEnumerator ChangeLight(float fromIntensity, float toIntensiy, float duration)
    {
        float elapseTime = 0f;

        if(elapseTime < duration)
        {
            Light.intensity = Mathf.Lerp(fromIntensity, toIntensiy, elapseTime / duration);
            elapseTime += Time.deltaTime;
            yield return null;
        }

        Light.intensity = toIntensiy;

    }
}
