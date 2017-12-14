using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float Damage { get; set; }
    public float DamageRadius { get; set; }
    public float ParticleRadius { get; set; }
    public float Timer { get; set; }
    public bool BonusEffect { get; set; }
    public float BonusDuration { get; set; }
	public GameObject Player { get; set; }

    ParticleSystem particleExplosion;
    ParticleSystem particleSparks;
    bool exploded;

    private void Start()
    {
        particleExplosion = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSparks = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        StartCoroutine(Active());
    }

    private void FixedUpdate()
    {
        int layerMask = LayerMask.GetMask("Monster");
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.GetComponent<SphereCollider>().radius, layerMask);
        if(colliders.Length > 0 && !exploded)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(Timer);
        if (!exploded) StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        exploded = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        particleSparks.Stop();
        GetComponent<Rigidbody>().velocity = new Vector3();
            
        particleExplosion.transform.position = this.transform.position;
        particleExplosion.transform.localScale = new Vector3(DamageRadius*ParticleRadius, DamageRadius*ParticleRadius, DamageRadius*ParticleRadius);
        particleExplosion.gameObject.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, DamageRadius);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.Damage;
                monster.Damage(damage, Player.transform);
                if (BonusEffect) monster.ApplyStatus(new StatusStun(monster, BonusDuration));
            }
        }
        yield return new WaitWhile(() => particleExplosion.IsAlive(true));
        Destroy(this.gameObject);
    }
}
