using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EggScript : MonoBehaviour {

    public GameObject moonWhite;
    public GameObject moonGreen;

    [HideInInspector]
    public bool LadHatched = false;
    [HideInInspector]
    public NavMeshAgent Lad;
    Animator LadAnimator;


    public bool arrowCoated = false;

    void Start()
    {
        LadAnimator = transform.parent.Find("LadAnimation").GetComponent<Animator>();
        Lad = transform.parent.Find("LadAnimation").GetComponent<NavMeshAgent>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (arrowCoated == true)
        {
            moonGreen.SetActive(true);
            moonWhite.SetActive(false);
        }
        else if (arrowCoated == false)
        {
            moonWhite.SetActive(true);
            moonGreen.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = true;
            other.GetComponent<ThirdPersonUserControl_Custom>().egg = this;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Arrow Coated!");
                arrowCoated = true;
            }
        }
    }
        

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = false;
            other.GetComponent<ThirdPersonUserControl_Custom>().egg = null;
        }
    }

    public void HatchLad(float waitTime = 0.0f)
    {
        Invoke("PlayHatchingAnimation", waitTime);
    }

    void PlayHatchingAnimation()
    {
        LadAnimator.Play("Lad Hatching");
    }
}
