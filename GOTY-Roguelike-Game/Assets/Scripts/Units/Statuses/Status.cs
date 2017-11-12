using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status {

	public float DurationMilliseconds { get; set; }
	public float ActiveDuration { get; set; }
	public LivingUnit Target { get; set; }

	public Status(LivingUnit target, float duration) {
		this.Target = target;
		this.DurationMilliseconds = duration;
	}

	public bool IsActive() {
		return ActiveDuration < DurationMilliseconds;
	}

	public void Update() {
		ActiveDuration += Time.deltaTime;
		UpdateAfter();
	}

	protected abstract void UpdateAfter();

	/** Called once when the status changes to inactive
	 */
	public virtual void OnExpire() {
	}

	public abstract int IconIndex();

}
