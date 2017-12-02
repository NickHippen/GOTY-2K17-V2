using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineAbility : Ability {
    
    public float damageMultiplier = 2f;
    public float duration;
	public float effectDistance;

    ParticleSystem particleEffect;

	protected override void Start(){
        base.Start();
		sfx = GetComponent<SoundData> ();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
	}

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		this.transform.position = player.transform.position + player.transform.forward * effectDistance;
		this.transform.parent = player.transform;

        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }

		StartCoroutine (effectTimer ());
		//effect.Emit (50);
    }

	private IEnumerator effectTimer(){
		particleEffect.Play ();
		sfx.playSound ();
		sfx.playLoop ();
		yield return new WaitForSeconds (duration);
		particleEffect.Stop ();
		sfx.stopLoop();
	}
}
