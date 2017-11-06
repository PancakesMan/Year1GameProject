using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class monologuePost : MonoBehaviour {

	public PostProcessingProfile monologueProfile;
	public PostProcessingProfile normalProfile;

	void Start () {
			Invoke ("SwitchNormal", 2); 
		}
		
	public void SwitchNormal ()
	{
	 gameObject.GetComponent<PostProcessingBehaviour>().profile = normalProfile;
		Invoke ("SwitchMonologue", 1);
	}

	public void SwitchMonologue ()
	{
		gameObject.GetComponent<PostProcessingBehaviour>().profile = monologueProfile;
		Invoke ("SwitchNormal", 1); 
	}
}
