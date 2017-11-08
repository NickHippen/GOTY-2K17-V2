using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : AbilityData {

	public Adrenaline (float damage, ParticleSystem effect, string name)
    {
        this.damage = damage;
        this.effect = effect;
        this.abilityName = name;
    }

    public override void applyEffect()
    {
        // need external method
    }
}
