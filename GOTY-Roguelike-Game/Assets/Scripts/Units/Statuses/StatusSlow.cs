using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSlow : Status {

	private static readonly int ICON_INDEX = 1;

	public float SlowPercent { get; set; }

	private float timeSinceTick;

	public StatusSlow(LivingUnit target, float duration, float slowPercent) : base(target, duration) {
		this.SlowPercent = slowPercent;
	}

	protected override void UpdateAfter() {
		Target.speed = Target.DefaultSpeed * (1 - SlowPercent);
	}

	public override void OnExpire() {
		Target.speed = Target.DefaultSpeed;
	}

	public override int IconIndex() {
		return ICON_INDEX;
	}
}
