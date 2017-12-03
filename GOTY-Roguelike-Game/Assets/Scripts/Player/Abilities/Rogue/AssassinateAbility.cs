using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinateAbility : Ability {

    public float damage;
    //Distance of particle effect from player
    public Vector2 effectPosition;
    //Radius of damage the effect has
    public float damageRadius;

    ParticleSystem particleEffect;

    //protected override void Start()
    //{
    //    base.Start();
    //    applyOnFrame = true;
    //    sfx = GetComponent<SoundData>();
    //    particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
    //    ParticleSystem.MainModule mainEffect = particleEffect.main;
    //    mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
    //    mainEffect = particleEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main;
    //    mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
    //}

    //public override void applyEffect(GameObject player)
    //{
    //    base.applyEffect(player);

    //    this.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;

    //    Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
    //    foreach (Collider collider in colliders)
    //    {
    //        RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
    //        Debug.Log(collider);
    //        if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
    //        {
    //            AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
    //            float damage = this.damage;
    //            monster.Damage(damage);
    //        }
    //    }
    //    sfx.playSound();
    //    particleEffect.Play();
    //}
}
