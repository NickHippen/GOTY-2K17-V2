using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	private AudioSource audioSource;
	public float offset = 0.2f;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (audioSource.volume);
	}

	void OnLevelWasLoaded(int level){
		AudioClip thisLevelMusic = levelMusicChangeArray [level];
		if (thisLevelMusic) { //If there's some music attached
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	public void ChangeVolume(float volume){
		//if (volume > offset) {
			audioSource.volume = (float)volume - offset;
		/*} else {
			audioSource.volume = (float)volume;
		}*/
	}

	public float getVolume(){
		return audioSource.volume;
	}
}
