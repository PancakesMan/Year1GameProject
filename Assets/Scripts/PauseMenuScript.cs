using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;  // PauseMenu canvas
    public bool paused = false;   // Is the game paused?
    float cd = 0.5f;              // Cooldown for pause button

    public void Update()
    {
        // If the script is attached to the player
        if (gameObject.name == "PlagueDoctorPrefab")
        {
            // Decrease the cooldown
            cd -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Escape) && cd < 0.0f)
            {
                paused = !paused; // Toggle paused state
                cd = 0.5f;        // Reset the cooldown
                PauseUnpause();   // Pause/Unpause the game
            }
        }
    }

    public void PauseUnpause()
    {
        if (paused)
        {
            pauseMenu.SetActive(true);                   // Activate the pause menu
            Cursor.lockState = CursorLockMode.None;      // Unlock the cursor
            Cursor.visible = true;                       // Make the cursor visible
            Time.timeScale = 0;                          // Set timescale to 0 to pause game
        }
        else
        {
            pauseMenu.SetActive(false);                  // Deactivate the pause menu
            Cursor.lockState = CursorLockMode.Locked;    // Lock the cursor
            Cursor.visible = false;                      // Hide the cursor
            Time.timeScale = 1;                          // Set timescale to 1 to pause game
        }                                                
    }

    // used for the Continue button on the pause menu
    public void Continue()
    {
        // Get the PauseMenu Script on the player
        PauseMenuScript player = GameObject.Find("PlagueDoctorPrefab").GetComponent<PauseMenuScript>();

        // Set the paused state to unpaused
        player.paused = false;

        // Call the PauseUnpause function
        player.PauseUnpause();
    }
}
