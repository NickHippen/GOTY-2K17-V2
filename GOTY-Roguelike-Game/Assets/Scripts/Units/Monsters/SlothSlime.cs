﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSlime : AggressiveUnit {
	protected override void Start() {
		base.Start();
		sfx = GetComponent<SoundData> ();
	}

	protected override void ApplyAttackBehavior() {
		attacks.Add(new BasicDamageAttack(
			new IntervalAttackController(this, 2, 2)
		));
	}

}