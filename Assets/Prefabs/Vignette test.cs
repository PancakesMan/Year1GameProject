using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
public class Vignettetest : MonoBehaviour {

    public PostProcessingProfile postMalone;
    void OnEnable()
    {
        var behaviour = GetComponent<PostProcessingBehaviour>();

        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }

        postMalone = Instantiate(behaviour.profile);
        behaviour.profile = postMalone;
    }

    void Update()
    {
        var vignette = postMalone.vignette.settings;
        vignette.smoothness = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup) * 0.99f) + 0.01f;
        postMalone.vignette.settings = vignette;
    }
}
