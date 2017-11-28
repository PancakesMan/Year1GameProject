using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;

    public Object scene;
    public float delay = 5.0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void OnTriggerExit(Collider other)
    {
        if (other.tag == "Suicide")
        {
            animator.Play("Commit");
            Invoke("LoadFinalScene", delay);
        }
    }

    void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
}
