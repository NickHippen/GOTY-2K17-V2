using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    ParticleSystem particleEffect;
    float timer;
    float damage;
    float damageRadius;
    float particleRadius;
    bool bonusEffect;
    float bonusDamage;
    float bonusRadiusMultiplier;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float DamageRadius
    {
        get { return damageRadius; }
        set { damageRadius = value; }
    }
    public float ParticleRadius
    {
        get { return particleRadius; }
        set { particleRadius = value; }
    }
    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }
    public bool BonusEffect
    {
        get { return bonusEffect; }
        set { bonusEffect = value; }
    }
    public float BonusDamage
    {
        get { return bonusDamage; }
        set { bonusDamage = value; }
    }
    public float BonusRadiusMultiplier
    {
        get { return bonusRadiusMultiplier; }
        set { bonusRadiusMultiplier = value; }
    }
	public GameObject Player { get; set; }

    private void Start()
    {
        particleEffect = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        StartCoroutine(PullPin());
    }

    IEnumerator PullPin()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = new Vector3();
            
        particleEffect.transform.position = this.transform.position;
        particleEffect.transform.localScale = bonusEffect
            ? new Vector3(damageRadius * particleRadius * BonusRadiusMultiplier, damageRadius * particleRadius * BonusRadiusMultiplier, damageRadius * particleRadius * bonusRadiusMultiplier) 
            : new Vector3(damageRadius*particleRadius, damageRadius*particleRadius, damageRadius*particleRadius);
        particleEffect.gameObject.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, bonusEffect? damageRadius * bonusRadiusMultiplier: damageRadius);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                if(bonusEffect)
                {
                    damage += bonusDamage;
                }
                monster.Damage(damage, Player.transform);
            }
        }
        StartCoroutine(removeGrenade());
    }

    IEnumerator removeGrenade()
    {
        yield return new WaitWhile(() => particleEffect.IsAlive(true));
        Destroy(this.gameObject);
    }
}
