using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private ParticleSystem particleEffect;
    private float timer;
    private float damage;
    private float damageRadius;
    private float particleRadius;

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
        particleEffect.transform.localScale = new Vector3(damageRadius*particleRadius, damageRadius*particleRadius, damageRadius*particleRadius);
        particleEffect.gameObject.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                monster.Damage(damage);
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
