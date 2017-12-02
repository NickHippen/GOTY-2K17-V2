using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData
{
    public float swordRange = 1f;
    public float swordBoxRadius = 1f;
    
    public override void Attack()
    {
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
        Collider[] colliders = Physics.OverlapBox(player.transform.position + player.transform.up + player.transform.forward*swordRange,
            new Vector3(swordBoxRadius,swordBoxRadius,swordBoxRadius), player.transform.rotation);

		PlayAttackAudio (0);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                damage *= damageMultiplier;
                damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
                damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
                monster.Damage(damage);
            }
        }
    }

 //   void OnTriggerEnter(Collider collision) {
 //       RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
 //       if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit) {
 //           AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
 //           float damage = this.damage;
 //           damage *= damageMultiplier;
 //           damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
 //           damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
 //           monster.Damage(damage);
 //       }
	//}
}
