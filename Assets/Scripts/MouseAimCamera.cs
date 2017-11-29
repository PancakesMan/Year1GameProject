using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{
	public GameObject target;                 // Object Camera is looking at
	public float rotateSpeed = 10;            // Speed the Camera rotates
    public float X, Y;                        // Min/Max X/Y rotation
    public float LookLeftRight, LookUpDown;   // The angle the Camera is looking on X/Y

	void Start() {
        // Make the target this objects parent
		transform.parent = target.transform;

        // Hide the cursor from the user
        // and lock the cursor to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
        // Increase/Decrease the angle you are looking on the X axis
        LookLeftRight += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        // Increase/Decrease the angle you are looking on the Y axis
        LookUpDown += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        // Clamp the angle you are looking on X/Y axis
        LookLeftRight = Mathf.Clamp(LookLeftRight, -X, X);
        LookUpDown = Mathf.Clamp(LookUpDown, -Y, Y);
    }

	void LateUpdate() {
        // Set the local euler angles of the Camera
        transform.localEulerAngles = new Vector3(-LookUpDown, LookLeftRight, 0);
    }
}