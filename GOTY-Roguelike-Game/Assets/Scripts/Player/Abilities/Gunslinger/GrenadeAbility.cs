using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAbility : Ability
{
    public float damage;
    public float damageRadius = 1f;
    public float particleScale = 5f;
    public float explosionTimer = 3f;
    public float throwForce = 3000f;
    public Vector2 throwAngle = new Vector2(2f, 1f);
    public float bonusDuration = 3f;

    Grenade grenadeObject;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
        grenadeObject = this.transform.GetChild(0).gameObject.GetComponent<Grenade>();
		sfx = GetComponent<SoundData> ();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        Grenade grenade = Instantiate(grenadeObject);
        grenade.Timer = explosionTimer;
        grenade.Damage = damage;
        grenade.DamageRadius = damageRadius;
        grenade.ParticleRadius = particleScale;
        grenade.transform.position = player.transform.position + player.transform.up * 2;
        grenade.BonusEffect = bonusEffect;
        grenade.BonusDuration = bonusDuration;
        grenade.gameObject.SetActive(true);
		grenade.Player = player;
        grenade.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward * throwAngle.x + player.transform.up * throwAngle.y) * throwForce);
        grenade.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -20)); // spin grenade on throw
    }
}
