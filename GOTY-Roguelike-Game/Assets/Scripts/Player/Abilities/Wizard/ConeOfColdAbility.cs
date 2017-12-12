using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfColdAbility : Ability {
    
    public float freezeDuration = 3f;
    public float coneDuration = 3f;
    public float bonusDuration;
    public float bonusSlowPercent;
    public Vector2 effectPosition = new Vector2(0.175f, 1.25f);

    GameObject coneOfCold;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
        coneOfCold = this.transform.GetChild(0).gameObject;
        sfx = GetComponent<SoundData> ();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        ConeOfCold cone = Instantiate(coneOfCold).GetComponent<ConeOfCold>();
        cone.gameObject.transform.position = player.transform.position + player.transform.up * effectPosition.y;
        cone.gameObject.transform.rotation = player.transform.rotation;
        cone.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, effectPosition.x);
        cone.gameObject.SetActive(true);
        cone.FreezeDuration = freezeDuration;
        cone.BonusEffect = bonusEffect;
        cone.BonusDuration = bonusDuration;
        cone.BonusSlowPercent = bonusSlowPercent;
        StartCoroutine(StopCone(cone));
    }
    IEnumerator StopCone(ConeOfCold cone)
    {
        yield return new WaitForSeconds(coneDuration);
        cone.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        yield return new WaitWhile(() => cone.transform.GetChild(0).GetComponent<ParticleSystem>().IsAlive());
        Destroy(cone.gameObject);
    }
}
