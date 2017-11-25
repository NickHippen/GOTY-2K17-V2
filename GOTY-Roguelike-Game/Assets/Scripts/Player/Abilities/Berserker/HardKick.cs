using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardKick : Ability {

    public float damage;
    public ParticleSystem effect;
    public float effectDistance;
	public float damageRadius;

    public override void applyEffect(GameObject player)
    {
        this.transform.position = player.transform.position + player.transform.forward * effectDistance;

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach(Collider collider in colliders)
        {
			RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider> ();
			Debug.Log (collider);
			if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
				AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
				float damage = this.damage;
				monster.ApplyStatus (new StatusStun(monster, 5f));
				monster.Damage(damage);
			}
        }
        effect.Emit(100);
    }
}
