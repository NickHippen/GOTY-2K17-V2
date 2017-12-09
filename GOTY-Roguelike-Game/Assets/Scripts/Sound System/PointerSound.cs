using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerSound : MonoBehaviour, IPointerEnterHandler {
	GameObject source;
	// Use this for initialization
	void Start () {
		source = GameObject.Find("ButtonSound");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData){
		source.GetComponent<AudioSource> ().Play ();
	}
}
