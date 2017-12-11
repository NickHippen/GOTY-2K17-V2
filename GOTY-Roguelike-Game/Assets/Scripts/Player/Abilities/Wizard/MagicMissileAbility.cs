using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissileAbility : Ability {
    
    public float damage;
    public float damageRadius = 1f;
    public float missileRadius = 1f;
    public float particleScale = 1f;
    public float explosionTimer = 6f;
    public float throwForce = 2000f;
    public float bonusDamage = 2f;
    public float bonusDuration = 15f;
    public float bonusTicksPerSecond = 2;
    public Vector2 effectPosition = new Vector2(1.3f, 1.5f);

    MagicMissile magicMissileObject;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData> ();
        magicMissileObject = transform.GetChild(0).gameObject.GetComponent<MagicMissile>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        MagicMissile missile = Instantiate(magicMissileObject);
        missile.Timer = explosionTimer;
        missile.Damage = damage;
        missile.DamageRadius = damageRadius;
        missile.MissileRadius = missileRadius;
        missile.ParticleRadius = particleScale;
        missile.BonusDamage = bonusDamage;
        missile.BonusDuration = bonusDuration;
        missile.BonusTPS = bonusTicksPerSecond;
        missile.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        missile.gameObject.SetActive(true);
		missile.Player = player;
        missile.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * throwForce);
    }
}
