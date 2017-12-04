using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    ParticleSystem impactParticle;
    float timer;
    float damage;
    float particleRadius;
    bool isHit;

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

    private void Start()
    {
        //impactParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        //StartCoroutine(RemoveObject(false));
    }

	// knife keeps going thru wall, will change to raycast
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Unwalkable") ||
            other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            GetComponent<Rigidbody>().velocity = new Vector3();
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				transform.position = hit.point;
			}
            GetComponent<MeshCollider>().enabled = false;
            RigCollider rigCollider = other.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
            {
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                monster.Damage(damage);
            }
            //StartCoroutine(RemoveObject(true));
        }
    }

    IEnumerator RemoveObject(bool hit)
    {
        if(hit) yield return new WaitWhile(() => impactParticle.IsAlive(true));
        else yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
