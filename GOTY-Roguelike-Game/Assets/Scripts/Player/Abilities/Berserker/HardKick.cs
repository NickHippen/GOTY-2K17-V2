using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardKick : Ability {

    public float damage;
    public ParticleSystem effect;
    public float effectDistance;

    public override void applyEffect(GameObject player)
    {
        this.transform.position = player.transform.position + player.transform.forward * effectDistance;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Monster")
            {
                AggressiveUnit unit = collider.GetComponent<AggressiveUnit>();
                unit.health -= damage;
            }
        }
        effect.Emit(100);
    }
}
