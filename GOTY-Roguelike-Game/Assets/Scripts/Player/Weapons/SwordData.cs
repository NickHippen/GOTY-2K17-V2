using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData
{
    // commented code was not working properly
    //public float hitDuration = 0.1f;
    //private bool hit;

    //public override void Attack()
    //{
    //    hit = true;
    //    StartCoroutine(HitDuration());
    //}

    void OnTriggerEnter(Collider collision) {
        //if (hit) {
            RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit) {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                damage *= damageMultiplier;
                damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
                damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
                monster.Damage(damage);
            }
        //}
	}

    //IEnumerator HitDuration()
    //{
    //    yield return new WaitForSeconds(hitDuration);
    //    hit = false;
    //}
}
