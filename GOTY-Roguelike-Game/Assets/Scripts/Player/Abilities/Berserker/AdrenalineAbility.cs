using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineAbility : Ability {
    
    public float damageMultiplier = 2f;
    public float duration;

    ParticleSystem particleEffect;
    Ability hardKick;
    bool isActive;

	protected override void Start(){
        base.Start();
		sfx = GetComponent<SoundData> ();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleEffect.Stop();
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
        this.transform.position = player.transform.position;
		this.transform.parent = player.transform;
        isActive = true;

        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }

		StartCoroutine (effectTimer ());
    }

	IEnumerator effectTimer(){
		particleEffect.Play ();
		//sfx.playSound ();
		sfx.playLoop ();
		yield return new WaitForSeconds (duration);
        isActive = false;
		particleEffect.Stop ();
		sfx.stopLoop();
	}
}
