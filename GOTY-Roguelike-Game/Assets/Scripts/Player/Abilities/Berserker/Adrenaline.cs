using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : Ability {
    
    public float damageMultiplier = 2f;
    public float duration = 5f;

    public override void applyEffect(GameObject player)
    {
        player.GetComponent<PlayerInventory>().getCurrentWeapon().GetComponent<WeaponData>().ApplyBonusDamage(duration, damageMultiplier);
    }
}
