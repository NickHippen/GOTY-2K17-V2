using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    ParticleSystem missileParticle;
    ParticleSystem explosionParticle;
    bool exploded;

    public float Damage { get; set; }
    public float DamageRadius { get; set; }
    public float MissileRadius { get; set; }
    public float ParticleRadius { get; set; }
    public float Timer { get; set; }
    public bool BonusEffect { get; set; }
    public float BonusDamage { get; set; }
    public float BonusDuration { get; set; }
    public float BonusTPS { get; set; }
    public GameObject Player { get; set; }

    private void Start()
    {
        missileParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        explosionParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
        StartCoroutine(Active());


        // change particleEffect size for to match object radiuses
        ParticleSystem[] pSystems = missileParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pSys in pSystems)
        {
            ParticleSystem.MainModule mainEffect = pSys.main;
            mainEffect.startSize = MissileRadius * mainEffect.startSize.constant;
        }

        pSystems = explosionParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pSys in pSystems)
        {
            ParticleSystem.MainModule mainEffect = pSys.main;
            mainEffect.startSize = DamageRadius * mainEffect.startSize.constant / 2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Unwalkable") ||
            other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(Timer);
        if(!exploded) StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        exploded = true;
        missileParticle.transform.gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = new Vector3();

        explosionParticle.transform.position = this.transform.position;
        ParticleSystem.MainModule mainSystem = explosionParticle.main;
        mainSystem.startSize = ParticleRadius;
        explosionParticle.gameObject.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, DamageRadius);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.Damage;
                monster.Damage(damage, Player.transform);
                if(BonusEffect) monster.ApplyStatus(new StatusPoison(monster, BonusDuration, BonusDamage, BonusTPS));
            }
        }

        yield return new WaitWhile(() => explosionParticle.IsAlive(true));
        Destroy(this.gameObject);
    }
}
