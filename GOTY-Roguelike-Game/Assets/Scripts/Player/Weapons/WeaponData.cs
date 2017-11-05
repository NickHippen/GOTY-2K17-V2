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

	private AudioSource audioSource;

	protected virtual void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	public virtual void Attack() {
		PlayAttackAudio();
	}

	protected virtual void PlayAttackAudio() {
		if (audioSource != null) {
			audioSource.Play();
		}
	}

}
