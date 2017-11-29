using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public AbilityPrefabList abilityList;
    public string classType;
    public float iconSize = 50f;
    public Text abilityText;
    public Text cooldownText;
    string COOLDOWN_FORMAT = "{0:00.0}";

    private List<Ability> abilities;
    private List<Image> abilityIcons = new List<Image>();
    private List<Text> cooldownTimers = new List<Text>();

    // Use this for initialization
    void Start() {

        if (classType.ToLower().Equals("berserker"))
        {
			abilities = abilityList.getBerserkerAbilities(gameObject.transform.parent);
        }
		else abilities = abilityList.getGunslingerAbilities(gameObject.transform.parent);

        for(int i = 0; i < abilities.Count; i++)
        {
            // Find the ability frames in the UI
            GameObject abilityFrame = GameObject.Find("Ability " + i);
            abilityFrame.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(iconSize, iconSize);

            // duplicate frame and add ability icon
            GameObject abilityIcon = Instantiate(abilityFrame, abilityFrame.transform.parent, false);
            abilityIcon.transform.SetSiblingIndex(1); // place behind frame
            
            // add text to abilities
            Instantiate(abilityText, abilityFrame.transform, false).text =
                Regex.Replace(abilities[i].name.Substring(0, abilities[i].name.Length - "Ability".Length), "([a-z])_?([A-Z])", "$1 $2");
            cooldownTimers.Add(Instantiate(cooldownText, abilityFrame.transform, false));
            cooldownTimers[i].enabled = false;
            Text inputButton = Instantiate(abilityText, abilityFrame.transform, false);
            inputButton.alignment = TextAnchor.LowerCenter;
            inputButton.fontSize += 2;
            if (i == 0) inputButton.text = "Z";
            else if (i == 1) inputButton.text = "X";
            else if (i == 2) inputButton.text = "C";
            else inputButton.text = "V";

            abilityIcon.GetComponent<Image>().sprite = abilities[i].icon;
            abilityIcons.Add(abilityIcon.GetComponent<Image>());
        }
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            cooldownTimers[i].text = string.Format(COOLDOWN_FORMAT, abilities[i].CooldownTimer);
        }
    }

    // returns whether or not the ability is used
    public bool useAbility(int abilityIndex) {
        // ability is not availible
        if (!abilities[abilityIndex].IsAvailible)
            return false;
        // else use the ability
        abilities[abilityIndex].IsAvailible = false;

        abilityIcons[abilityIndex].color = new Color(1, 1, 1, 0.5f);
        cooldownTimers[abilityIndex].enabled = true;
        StartCoroutine(abilityOpacity(abilities[abilityIndex].cooldownTime, abilityIndex));

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
            anim.SetLayerWeight(3, 1);
        }
        else
        {
            anim.SetLayerWeight(2, 1);
        }
    }

    IEnumerator abilityOpacity(float duration, int index) {
        yield return new WaitForSeconds(duration);
        abilityIcons[index].color = new Color(1, 1, 1, 1f);
        cooldownTimers[index].enabled = false;
}
}
