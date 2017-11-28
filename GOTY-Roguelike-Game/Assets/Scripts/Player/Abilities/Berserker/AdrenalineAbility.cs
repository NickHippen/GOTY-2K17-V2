using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineAbility : Ability {
    
    public float damageMultiplier = 2f;
    public float duration;
	public ParticleSystem effect;
	public float effectDistance;

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
		effect.Play ();
		yield return new WaitForSeconds (duration);
		effect.Stop ();
	}
}
