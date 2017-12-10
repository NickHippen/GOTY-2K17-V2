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
		source.GetComponent<AudioSource>().volume = mm.GetComponent<MusicManager> ().getVolume ();

		Debug.Log("THING" + source.GetComponent<AudioSource>().volume);
		//Debug.Log ("SOUND EFFECTS"source.volume);
	}

	public void OnPointerEnter(PointerEventData eventData){
		source.GetComponent<AudioSource> ().Play ();
	}
}
