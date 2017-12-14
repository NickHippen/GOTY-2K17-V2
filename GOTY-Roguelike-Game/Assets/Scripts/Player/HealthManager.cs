using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HealthManager : MonoBehaviour {

	public float maxHealth;
	public float health;
	public bool invincible;
	public Image healthSlider;

	public Canvas mainHUD;
	public Canvas deathHUD;

	private bool created = false;

	public SoundData sfx;

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
				health = 0;
			}
		}
	}

	public bool Living {
		get {
			return health > 0;
		}
	}

	void Start() {
		health = maxHealth;
		invincible = false;
		sfx = GetComponent<SoundData> ();
	}

	void Update() {
		if (!Living) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			//Camera.main.GetComponent<CameraController>().enabled = false;
			Camera.main.GetComponentInParent<CameraFollow>().enabled = false;
			transform.GetComponent<ThirdPersonCharacter>().m_Animator.SetBool("Dead", true);
			transform.GetComponent<ThirdPersonCharacter>().enabled = false;
			transform.GetComponent<ThirdPersonUserControl>().enabled = false;
			transform.GetComponent<Rigidbody>().isKinematic = true;
			mainHUD.gameObject.SetActive(false);
			StartCoroutine("ShowDeathHUD");
			Time.timeScale = 0.5f;
		}
	}

	private IEnumerator ShowDeathHUD() {
		yield return new WaitForSeconds(3);
		if (!created) {
			Instantiate(deathHUD);
			created = true;
		}
		//deathHUD.gameObject.SetActive(true);
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
		if (!invincible && Living) {
			int roll = Random.Range (0, 2);
			Debug.Log (roll);
			switch (roll) {
			case 0:
				sfx.playSound (Random.Range (4, 8));
				break;
			default:
				break;
			}
			Health -= amount;
			healthSlider.fillAmount = Health/100;
		}
	}

	public void Heal(float amount) {
		Health += amount;
		healthSlider.fillAmount = Health/100;
	}

}
