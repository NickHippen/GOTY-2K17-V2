using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPoison : Status {

	private static readonly int ICON_INDEX = 0;

	public float TickRatePerSecond { get; set; }
	public float Damage { get; set; }

	private float timeSinceTick;

	public StatusPoison(LivingUnit target, float duration, float damage, float ticksPerSecond) : base(target, duration) {
		this.Damage = damage;
		this.TickRatePerSecond = ticksPerSecond;
	}

	protected override void UpdateAfter() {
		timeSinceTick += Time.deltaTime;
		if (timeSinceTick >= 1f / TickRatePerSecond) {
			timeSinceTick = 0;
			Target.Damage(Damage);
			Debug.Log("Poison Damage");
		}
	}

	public override int IconIndex() {
		return ICON_INDEX;
	}
}
