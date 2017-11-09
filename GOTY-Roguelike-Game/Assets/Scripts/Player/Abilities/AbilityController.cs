using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public List<AbilityData> abilities;

    public string classType;
    public AnimatorOverrideController gunslingerOverride;
    public AnimatorOverrideController berserkerOverride;

    public Text txt;

    string abiltyText = "Z: {0} X: {1} C: {2} V: {3}";

    // Use this for initialization
    void Start() {
		if (txt != null) {
			txt = GameObject.Find ("UIText").GetComponent<Text> ();

			string uitext = string.Format (abiltyText, abilities [0].abilityName, abilities [1].abilityName, "", "");
			txt.text = uitext;
		}

		for (int x = 0; x < abilities.Count; x++) {
			abilities [x] = Instantiate (abilities [x]);
			abilities [x].transform.SetParent (gameObject.transform);
		}
        //print(abilities[0].effect);

        //if (classType.ToLower().Equals("berserker"))
        //{
        //    setBerserkerData();
        //}
        //else
        //{
        //    setGunslingerData();
        //}

        //abilities [0].transform.position = gameObject.transform.position;
    }

    public void useAbility(bool a1, bool a2, bool a3, bool a4) {
        if (a1 && abilities[0].effect != null) {
            Debug.Log("Entered");
            abilities[0].effect.Emit(10);
        }

		if (a2 && abilities[1].effect != null) {
			Debug.Log("Entered");
			abilities[1].effect.Emit(100);
		}

		if (a1 && abilities[2].effect != null) {
			Debug.Log("Entered");
			abilities[0].effect.Emit(10);
		}

		if (a2 && abilities[3].effect != null) {
			Debug.Log("Entered");
			abilities[1].effect.Emit(100);
		}
    }

    public string getClassType()
    {
        return classType;
    }

    // applies the override controller of the current class type to weapon animations
    public AnimatorOverrideController getClassOverrideController(RuntimeAnimatorController anim)
    {
        if (classType.ToLower().Equals("berserker"))
        {
            berserkerOverride.runtimeAnimatorController = anim;
            return berserkerOverride;
        }
        else
        {
            gunslingerOverride.runtimeAnimatorController = anim;
            return gunslingerOverride;
        }
    }

    //private void setBerserkerData()
    //{
    //    abilities.Add(new Cyclone());
    //    abilities.Add(new HardKick());
    //    abilities.Add(new TankUp());
    //    abilities.Add(new Adrenaline());
    //}

    //private void setGunslingerData()
    //{
    //    abilities.Add(new Grenade());
    //    abilities.Add(new Grenade());
    //    abilities.Add(new Grenade());
    //    abilities.Add(new Grenade());
    //}
}
