using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnText : MonoBehaviour {


	public GameObject text1; 
	public int waitTime = 7;

	void Start () {
		Invoke ("SpawnText", waitTime);
	}

	void SpawnText (){
		text1.SetActive (true); 
	}
	

}
