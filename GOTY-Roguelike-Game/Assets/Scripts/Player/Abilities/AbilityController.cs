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
    public float feedbackTimer = 0.1f;

    string COOLDOWN_FORMAT = "{0:00.0}";
    List<Ability> abilities;
    List<Image> abilityIcons = new List<Image>();
    List<Text> cooldownTimers = new List<Text>();
    Animator anim;
    int layerNum;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();

        if (classType.ToLower().Equals("berserker"))
        {
            layerNum = 6;
            anim.SetLayerWeight(layerNum, 1);
            abilities = abilityList.getBerserkerAbilities(gameObject.transform.parent);
        }
        else if (classType.ToLower().Equals("wizard"))
        {
            layerNum = 7;
            anim.SetLayerWeight(layerNum, 1);
            abilities = abilityList.getWizardAbilities(gameObject.transform.parent);
        }
        else if(classType.ToLower().Equals("rogue"))
        {
            layerNum = 8;
            anim.SetLayerWeight(layerNum, 1);
            abilities = abilityList.getRogueAbilities(gameObject.transform.parent);
        }
        else
        {
            layerNum = 5;
            anim.SetLayerWeight(layerNum, 1);
            abilities = abilityList.getGunslingerAbilities(gameObject.transform.parent);
        }

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

    public List<Ability> getAbilityList()
    {
        return abilities;
    }

    // returns whether or not the ability is used
    public bool useAbility(int abilityIndex) {

        if (!abilities[abilityIndex].IsAvailible)
        {   // ability is not availible
            return false;
        }
        if (!anim.GetCurrentAnimatorStateInfo(layerNum).IsName("Grounded"))
        {   // if another ability is active
            StartCoroutine(abilityOpacity(false, abilityIndex));
            return false;
        }
        // else use the ability
        abilities[abilityIndex].IsAvailible = false;
        cooldownTimers[abilityIndex].enabled = true;
        StartCoroutine(abilityOpacity(true, abilityIndex));

        // if the animation will by applied during animation, then skip
        if (!abilities[abilityIndex].ApplyOnFrame)
        {
            abilities[abilityIndex].applyEffect(gameObject);
        }
        return true;
    }

    // called by animation frame functions
	public void applyEffectOnFrame(int abilityIndex){
        abilities[abilityIndex].applyEffect(gameObject);
    }

    IEnumerator abilityOpacity(bool useAbility, int index)
    {
        abilityIcons[index].color = new Color(1, 1, 1, 0.5f);
        if(useAbility) yield return new WaitUntil(() => abilities[index].IsAvailible);
        else yield return new WaitForSeconds(feedbackTimer);
        abilityIcons[index].color = new Color(1, 1, 1, 1f);
        cooldownTimers[index].enabled = false;
    }
}
