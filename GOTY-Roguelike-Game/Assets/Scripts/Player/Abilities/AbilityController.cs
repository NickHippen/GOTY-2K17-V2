using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public AbilityPrefabList abilityList;
    public string classType;
    public Text uiText;
    public Text cdText;
    string abiltyText = "Z: {0} X: {1} C: {2} V: {3}";
    string cooldownText = "Z: {0:0.00} X: {1:0.00} C: {2:0.00} V: {3:0.00}";

    private List<Ability> abilities;

    // Use this for initialization
    void Start() {

        if (classType.ToLower().Equals("berserker"))
        {
			abilities = abilityList.getBerserkerAbilities(gameObject.transform.parent);

        }
		else abilities = abilityList.getGunslingerAbilities(gameObject.transform.parent);

		string uitext = string.Format (abiltyText, abilities[0].name, abilities[1].name, abilities[2].name, abilities[3].name);
		uiText.text = uitext;
    }

    private void Update()
    {
        string cdtext = string.Format(cooldownText, abilities[0].CooldownTimer, abilities[1].CooldownTimer, abilities[2].CooldownTimer, abilities[3].CooldownTimer);
        cdText.text = cdtext;
    }

    // returns whether or not the ability is used
    public bool useAbility(int abilityIndex) {
        // ability is not availible
        if (!abilities[abilityIndex].IsAvailible)
            return false;
        // else use the ability
        abilities[abilityIndex].IsAvailible = false;

        // if the animation will by applied during animation, then skip
        if (!abilities[abilityIndex].ApplyOnFrame)
        {
            abilities[abilityIndex].applyEffect(gameObject);
        }
        return true;
    }

	public void applyEffectOnFrame(int abilityIndex){
        abilities[abilityIndex].applyEffect(gameObject);
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
