using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardKick : AbilityData {

	public HardKick (float damage, ParticleSystem effect, string name)
    {
        this.damage = damage;
        this.effect = effect;
        this.abilityName = name;
    }

    public override void applyEffect()
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
