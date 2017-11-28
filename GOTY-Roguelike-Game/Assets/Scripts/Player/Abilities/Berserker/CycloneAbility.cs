using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneAbility : Ability {

    public float damage;
    public ParticleSystem effect;
	//Distance of particle effect from player
    public float effectDistance;
	//Radius of damage the effect has
	public float damageRadius;

	protected override void Start(){
        base.Start();
        applyOnFrame = true;
		effect.transform.localScale = new Vector3 (damageRadius, damageRadius, 1);
	}

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        this.transform.position = player.transform.position + player.transform.forward * effectDistance;

		Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
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
