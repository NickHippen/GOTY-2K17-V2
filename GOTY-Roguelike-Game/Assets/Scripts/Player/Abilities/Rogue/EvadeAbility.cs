using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeAbility : Ability {

    public float bonusHealAmount;
    public float bonusHealRadius = 20f;
    public Vector2 effectPosition = new Vector2(0, 1.25f);
    GameObject particleEffect;

    protected override void Start()
    {
        base.Start();
        particleEffect = this.transform.GetChild(0).gameObject;
		sfx = GetComponent<SoundData>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        
        if (bonusEffect)
        {
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, bonusHealRadius);
            foreach (Collider collider in colliders)
            {
                RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
                Debug.Log(collider);
                if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
                {
                    AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                    if (bonusEffect)
                    {
                        player.GetComponent<HealthManager>().Heal(bonusHealAmount);
                        break;
                    }
                }
            }
        }
        GameObject activeParticle = Instantiate(particleEffect, player.transform, false);
        activeParticle.transform.position += player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        activeParticle.gameObject.SetActive(true);
        StartCoroutine(RemoveParticle(activeParticle));
    }

    IEnumerator RemoveParticle(GameObject particle)
    {
        yield return new WaitWhile(() => particle.GetComponent<ParticleSystem>().IsAlive());
        Destroy(particle);
    }
		
}
