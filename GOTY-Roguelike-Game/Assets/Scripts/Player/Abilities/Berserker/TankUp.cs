using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : AbilityData {

	public TankUp (float damage, ParticleSystem effect, Vector3 effectPosition, string name)
    {
        this.damage = damage;
        this.effect = effect;
        this.effectPos = effectPosition;
        this.name = name;
    }

    public void applyEffect()
    {
        // need external method
    }
}
