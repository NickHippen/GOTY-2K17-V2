using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStun : Status {

	private static readonly int ICON_INDEX = 2;

	public StatusStun(LivingUnit target, float duration) : base(target, duration) {
	}

	protected override void UpdateAfter() {
		Target.speed = 0;
	}

	public override void OnExpire() {
		Target.speed = Target.DefaultSpeed;
	}

	public override int IconIndex() {
		return ICON_INDEX;
	}
}
