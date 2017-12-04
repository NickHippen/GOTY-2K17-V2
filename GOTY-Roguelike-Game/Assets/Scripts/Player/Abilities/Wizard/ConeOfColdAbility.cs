using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfColdAbility : Ability {
    
    public float effectSize;
    public float freezeDuration = 3f;
    public float coneDuration = 3f;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        GameObject cone = this.transform.GetChild(0).gameObject;
        cone.SetActive(true);
        cone.GetComponent<ConeOfCold>().FreezeDuration = freezeDuration;
        cone.transform.position = player.transform.position + player.transform.up * 1.2f;
        cone.transform.rotation = player.transform.rotation;
        StartCoroutine(StopCone(cone));
    }
    IEnumerator StopCone(GameObject cone)
    {
        yield return new WaitForSeconds(coneDuration);
        cone.SetActive(false);
    }
}
