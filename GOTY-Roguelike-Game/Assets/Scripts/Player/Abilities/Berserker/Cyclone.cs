using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : AbilityData {

	public Cyclone (float damage, ParticleSystem effect, Vector3 effectPosition, string name)
    {
        this.damage = damage;
        this.effect = effect;
        this.effectPos = effectPosition;
        this.name = name;
    }

    public void applyEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(effectPos, 1f);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Monster")
            {
                AggressiveUnit unit = collider.GetComponent<AggressiveUnit>();
                unit.health -= damage;
            }
        }
    }
}
