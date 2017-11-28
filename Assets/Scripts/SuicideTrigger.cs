using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;
    private ThirdPersonUserControl_Custom controller;

    public Object scene;
    public float delay = 5.0f;

	// Use this for initialization
	void Start () {
        animator = GameObject.Find("ThirdPersonController").GetComponent<Animator>();
        controller = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonUserControl_Custom>();
	}

    void OnDisable()
    {
        controller.userHasControl = false;
        animator.Play("Commit");
        Invoke("LoadFinalScene", delay);
    }

    void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
}
