using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissileAbility : Ability {

    public MagicMissile magicMissileObject;
    public float damage;
    public float damageRadius = 1f;
    public float missileRadius = 1f;
    public float particleScale = 1f;
    public float explosionTimer = 6f;
    public float throwForce = 2000f;
    public Vector2 effectPosition = new Vector2(1.3f, 1.5f);

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        MagicMissile missile = Instantiate(magicMissileObject);
        print(missile);
        missile.Timer = explosionTimer;
        missile.Damage = damage;
        missile.DamageRadius = damageRadius;
        missile.MissileRadius = missileRadius;
        missile.ParticleRadius = particleScale;
        missile.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        missile.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * throwForce);
    }
}
