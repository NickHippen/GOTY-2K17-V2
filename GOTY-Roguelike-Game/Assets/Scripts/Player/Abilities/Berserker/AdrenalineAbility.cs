using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineAbility : Ability {
    
    public float damageMultiplier = 2f;
    public float duration;
    public float bonusCdMultiplier;

    ParticleSystem particleEffect;

	protected override void Start(){
        base.Start();
		sfx = GetComponent<SoundData> ();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleEffect.Stop();
	}

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		this.transform.position = player.transform.position;
		this.transform.parent = player.transform;

        if(bonusEffect)
        {
            List<Ability> abilities = player.GetComponent<AbilityController>().getAbilityList();
            foreach (Ability ability in abilities)
            {
                if (ability is AdrenalineAbility) continue;
                ability.CooldownMultiplier += bonusCdMultiplier;
            }
            StartCoroutine(StopBonus(abilities));
        }

        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }

		StartCoroutine (effectTimer ());
		//effect.Emit (50);
    }

	IEnumerator effectTimer(){
		particleEffect.Play ();
		//sfx.playSound ();
		sfx.playLoop ();
		yield return new WaitForSeconds (duration);
		particleEffect.Stop ();
		sfx.stopLoop();
	}

    IEnumerator StopBonus(List<Ability> abilities)
    {
        sfx.playLoop();
        yield return new WaitForSeconds(duration);
        foreach (Ability ability in abilities)
        {
            if (ability is AdrenalineAbility) continue;
            ability.CooldownMultiplier -= bonusCdMultiplier;
        }
    }
}
