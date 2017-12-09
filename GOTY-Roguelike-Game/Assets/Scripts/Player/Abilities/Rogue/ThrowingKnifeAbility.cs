using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeAbility : Ability {

    public float damage;
    public Vector2 effectPosition;
    public float despawnTimer = 6f;
    public float throwForce = 5000f;

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
        base.applyEffect(player);
        ThrowingKnife knife = Instantiate(throwingKnife);
        knife.Timer = despawnTimer;
        knife.Damage = damage;
        knife.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        knife.gameObject.SetActive(true);
        knife.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * throwForce);
    }
}
