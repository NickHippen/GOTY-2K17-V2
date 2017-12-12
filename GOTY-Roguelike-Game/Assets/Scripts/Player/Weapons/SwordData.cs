using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData
{
    public float swordRange = 2f;
    public float swordRadius = 2.5f;
    
    public override void Attack()
    {
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
		Collider[] colliders = Physics.OverlapCapsule(player.transform.position + player.transform.forward * swordRange,
			player.transform.position + player.transform.up*2f + player.transform.forward * swordRange, swordRadius);

		PlayAttackAudio (0);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
            {
				PlayAttackAudio (1);
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                damage *= damageMultiplier;
                damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
                damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
                monster.Damage(damage, Player.transform);
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
