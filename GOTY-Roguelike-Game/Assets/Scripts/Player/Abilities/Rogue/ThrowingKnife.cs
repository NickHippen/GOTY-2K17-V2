using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    ParticleSystem impactParticle;
    float timer;
    float damage;
    float particleRadius;
    float collisionOffset;
    bool bonusEffect;
    float poisonDuration;
    float poisonDamage;
    float poisonTPS;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
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
    public float CollisionOffset
    {
        get { return collisionOffset; }
        set { collisionOffset = value; }
    }
    public bool BonusEffect
    {
        get { return bonusEffect; }
        set { bonusEffect = value; }
    }
    public float PoisonDuration
    {
        get { return poisonDuration; }
        set { poisonDuration = value; }
    }
    public float PoisonDamage
    {
        get { return poisonDamage; }
        set { poisonDamage = value; }
    }
    public float PoisonTPS
    {
        get { return poisonTPS; }
        set { poisonTPS = value; }
    }
    private void Start()
    {
        impactParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        //StartCoroutine(RemoveObject(false));
    }

	// knife keeps going thru wall, will change to raycast
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Unwalkable") ||
            other.gameObject.layer == LayerMask.NameToLayer("Ground")) {

            impactParticle.gameObject.SetActive(true);

            transform.position = this.transform.position - GetComponent<Rigidbody>().velocity * collisionOffset;
            GetComponent<Rigidbody>().velocity = new Vector3();

            GetComponent<MeshCollider>().enabled = false;
            RigCollider rigCollider = other.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                monster.Damage(damage);
                if(bonusEffect)
                {
                    monster.ApplyStatus(new StatusPoison(monster, poisonDuration, poisonDamage, poisonTPS));
                }
            }
            //StartCoroutine(RemoveObject(true));
        }
    }

    IEnumerator RemoveObject(bool hit)
    {
        if (hit) yield return new WaitWhile(() => impactParticle.IsAlive(true));
        else yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
