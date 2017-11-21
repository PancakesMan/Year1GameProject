using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
