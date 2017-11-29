using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EggScript : MonoBehaviour {
    [HideInInspector]
    public bool LadHatched = false;  // Has the Lad been hatched from this egg?
    [HideInInspector]
    public NavMeshAgent Lad;         // NavMeshAgent of the Lad hatched from the egg
    Animator LadAnimator;


    public bool arrowCoated = false; // Is the arrow coated?

    void Start()
    {
        LadAnimator = transform.parent.Find("LadAnimation").GetComponent<Animator>();
        Lad = transform.parent.Find("LadAnimation").GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = true;  // Update player to be colliding with egg
            other.GetComponent<ThirdPersonUserControl_Custom>().egg = this;               // Update player egg to this
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
