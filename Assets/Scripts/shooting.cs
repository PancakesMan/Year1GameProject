using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class shooting : MonoBehaviour {

	public Camera mainCam;
	public Animator tree;
    public GameObject collisionToggle;//, ThirdPersonObject;
    public GameObject ThirdPersonObject;

    [HideInInspector]
    public bool coated;

    private Animator xbowAnimator;
    private ThirdPersonUserControl_Custom player;

    void Start()
    {
        xbowAnimator = GetComponent<Animator>();
        player = ThirdPersonObject.GetComponent<ThirdPersonUserControl_Custom>();
    }

    void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
            xbowAnimator.SetBool ("xbowFired",true);
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay (Input.mousePosition); 
			if (Physics.Raycast (ray, out hit)) {
                Debug.Log(hit.collider.tag + " was hit");
				if (hit.collider.tag == "Boulder") {
					Debug.Log ("You shot the boulder!");
					hit.rigidbody.useGravity = true;
					Invoke ("TreeFalling", 3); 
				}
				else if (hit.collider.tag == "WoodBlockade" && coated) {
                    player.followingLad.SetDestination(hit.collider.transform.position);
                    player.followingLad.transform.GetComponent<Animator>().Play("Walking");
					//hit.collider.gameObject.SetActive(false);
				}

			}

            // Set coated to false
            //coating must be re-applied after every shot
            coated = false;
		}
      
	

	}

		
	void TreeFalling()
	{
		tree.SetBool ("treeFalling", true);
        collisionToggle.SetActive(false);
    }

	}


