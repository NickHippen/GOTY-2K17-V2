using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour {

	public int id = -1;
	public string name;
	public string desc;
	public float damage = 10f;
	public Vector3 rotation;
	public GameObject model;
	public Sprite icon;

	public ParticleSystem effect;

	private AudioSource audioSource;

	protected virtual void Start() {
		audioSource = GetComponent<AudioSource>();
		//effect.transform.parent = transform.parent;
		//effect.transform.position = transform.position;
	}

	public virtual void Attack() {
		PlayAttackAudio();
		if (effect != null) {
			//effect.transform.position = transform.position + transform.forward;
			effect.Emit(10);
		}
	}

	protected virtual void PlayAttackAudio() {
		if (audioSource != null) {
			audioSource.Play();
		}
	}

}
