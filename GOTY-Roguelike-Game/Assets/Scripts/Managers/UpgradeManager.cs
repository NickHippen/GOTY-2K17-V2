using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UpgradeManager : MonoBehaviour {

	public Texture upgradedTexture;

	private GameObject player;

	public GameObject Player {
		get {
			if (this.player == null) {
				this.player = GameObject.Find("remy");
			}
			return this.player;
		}
		set {
			this.player = value;
		}
	}

	void Start() {
		if (SceneManager.GetActiveScene().name == "10 Sloth") {
			CloseMenu ();
			return;
		}
		string classType = Player.GetComponent<AbilityController>().classType;
		transform.parent.parent.Find(classType).GetComponent<CanvasGroup>().alpha = 1;
		transform.parent.parent.Find(classType + "Upgrade").GetComponent<CanvasGroup>().alpha = 1;
		StartCoroutine("EnableCursor");
	}

	IEnumerator EnableCursor() {
		yield return new WaitForSeconds(1);
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		StopCoroutine("EnableCursor");
		yield return null;
	}

	void Update() {
		int baseIndex = transform.GetSiblingIndex();
		for (int i = 1; i <= 4; i++) {
			if (IsUpgraded(i - 1)) {
				Transform upgradeButton = transform.parent.GetChild(baseIndex + i);
				upgradeButton.GetComponent<RawImage>().texture = upgradedTexture;
			}
		}
	}

	public void UpgradeAbility(int index) {
		Debug.Log("Upgrade: " + index);
		AbilityController ac = Player.GetComponent<AbilityController>();
		if (ac == null) {
			Debug.Log("No ability controller");
			return;
		}
		Ability ability = ac.getAbilityList()[index];
		if (!ability.BonusEffectActive) {
			ability.BonusEffectActive = true;
			CloseMenu();
		}
	}

	public bool IsUpgraded(int index) {
		AbilityController ac = Player.GetComponent<AbilityController>();
		if (ac == null) {
			Debug.Log("No ability controller");
			return false;
		}
		Ability ability = ac.getAbilityList()[index];
		return ability.BonusEffectActive;
	}

	public void CloseMenu() {
		Destroy(GameObject.Find("UpgradeHUD"));
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

}
