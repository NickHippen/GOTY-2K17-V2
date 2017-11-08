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
        txt = GameObject.Find("UIText").GetComponent<Text>();

        //abilities[0] = Instantiate(abilities[0]);
        //abilities[0].transform.SetParent(gameObject.transform);
        //print(abilities[0].effect);

        if (classType.ToLower().Equals("berserker"))
        {
            setBerserkerData();
        }
        else
        {
            setGunslingerData();
        }

        string uitext = string.Format(abiltyText, abilities[0].name, "", "", "");
        txt.text = uitext;

        //abilities [0].transform.position = gameObject.transform.position;
    }

    public void useAbility(bool a1, bool a2, bool a3, bool a4) {
        if (a1 && abilities[0].effect != null) {
            Debug.Log("Entered");
            abilities[0].effect.Emit(10);
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

    private void setBerserkerData() {
        abilities.Add(new Cyclone(10f, new ParticleSystem(), "Cyclone"));
        abilities.Add(new HardKick(10f, new ParticleSystem(), "Hard Kick"));
        abilities.Add(new TankUp(10f, new ParticleSystem(), "Tank Up"));
        abilities.Add(new Adrenaline(10f, new ParticleSystem(), "Adrenaline"));
    }

    private void setGunslingerData()
    {
        abilities.Add(new Grenade(10f, new ParticleSystem(), "Grenade"));
        abilities.Add(new Grenade(10f, new ParticleSystem(), "Grenade"));
        abilities.Add(new Grenade(10f, new ParticleSystem(), "Grenade"));
        abilities.Add(new Grenade(10f, new ParticleSystem(), "Grenade"));
    }
}
