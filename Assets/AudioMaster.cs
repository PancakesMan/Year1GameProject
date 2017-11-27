using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioClip;

public class AudioMaster : MonoBehaviour {

	public AudioClip clipAmb;
	public AudioClip clipSus;
	public AudioClip clipDetect;
	public AudioSource newAudio;

	private AudioSource audioAmb;
	private AudioSource audioSus;
	private AudioSource audioDetect;

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol){
		newAudio = gameObject.AddComponent(AudioSource); 
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public void Awake(){
		audioAmb = AddAudio (clipAmb, true, true, 0.2);
		audioSus = AddAudio (clipSus, true, true, 0.4);
		audioDetect = AddAudio (clipDetect, true, true, 0.8);
	}
}		
