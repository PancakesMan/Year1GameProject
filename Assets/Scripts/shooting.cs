using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shooting : MonoBehaviour {

	public Camera mainCam;
	public Animator tree;
    public GameObject collisionToggle;

    private Animator xbowAnimator;

    void Start()
    {
        xbowAnimator = GetComponent<Animator>();
    }

    void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
            xbowAnimator.SetBool ("xbowFired",true);
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay (Input.mousePosition); 
			if (Physics.Raycast (ray, out hit)) {

			
				if (hit.collider.tag == "Boulder") {
					Debug.Log ("You shot the boulder!");
					hit.rigidbody.useGravity = true;
					Invoke ("TreeFalling", 3); 

				}
				if (hit.collider.tag == "WoodBlockade") {
					hit.collider.gameObject.SetActive(false);
				}

			}
					
		}
      
	

	}

		
	void TreeFalling()
	{
		tree.SetBool ("treeFalling", true);
        collisionToggle.SetActive(false);
    }

	}


