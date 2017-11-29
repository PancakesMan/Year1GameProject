using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        // Hide the cursor from the user
        // And lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
