using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerSound : MonoBehaviour, IPointerEnterHandler {
	GameObject source;
	// Use this for initialization
	GameObject mm;
	void Start () {
		source = GameObject.Find("ButtonSound");
		mm = GameObject.Find ("MusicManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (mm != null && mm.GetComponent<MusicManager>() != null) {
			source.GetComponent<AudioSource>().volume = mm.GetComponent<MusicManager>().getVolume();
		} else {
			source.GetComponent<AudioSource>().volume = 1f;
		}

		Debug.Log("THING" + source.GetComponent<AudioSource>().volume);
		//Debug.Log ("SOUND EFFECTS"source.volume);
	}

	public void OnPointerEnter(PointerEventData eventData){
		source.GetComponent<AudioSource> ().Play ();
	}
}
