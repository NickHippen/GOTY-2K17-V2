using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public float maxHealth;
	public float health;
	public bool invincible;

	void Start () {
		health = maxHealth;
		invincible = false;
	}

	public float Health {
		get {
			return health;
		}
		set {
			if (value > maxHealth) {
				health = maxHealth;
			} else if (value >= 0) {
				health = value;
			} else {
				value = 0;
			}
		}
	}

	public bool Living {
		get {
			return health > 0;
		}
	}
	
	public void Damage(float amount) {
		WeaponEmotion emotion = GetComponent<PlayerInventory>().getCurrentWeapon().GetComponent<WeaponData>().emotion;
		if (emotion == WeaponEmotion.Confidence) {
			amount *= 0.8f;
		} else if (emotion == WeaponEmotion.Shame) {
			if (Random.Range(0f, 1f) < 0.15f) {
				return; // Dodge
			}
		}
		if (!invincible) {
			Health -= amount;
			GameObject.Find ("HealthSlider").GetComponent<Slider> ().value = Health;
		}
	}

	public void Heal(float amount) {
		Health += amount;
		GameObject.Find ("HealthSlider").GetComponent<Slider> ().value = Health;
	}

}
