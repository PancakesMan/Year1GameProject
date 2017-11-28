using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _31Toggle : MonoBehaviour {

    public List<GameObject> ThirdPersonObjects;
    public List<GameObject> FirstPersonObjects;

    bool first, third;
    public float cd;

	// Use this for initialization
	void Start () {
        first = false;
        third = true;
        cd = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        cd += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && cd > 0.25f)
        {
            foreach (GameObject go in FirstPersonObjects)
                go.SetActive(!first);

            foreach(GameObject go in ThirdPersonObjects)
                go.SetActive(!third);
            
            first = !first;
            third = !third;
            cd = 0.0f;
        }
    }
}
