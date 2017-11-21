using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : Ability {
    
    public float damageMultiplier = 2f;
    public float duration = 5f;

    public override void applyEffect(GameObject player)
    {
        foreach(GameObject weapon in player.GetComponent<PlayerInventory>().weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }
    }
}
