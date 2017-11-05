using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

	public List<AbilityData> abilities;

	// Use this for initialization
	void Start () {
		abilities[0] = Instantiate (abilities [0]);
		abilities[0].transform.SetParent (gameObject.transform);
		//abilities [0].transform.position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void useAbility(bool a1, bool a2, bool a3, bool a4){
		if (a1 && abilities[0].effect != null){
			Debug.Log ("Entered");
			abilities [0].effect.Emit (10);
		}

	}


}
