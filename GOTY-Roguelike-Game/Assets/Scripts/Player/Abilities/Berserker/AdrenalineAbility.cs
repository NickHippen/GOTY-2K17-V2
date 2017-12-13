using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineAbility : Ability {
    
    public float damageMultiplier = 2f;
    public float duration;

    GameObject portalEffect;
    GameObject energyEffect;
    private float energyHeight = 1.28f;
    Ability hardKick;
    bool isActive;

	protected override void Start(){
        base.Start();
		sfx = GetComponent<SoundData> ();
        energyEffect = transform.GetChild(0).gameObject;
        portalEffect = transform.GetChild(1).gameObject;
	}

    protected override void Update()
    {
        base.Update();
        if (bonusEffect && isActive)
        {
            hardKick.IsAvailible = true;
        }
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        hardKick = player.GetComponent<AbilityController>().getAbilityList()[1];
        isActive = true;

        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }

		StartCoroutine (effectTimer (player));
    }

	IEnumerator effectTimer(GameObject player){
        ParticleSystem energy = Instantiate(energyEffect, player.transform, false).GetComponent<ParticleSystem>();
        ParticleSystem portal = Instantiate(portalEffect, player.transform, false).GetComponent<ParticleSystem>();
        energy.transform.position += player.transform.up * energyHeight;
        energy.gameObject.SetActive(true);
        portal.gameObject.SetActive(true);
		sfx.playLoop ();
		yield return new WaitForSeconds (duration);
        energy.Stop();
        portal.Stop();
        isActive = false;
        sfx.stopLoop();
        yield return new WaitWhile(() => energy.IsAlive() || portal.IsAlive());
        Destroy(energy.gameObject);
        Destroy(portal.gameObject);
	}
}
