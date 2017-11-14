using UnityEngine;
using System.Collections;

public class CursorDisplay : MonoBehaviour {

	//UnityStandardAssets.Characters.FirstPerson.FirstPersonController character;

	public bool startHidden = true;

	void Awake (){
		if (startHidden) {
			HideCursor ();
		} else {
			ShowCursor ();
		}
	}

	public void ShowCursor (){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void HideCursor(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
