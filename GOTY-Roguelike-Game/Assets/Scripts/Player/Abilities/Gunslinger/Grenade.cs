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
    private void Start()
    {
        particleExplosion = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSparks = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        StartCoroutine(PullPin());
    }

    IEnumerator PullPin()
    {
        yield return new WaitForSeconds(Timer);
        GetComponent<MeshRenderer>().enabled = false;
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
        StartCoroutine(removeGrenade());
    }

    IEnumerator removeGrenade()
    {
        yield return new WaitWhile(() => particleExplosion.IsAlive(true));
        Destroy(this.gameObject);
    }
}
