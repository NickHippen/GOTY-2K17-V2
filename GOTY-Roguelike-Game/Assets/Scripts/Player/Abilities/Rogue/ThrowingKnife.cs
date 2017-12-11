using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    ParticleSystem impactParticle;
    public float Damage { get; set; }
    public float ParticleRadius { get; set; }
    public float Timer { get; set; }
    public float CollisionOffset { get; set; }
    public bool BonusEffect { get; set; }
    public float PoisonDuration { get; set; }
    public float PoisonDamage { get; set; }
    public float PoisonTPS { get; set; }
    public float BonusDuration { get; set; }
    public float BonusMultiplier { get; set; }
	public GameObject Player { get; set; }

    private void Start()
    {
        impactParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        transform.GetChild(1).GetComponent<ParticleSystem>().gameObject.SetActive(true);
        StartCoroutine(RemoveObject());
    }

	// knife keeps going thru wall, may change to raycast
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Unwalkable") ||
            other.gameObject.layer == LayerMask.NameToLayer("Ground")) {

            impactParticle.gameObject.SetActive(true);

            transform.position = this.transform.position - GetComponent<Rigidbody>().velocity * CollisionOffset;
            GetComponent<Rigidbody>().velocity = new Vector3();

            GetComponent<MeshCollider>().enabled = false;
            RigCollider rigCollider = other.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.Damage;
                monster.Damage(damage, Player.transform);
                monster.ApplyStatus(new StatusPoison(monster, PoisonDuration, PoisonDamage, PoisonTPS));
                if (BonusEffect)
                {
                    monster.ApplyStatus(new StatusVulnerable(monster, BonusDuration, BonusMultiplier));
                }
                this.transform.SetParent(monster.transform);
            }
        }
    }

    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(this.gameObject);
    }
}
