using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

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
		Debug.Log("Start");
		string classType = Player.GetComponent<AbilityController>().classType;
		Debug.Log("Checking for " + classType);
		transform.parent.parent.FindChild(classType).GetComponent<CanvasGroup>().alpha = 1;
		transform.parent.parent.FindChild(classType + "Upgrade").GetComponent<CanvasGroup>().alpha = 1;
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Update() {
		int baseIndex = transform.GetSiblingIndex();
		for (int i = 1; i <= 4; i++) {
			if (IsUpgraded(i-1)) {
				Transform upgradeButton = transform.parent.GetChild(baseIndex + i);
				for (int j = 0; j < upgradeButton.childCount; j++) {
					Text text = upgradeButton.GetChild(j).GetComponent<Text>();
					if (text == null) {
						continue;
					}
					text.color = Color.green;
				}
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
