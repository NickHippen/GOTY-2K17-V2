using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusVulnerable : Status {

	private static readonly int ICON_INDEX = 3;

	public float Multiplier { get; set; }

	public StatusVulnerable(LivingUnit target, float duration, float multiplier) : base(target, duration) {
		this.Multiplier = multiplier;
	}

	protected override void UpdateAfter() {
		// Nothing to actively do
	}

	public override int IconIndex() {
		return ICON_INDEX;
	}
}
