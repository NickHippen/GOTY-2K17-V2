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
    public Vector2 throwPosition = new Vector2(2.1f, 1.5f);
    public float raycastRange = 100f;

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
        knife.transform.position = player.transform.position + player.transform.forward * throwPosition.x + player.transform.up * throwPosition.y;
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
        knife.gameObject.SetActive(true);

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Unwalkable", "Monster", "Ground");
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, raycastRange, layerMask))
        {
            knife.transform.LookAt(hit.point);
            Vector3 throwPointToHitPoint = hit.point - knife.transform.position;
            //knife.GetComponent<Rigidbody>().AddForce(throwPointToHitPoint.normalized * throwForce);
        }
        else
        {

            knife.transform.rotation = player.transform.rotation;
            //.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * throwForce);
        }
    }
}
