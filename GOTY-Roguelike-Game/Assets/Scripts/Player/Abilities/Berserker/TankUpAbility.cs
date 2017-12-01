using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpAbility : Ability {

	public ParticleSystem particleEffect;
	public float effectDistance;
	public float effectDuration;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		this.transform.position = player.transform.position + player.transform.forward * effectDistance;
		this.transform.parent = player.transform;

		StartCoroutine(TankTimer(player));
	}

	private IEnumerator TankTimer(GameObject player){
		particleEffect.Play ();
		sfx.playSound ();
        player.GetComponent<HealthManager>().invincible = true;
		yield return new WaitForSeconds (effectDuration);
        player.GetComponent<HealthManager>().invincible = false;
		particleEffect.Stop ();
	}
		
}
