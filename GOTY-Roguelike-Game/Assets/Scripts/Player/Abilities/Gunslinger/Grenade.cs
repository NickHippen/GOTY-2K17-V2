using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Ability
{
    public GrenadeObject grenadeObject;
    public float damage;
    public float damageRadius = 1f;
    public float explosionTimer = 3f;
    public float throwForce = 3000f;

    public override void applyEffect(GameObject player)
    {
        GrenadeObject grenade = Instantiate(grenadeObject);
        grenade.Timer = explosionTimer;
        grenade.Damage = damage;
        grenade.DamageRadius = damage;
        grenade.transform.position = player.transform.position + player.transform.up * 2;
        grenade.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce);
    }
}
