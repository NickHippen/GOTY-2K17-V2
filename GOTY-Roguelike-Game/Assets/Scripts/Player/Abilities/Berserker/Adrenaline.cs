using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : Ability {
    
    public override void applyEffect(GameObject player)
    {
		StartCoroutine (adrenalineRush (player));
    }

	private IEnumerator adrenalineRush(GameObject player){
		PlayerInventory i = player.GetComponent<PlayerInventory> ();
		i.multiplier = 2;
		for (int x = 0; x < player.GetComponent<PlayerInventory> ().weapons.Count; x++) {
			i.weapons [x].GetComponent<WeaponData> ().damage = i.weapons [x].GetComponent<WeaponData> ().baseDamage * i.multiplier;
		}
		yield return new WaitForSeconds (20);
		i.multiplier = 1;

		for (int x = 0; x < player.GetComponent<PlayerInventory> ().weapons.Count; x++) {
			i.weapons [x].GetComponent<WeaponData> ().damage = i.weapons [x].GetComponent<WeaponData> ().baseDamage * i.multiplier;
		}
	}
}
