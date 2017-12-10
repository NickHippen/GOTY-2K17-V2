using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffData : WeaponData {

    public float projectileDuration;
    public float projectileSpeed = 3000f;
    public float radius;
    public Vector2 effectPosition = new Vector2(0, 2);
    StaffProjectile staffProjectile;

	protected override void Start() {
		base.Start();
        staffProjectile = this.transform.GetChild(0).GetComponent<StaffProjectile>();
        
    }

	public override void Attack()
    {
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
        StaffProjectile proj = Instantiate(staffProjectile).GetComponent<StaffProjectile>();
        proj.Timer = projectileDuration;
        proj.Damage = damage;
        proj.Radius = radius;
        proj.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        proj.DamageMultiplier = damageMultiplier;
        proj.StaffObject = this;
        proj.Emotion = emotion;
        proj.Modifier = modifier;
        proj.gameObject.SetActive(true);
		proj.Player = Player;
        proj.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * projectileSpeed);


        //foreach (Collider collider in colliders)
        //{
        //    RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
        //    if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
        //    {
        //        AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
        //        float damage = this.damage;
        //        damage *= damageMultiplier;
        //        damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
        //        damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
        //        monster.Damage(damage);
        //    }
        //}
    }
}
