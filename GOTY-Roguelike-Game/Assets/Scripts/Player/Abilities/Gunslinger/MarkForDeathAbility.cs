using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkForDeathAbility : Ability
{
    public ParticleSystem particleEffect;
    public float particleSize = 1f;
    public float missDuration = 1f;
    public float markDuration = 20f;
    public float damageMultiplier = 2f;
    public float range = 100f;
    public float bonusCdReduction =  5f;
    //public float bonusEffectRadius = 2f;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData> ();
        // instantiates a camera attached to particle affect and puts it on Main Camera so it will always render the particle effect
        Instantiate(this.transform.GetChild(1), Camera.main.transform, false).gameObject.SetActive(true);
        ParticleSystem.MainModule mainSystem = particleEffect.main;
        mainSystem.startSize = particleSize;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        if (bonusEffect) newCooldownTime(cooldownTime += bonusCdReduction);

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Unwalkable", "Monster", "Ground");
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, range, layerMask))
        {
            particleEffect.transform.position = hit.point;

            List<GameObject> mark = new List<GameObject>();
            mark.Add(Instantiate(particleEffect.transform.gameObject, hit.transform, true));
            mark[0].gameObject.SetActive(true);

            RigCollider rigCollider = hit.transform.GetComponent<RigCollider>();
            if (rigCollider == null)
            {
                StartCoroutine(RemoveParticle(missDuration, mark));
                return;
            }

            Unit unit = rigCollider.RootUnit;
            if (unit is AggressiveUnit)
            {
                AggressiveUnit monster = (AggressiveUnit)unit;
                monster.ApplyStatus(new StatusVulnerable(monster, markDuration, damageMultiplier));

                // attempt at aoe Mark
                //if(!bonusEffect)
                //{
                //    Collider[] colliders = Physics.OverlapSphere(hit.point, bonusEffectRadius);
                //    foreach (Collider collider in colliders)
                //    {
                //        RigCollider areaCollider = collider.gameObject.GetComponent<RigCollider>();
                //        if (areaCollider != null && areaCollider.RootUnit is AggressiveUnit)
                //        {
                //            AggressiveUnit areaMonster = ((AggressiveUnit) areaCollider.RootUnit);
                //            if (bonusEffect)
                //            {
                //                mark.Add(Instantiate(mark[0].transform.gameObject, areaMonster.transform, true));
                //                areaMonster.ApplyStatus(new StatusVulnerable(areaMonster, markDuration, damageMultiplier));
                //            }
                //        }
                //    }
                //}
                StartCoroutine(RemoveParticle(markDuration, mark));
            }
            else
            {
                StartCoroutine(RemoveParticle(missDuration, mark));
            }
        }
    }

    private IEnumerator RemoveParticle(float timer, List<GameObject> mark)
    {
        yield return new WaitForSeconds(timer);
        foreach(GameObject m in mark)
        {
            Destroy(m);
        }
    }
}
