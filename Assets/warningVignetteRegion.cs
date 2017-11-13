using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class warningVignetteRegion : MonoBehaviour {

    float origVignetteIntensity;
    float origVignetteSmoothness;

    [Range(0f, 1.0f)]
    public float fullWarningIntensity = 1f;
    [Range(0f, 1.0f)]
    public float fullWarningSmoothness = 1f;

    public PostProcessingProfile postProfileToAlter;

    float effectRadius;

    private void Start()
    {
        effectRadius = GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            origVignetteIntensity = postProfileToAlter.vignette.settings.intensity;
            origVignetteSmoothness = postProfileToAlter.vignette.settings.smoothness;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var vignette = postProfileToAlter.vignette.settings;
            float lerpValue = Vector3.Magnitude(transform.position - other.transform.position) / effectRadius;
            vignette.intensity = Mathf.Lerp(fullWarningIntensity, origVignetteIntensity, lerpValue);
            vignette.smoothness = Mathf.Lerp(fullWarningSmoothness, origVignetteSmoothness, lerpValue);

            postProfileToAlter.vignette.settings = vignette;
           
        }
    }

}
