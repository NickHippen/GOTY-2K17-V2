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
    public float range = 40f;
    public float playerHeight = 2f;
    
     Vector3 shootPoint;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
        // instantiates a camera attached to particle affect and puts it on Main Camera so it will always render the particle effect
        Instantiate(this.transform.GetChild(1), Camera.main.transform, false).gameObject.SetActive(true);
        ParticleSystem.MainModule mainSystem = particleEffect.main;
        mainSystem.startSize = particleSize;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        player.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        shootPoint = new Vector3(0, playerHeight,0) + player.transform.position;

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Unwalkable", "Monster"); // what about floor?
        if (Physics.Raycast(shootPoint, Camera.main.transform.forward, out hit, range, layerMask))
        {
            particleEffect.transform.position = hit.point;
            
            GameObject mark = Instantiate(particleEffect.transform.gameObject, hit.transform, true);
            mark.gameObject.SetActive(true);

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
                StartCoroutine(RemoveParticle(markDuration, mark));
            }
            else
            {
                StartCoroutine(RemoveParticle(missDuration, mark));
            }
        }
    }

    private IEnumerator RemoveParticle(float timer, GameObject mark)
    {
        yield return new WaitForSeconds(timer);
        Destroy(mark);
    }
}
