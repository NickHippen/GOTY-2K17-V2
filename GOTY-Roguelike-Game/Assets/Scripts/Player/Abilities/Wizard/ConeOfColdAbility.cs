using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfColdAbility : Ability {

    public float damage;
    //public ParticleSystem particleEffect;
    public Vector2 effectPosition;
    public float damageRadius;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;

        //// change particleEffect size for to match damage radius
        //ParticleSystem[] pSystems = particleEffect.GetComponentsInChildren<ParticleSystem>();

        //// took the time to adjust the values in particle effect to scale with damage
        //ParticleSystem.MainModule mainEffect = pSystems[0].main;
        //mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
        //mainEffect = pSystems[1].main;
        //mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

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
}
