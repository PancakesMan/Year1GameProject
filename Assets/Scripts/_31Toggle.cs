using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _31Toggle : MonoBehaviour {

    public List<GameObject> ThirdPersonObjects; // List of objects to be enabled when in 3rd person
    public List<GameObject> FirstPersonObjects; // List of objects to be enabled when in 1st person

    // first = 1st person mode active
    // third = 3rd person mode active
    bool first, third;

    public bool active = true; // Can we switch modes?
    public float cd;           // cooldown for switching modes

	// Use this for initialization
	void Start () {
        first = false; // player stars in third person, first = false
        third = true;  // and therefore third = true
        cd = 0.0f;     // cooldown for switching modes initialzied
	}
	
	// Update is called once per frame
	void Update () {
        cd += Time.deltaTime; // add to cooldown timer

        if (Input.GetMouseButtonDown(1) && cd > 0.25f && active)
        {
            // disable or enable all first person objects
            foreach (GameObject go in FirstPersonObjects)
                go.SetActive(!first);

            // disable or enable all third person objects
            foreach(GameObject go in ThirdPersonObjects)
                go.SetActive(!third);
            
            first = !first;  // Toggle first person mode state
            third = !third;  // toggle third person mode state
            cd = 0.0f;       // reset cooldown
        }
    }
}
