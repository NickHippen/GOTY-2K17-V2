using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeObject : MonoBehaviour
{
    public ParticleSystem effect;

    private float timer;
    private float damage;
    private float damageRadius;
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

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0 && !exploded)
        {
            exploded = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().velocity = new Vector3();
            
            effect.transform.position = this.transform.position;
            effect.transform.localScale = new Vector3(damageRadius, damageRadius, damageRadius);
            effect.Emit(10);

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
            foreach (Collider collider in colliders)
            {
                RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
                Debug.Log(collider);
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
