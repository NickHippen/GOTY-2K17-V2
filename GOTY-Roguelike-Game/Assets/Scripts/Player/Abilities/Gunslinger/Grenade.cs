using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public ParticleSystem effect;

    private float timer;
    private float damage;
    private float damageRadius;
    private float particleRadius;
    private bool exploded;

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
    
    void FixedUpdate()
    {
        if(timer < 0 && !exploded)
        {
            exploded = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().velocity = new Vector3();
            
            effect.transform.position = this.transform.position;
            effect.transform.localScale = new Vector3(damageRadius*particleRadius, damageRadius*particleRadius, damageRadius*particleRadius);
            effect.Emit(10);

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
        else timer -= Time.deltaTime;
    }

    IEnumerator removeGrenade()
    {
        yield return new WaitForSeconds(effect.main.duration);
        Destroy(this.gameObject);
    }
}
