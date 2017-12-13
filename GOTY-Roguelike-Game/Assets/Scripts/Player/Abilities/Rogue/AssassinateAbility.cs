using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinateAbility : Ability {

    public float damage;
    //Radius of damage the effect has
    public float range = 10;
    public float bonusHealPercent;
    //Distance of particle effect from player
    public Vector2 effectPosition;

    GameObject particleEffect;
    GameObject particleClone;

    protected override void Start()
    {
        base.Start();
        
        sfx = GetComponent<SoundData>();
        particleEffect = transform.GetChild(0).gameObject;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        if (!applyOnFrame)
        {
            applyOnFrame = true;
            particleClone = Instantiate(particleEffect, player.transform, false);
            particleClone.transform.position += particleClone.transform.forward * effectPosition.x + particleClone.transform.up * effectPosition.y;
            particleClone.SetActive(true);
        }
        else
        {
            applyOnFrame = false;

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Unwalkable", "Monster", "Ground");
            if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, range++, layerMask))
            {
                player.transform.position = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);

                RigCollider rigCollider = hit.transform.GetComponent<RigCollider>();
                if (rigCollider == null)
                {
                    return;
                }

                Unit unit = rigCollider.RootUnit;
                if (unit is AggressiveUnit)
                {
                    AggressiveUnit monster = (AggressiveUnit)unit;
                    monster.Damage(damage, player.transform);
                    if(bonusEffect)
                    {
                        player.GetComponent<HealthManager>().Heal(damage * bonusHealPercent);
                    }
                }
            }
            else
            {
                player.transform.position = new Vector3(player.transform.position.x + Camera.main.transform.forward.x * range,
                    player.transform.position.y, player.transform.position.z + Camera.main.transform.forward.z * range);
            }
            StartCoroutine(RemoveParticle(particleClone.GetComponent<ParticleSystem>()));
        }
    }

    private IEnumerator RemoveParticle(ParticleSystem particle)
    {
        yield return new WaitWhile(() => particle.IsAlive(true));
        Destroy(particle.gameObject);
    }
}
