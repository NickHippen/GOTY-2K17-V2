using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpAbility : Ability {

	public ParticleSystem effect;
	public float effectDistance;
	public float effectDuration;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		this.transform.position = player.transform.position + player.transform.forward * effectDistance;
		this.transform.parent = player.transform;
		Debug.Log ("TANK UP");
		//player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
		StartCoroutine(TankTimer(player));
	}

	private IEnumerator TankTimer(GameObject player){
		effect.Play ();
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
		yield return new WaitForSeconds (effectDuration);
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
		effect.Stop ();
	}
		
}
