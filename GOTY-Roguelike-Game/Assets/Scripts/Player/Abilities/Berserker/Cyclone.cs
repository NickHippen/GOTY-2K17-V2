using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : Ability {

    public float damage;
    public ParticleSystem effect;
    public float effectDistance;

    public override void applyEffect(GameObject player)
    {
		Debug.Log ("Cyclone");
        this.transform.position = player.transform.position + player.transform.forward * effectDistance;

		Collider[] colliders = Physics.OverlapSphere(this.transform.position, effectDistance);
        foreach(Collider collider in colliders)
        {
			RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider> ();
			Debug.Log (collider);
			if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
				AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
				float damage = this.damage;
				monster.Damage(damage);
			}
        }
        effect.Emit(10);
    }
}
