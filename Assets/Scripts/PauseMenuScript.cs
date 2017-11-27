using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool paused = false;
    float cd = 0.5f;

    public void Update()
    {
        if (gameObject.name == "PlagueDoctorPrefab")
        {
            cd -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Escape) && cd < 0.0f)
            {
                paused = !paused;
                cd = 0.5f;
            }

            if (paused)
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Continue()
    {
        PauseMenuScript player = GameObject.Find("PlagueDoctorPrefab").GetComponent<PauseMenuScript>();
        player.paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
