﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class MouseAimCamera : MonoBehaviour {
		
	public GameObject target;
	public float rotateSpeed = 10;


	void Start() {
		transform.parent = target.transform;
		transform.LookAt(target.transform);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
		//if (Input.GetKey(KeyCode.Escape))
			//Screen.lockCursor = false;
		//else
           // Screen.lockCursor = true;
    }

	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		horizontal = Mathf.Clamp (horizontal, -5, 5);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, horizontal);

		float vertical = Input.GetAxis("Mouse Y") * -rotateSpeed;
		vertical = Mathf.Clamp (vertical, -5, 5);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, vertical);

        // Vertical, Horizontal
		target.transform.Rotate(vertical, horizontal, 0);
	}
}