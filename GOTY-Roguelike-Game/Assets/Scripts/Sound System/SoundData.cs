using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour {

	public List<AudioClip> soundEffects;
	AudioSource source;
	public int directIndex;
	public int loopIndex;
	public float delay;

	// Use this for initialization
	protected virtual void Start () {
		this.gameObject.AddComponent<AudioSource>();
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void playSound(int index){
		if (soundEffects [index] != null) {
			source.PlayOneShot (soundEffects [index]);
		}
	}

	public virtual void playSound(){
		if (soundEffects [directIndex] != null) {
			source.clip = soundEffects [directIndex];
			//source.PlayOneShot (soundEffects [directIndex]);
			source.PlayDelayed(delay);
		}
	}

	public virtual void playLoop(){
		if (soundEffects [loopIndex] != null) {
			source.loop = true;
			source.PlayOneShot (soundEffects [loopIndex]);
		}
	}

	public virtual void stopLoop(){
		source.Stop ();
		source.loop = false;
	}
}
