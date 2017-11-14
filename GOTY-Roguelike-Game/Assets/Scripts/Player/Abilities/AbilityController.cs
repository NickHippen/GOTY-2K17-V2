using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public AbilityPrefabList abilityList;
    public string classType;
    public Text txt;
    string abiltyText = "Z: {0} X: {1} C: {2} V: {3}";

    private List<AbilityData> abilities;
    private Transform playerTransform;

    // Use this for initialization
    void Start() {
		if (txt != null) {
			txt = GameObject.Find ("UIText").GetComponent<Text> ();

			string uitext = string.Format (abiltyText, abilities[0].name, abilities[1].name, "", "");
			txt.text = uitext;
		}
        if (classType.ToLower().Equals("berserker"))
        {
            abilities = abilityList.getBerserkerAbilities();
        }
        else abilities = abilityList.getGunslingerAbilities();

    //    for (int x = 0; x < abilities.Count; x++)
    //    {
    //        //    abilities[x] = Instantiate(abilities[x]);
    //        abilities[x].transform.SetParent(gameObject.transform);
    //}
}

    public void useAbility(bool a1, bool a2, bool a3, bool a4) {
        if (a1 && abilities[0].effect != null) {
            Debug.Log("Entered");
            abilities[0].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[0].effectDistance;
            abilities[0].effect.Emit(10);
        }

		if (a2 && abilities[1].effect != null) {
			Debug.Log("Entered");
            abilities[1].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[1].effectDistance;
            abilities[1].effect.Emit(100);
		}

		if (a1 && abilities[2].effect != null) {
			Debug.Log("Entered");
            abilities[0].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[0].effectDistance;
            abilities[0].effect.Emit(10);
		}

		if (a2 && abilities[3].effect != null) {
			Debug.Log("Entered");

            abilities[1].transform.position = gameObject.transform.position + gameObject.transform.forward * abilities[1].effectDistance;
            abilities[1].effect.Emit(100);
		}
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
            anim.SetLayerWeight(5, 1);
        }
    }
}
