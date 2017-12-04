using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeAbility : Ability {

    protected override void Start()
    {
        base.Start();
		//sfx = GetComponent<SoundData>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
		//lolz no effect needed
	}
		
}
