using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class MouseAimCamera : MonoBehaviour {
		
	public GameObject target;
	public float rotateSpeed = 10;
    public float X, Y;

    public float LookLeftRight, LookUpDown;

	void Start() {
		transform.parent = target.transform;
		//transform.LookAt(target.transform);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
        LookLeftRight += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        LookUpDown += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        LookLeftRight = Mathf.Clamp(LookLeftRight, -X, X);
        LookUpDown = Mathf.Clamp(LookUpDown, -Y, Y);
    }

	void LateUpdate() {
        //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, horizontal);

        //float vertical = Input.GetAxis("Mouse Y") * -rotateSpeed;
        transform.localEulerAngles = new Vector3(-LookUpDown, LookLeftRight, 0);

        // Vertical, Horizontal
        //target.transform.Rotate(vertical, horizontal, 0);

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //target.transform.forward = player.transform.forward + (LookLeftRight * player.transform.right) + (LookUpDown * player.transform.up);
        //Mathf.Clamp(target.transform.localEulerAngles.x, -45, 45);
        //Mathf.Clamp(target.transform.localEulerAngles.y, -45, 45);
    }
}