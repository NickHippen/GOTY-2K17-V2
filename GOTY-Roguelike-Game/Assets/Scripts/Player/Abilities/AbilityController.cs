using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public AbilityPrefabList abilityList;
    public string classType;
    public Text txt;
    string abiltyText = "Z: {0} X: {1} C: {2} V: {3}";

    private List<Ability> abilities;

    // Use this for initialization
    void Start() {

        if (classType.ToLower().Equals("berserker"))
        {
			abilities = abilityList.getBerserkerAbilities();

        }
		else abilities = abilityList.getGunslingerAbilities();

		string uitext = string.Format (abiltyText, abilities[0].name, abilities[1].name, abilities[2].name, abilities[3].name);
		txt.text = uitext;
}

    public void useAbility(bool a1, bool a2, bool a3, bool a4) {
		/*Debug.Log (a1);
		Debug.Log (abilities[0].effect);*/
        if (a1 && abilities[0].IsAvailible) {
            Debug.Log("Entered");
            abilities[0].IsAvailible = false;
            abilities[0].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[0].effectDistance;
			abilities [0].applyEffect ();
            if (abilities[0].effect != null)
                abilities[0].effect.Emit(10);
        }

        if (a2 && abilities[1].IsAvailible)
        {
            Debug.Log("Entered");
            abilities[1].IsAvailible = false;
            abilities[1].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[1].effectDistance;
			abilities [1].applyEffect ();
            if (abilities[1].effect != null)
                abilities[1].effect.Emit(100);
        }

        if (a3 && abilities[2].IsAvailible) {
			Debug.Log("Entered");
            abilities[2].IsAvailible = false;
            abilities[2].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[2].effectDistance;
			abilities [2].applyEffect ();
            if(abilities[2].effect != null)
                abilities[2].effect.Emit(10);
		}

		if (a4 && abilities[3].IsAvailible) {
			Debug.Log("Entered");
            abilities[3].IsAvailible = false;
            abilities[3].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[3].effectDistance;
			abilities [3].applyEffect ();
            if(abilities[3].effect != null)
                abilities[3].effect.Emit(100);
		}
    }

	public void HardKick(){
		abilities[1].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[1].effectDistance;
		abilities[1].effect.Emit(100);
	}

    // applies the proper layer of ability animations in animator
    public void setClassAbilities(Animator anim)
    {
        if (classType.ToLower().Equals("berserker"))
        {
            anim.SetLayerWeight(4, 1);
        }
        else
        {
            anim.SetLayerWeight(3, 1);
        }
    }
}
