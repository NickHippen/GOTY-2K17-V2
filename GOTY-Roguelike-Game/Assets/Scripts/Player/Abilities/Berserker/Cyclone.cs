using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : Ability {

    public float damage;
    public ParticleSystem effect;
    public float effectDistance;

    public override void applyEffect(GameObject player)
    {
        this.transform.position = player.transform.position + player.transform.forward * effectDistance;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Monster")
            {
                AggressiveUnit unit = collider.GetComponent<AggressiveUnit>();
                //unit.health -= damage;
				unit.Damage(damage);
            }
        }
        effect.Emit(10);
    }
}
