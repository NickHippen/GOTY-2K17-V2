using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : Ability {
    
    public float damageMultiplier = 2f;
    public float duration = 5f;
	public ParticleSystem effect;
	public float effectDistance;

	void Start(){
		//ApplyOnFrame = true;
	}

    public override void applyEffect(GameObject player)
    {
		this.transform.position = player.transform.position + player.transform.forward * effectDistance;

        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }

		effect.Emit (50);
    }
}
