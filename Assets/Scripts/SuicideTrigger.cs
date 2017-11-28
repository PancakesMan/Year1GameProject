using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;
    private ThirdPersonUserControl_Custom controller;
    _31Toggle fpsToggle;

    public Object scene;
    public float delay = 5.0f;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("ThirdPersonController");

        animator = player.GetComponent<Animator>();
        controller = player.GetComponent<ThirdPersonUserControl_Custom>();
        fpsToggle = player.GetComponent<_31Toggle>();

    }

    void OnDisable()
    {
        controller.userHasControl = false;
        fpsToggle.active = false;
        animator.Play("Commit");
        Invoke("LoadFinalScene", delay);
    }

    void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
}
