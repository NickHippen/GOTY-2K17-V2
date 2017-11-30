using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour {

	public List<AudioClip> soundEffects;
	AudioSource source;

	// Use this for initialization
	protected virtual void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void playSound(int index){
		source.PlayOneShot (soundEffects [index]);
	}
}
