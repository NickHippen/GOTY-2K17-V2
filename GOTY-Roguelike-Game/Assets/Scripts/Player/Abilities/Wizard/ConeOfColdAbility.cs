using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfColdAbility : Ability {
    
    public float effectSize;
    public float freezeDuration = 3f;
    public float coneDuration = 3f;
    public float bonusDuration;
    public float bonusSlowPercent;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData> ();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        ConeOfCold cone = this.transform.GetChild(0).GetComponent<ConeOfCold>();
        cone.gameObject.SetActive(true);
        cone.gameObject.transform.position = player.transform.position + player.transform.up * 1.2f;
        cone.gameObject.transform.rotation = player.transform.rotation;
        cone.FreezeDuration = freezeDuration;
        cone.BonusEffect = bonusEffect;
        cone.BonusDuration = bonusDuration;
        cone.BonusSlowPercent = bonusSlowPercent;
        StartCoroutine(StopCone(cone));
    }
    IEnumerator StopCone(ConeOfCold cone)
    {
        yield return new WaitForSeconds(coneDuration);
        cone.gameObject.SetActive(false);
    }
}
