using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinateAbility : Ability {

    public float damage;
    //Distance of particle effect from player
    public Vector2 effectPosition;
    //Radius of damage the effect has
    public float range = 10;

    GameObject particleEffect;
    GameObject tempParticle;
    Vector3 shootPoint;

    protected override void Start()
    {
        base.Start();
        
        sfx = GetComponent<SoundData>();
        particleEffect = transform.GetChild(0).gameObject;
        //ParticleSystem.MainModule mainEffect = particleEffect.main;
        //mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
        //mainEffect = particleEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        //mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        if (!applyOnFrame)
        {
            applyOnFrame = true;
            tempParticle = Instantiate(particleEffect, player.transform, false);
            tempParticle.transform.position += tempParticle.transform.forward * effectPosition.x + tempParticle.transform.up * effectPosition.y;
            tempParticle.SetActive(true);
        }
        else
        {
            applyOnFrame = false;
            shootPoint = new Vector3(0, effectPosition.y, 0) + player.transform.position;

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Unwalkable", "Monster", "Ground");
            if (Physics.Raycast(shootPoint, Camera.main.transform.forward, out hit, range, layerMask))
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
                    monster.Damage(damage);
                }
            }
            else
            {
                player.transform.position = new Vector3(player.transform.position.x + Camera.main.transform.forward.x * range,
                    player.transform.position.y, player.transform.position.z + Camera.main.transform.forward.z * range);
            }
            StartCoroutine(RemoveParticle(tempParticle.GetComponent<ParticleSystem>()));
        }
    }

    private IEnumerator RemoveParticle(ParticleSystem particle)
    {
        yield return new WaitWhile(() => particle.IsAlive(true));
        Destroy(particle.gameObject);
    }
}
