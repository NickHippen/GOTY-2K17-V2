using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAbility : Ability
{
    public Grenade grenadeObject;
    public float damage;
    public float damageRadius = 1f;
    public float explosionTimer = 3f;
    public float throwForce = 3000f;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        Grenade grenade = Instantiate(grenadeObject);
        grenade.Timer = explosionTimer;
        grenade.Damage = damage;
        grenade.DamageRadius = damage;
        grenade.transform.position = player.transform.position + player.transform.up * 2;
        grenade.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce);
    }
}
