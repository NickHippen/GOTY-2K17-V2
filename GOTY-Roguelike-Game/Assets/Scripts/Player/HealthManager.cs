using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public float maxHealth;
	public float health;

	void Start () {
		health = maxHealth;
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
		Health -= amount;
		GameObject.Find ("HealthSlider").GetComponent<Slider> ().value = Health;
	}

	public void Heal(float amount) {
		Health += amount;
		GameObject.Find ("HealthSlider").GetComponent<Slider> ().value = Health;
	}

}
