using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeAbility : Ability {

    public float bonusHealAmount;

    protected override void Start()
    {
        base.Start();
		sfx = GetComponent<SoundData>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		if(bonusEffect)
        {
            player.GetComponent<HealthManager>().Heal(bonusHealAmount);
        }
	}
		
}
