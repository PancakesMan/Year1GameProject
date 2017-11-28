using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;
    private ThirdPersonUserControl_Custom controller;
    private _31Toggle fpsToggle;
    private GameObject playerCamera;
    private GameObject deathView;

    public Object scene;
    public float delay = 5.0f;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("ThirdPersonController");

        animator = player.GetComponent<Animator>();
        controller = player.GetComponent<ThirdPersonUserControl_Custom>();
        fpsToggle = GameObject.Find("PlagueDoctorPrefab").GetComponent<_31Toggle>();
        playerCamera = GameObject.Find("MainCamera");
        deathView = GameObject.Find("DeathView");

    }

    void OnDisable()
    {
        controller.userHasControl = false;
        fpsToggle.active = false;
        animator.Play("Commit");
        Invoke("LoadFinalScene", delay);
        playerCamera.SetActive(false);
        deathView.SetActive(true);
    }

    void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
}
