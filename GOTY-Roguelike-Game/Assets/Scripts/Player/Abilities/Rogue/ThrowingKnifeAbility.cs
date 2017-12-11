using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeAbility : Ability {

    public float damage;
    public float despawnTimer = 6f;
    public float throwForce = 5000f;
    public float collisionOffset = 0.02f;
    public float PoisonDamage;
    public float PoisonDuration;
    public float PoisonTicksPerSecond;
    public float bonusVulnDuration;
    public float bonusVulnMulitplier;
    public Vector2 effectPosition;

    ThrowingKnife throwingKnife;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData> ();
        throwingKnife = transform.GetChild(0).gameObject.GetComponent<ThrowingKnife>();
    }

    public override void applyEffect(GameObject player)
    {
        print(throwingKnife);
        base.applyEffect(player);
        ThrowingKnife knife = Instantiate(throwingKnife);
        knife.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        knife.transform.rotation = player.transform.rotation;
        knife.transform.Rotate(0f, 270f, 0f);
        knife.Timer = despawnTimer;
        knife.Damage = damage;
        knife.CollisionOffset = collisionOffset;
        knife.gameObject.SetActive(true);
        knife.BonusEffect = bonusEffect;
        knife.PoisonDamage = PoisonDamage;
        knife.PoisonDuration = PoisonDuration;
        knife.PoisonTPS = PoisonTicksPerSecond;
        knife.BonusDuration = bonusVulnDuration;
        knife.BonusMultiplier = bonusVulnMulitplier;
		knife.Player = player;
        knife.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * throwForce);
    }
}
