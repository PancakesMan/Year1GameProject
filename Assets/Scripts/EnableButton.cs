using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableButton : MonoBehaviour {

    public GameObject Button;    // Button to be enabled
    public float delay = 1.0f;   // Time before Button is enabled

	// Use this for initialization
	void Start () {
        Invoke("Enable", delay);
	}

    void Enable()
    {
        Button.SetActive(true);
    }
}
