using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

	public List<AbilityData> abilities;
	public Text txt;
	string abiltyText = "Z: {0} X: {1} C: {2} V: {3}";
	// Use this for initialization
	void Start () {
		txt = GameObject.Find ("UIText").GetComponent<Text>();
		abilities[0] = Instantiate (abilities [0]);
		abilities[0].transform.SetParent (gameObject.transform);

		string uitext = string.Format (abiltyText, abilities [0].name, "", "", "");
		txt.text = uitext;

		//abilities [0].transform.position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void useAbility(bool a1, bool a2, bool a3, bool a4){
		if (a1 && abilities[0].effect != null){
			Debug.Log ("Entered");
			abilities [0].effect.Emit(10);
		}

	}


}
