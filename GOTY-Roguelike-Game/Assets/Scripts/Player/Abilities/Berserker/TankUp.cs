using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : Ability {

	public ParticleSystem effect;
	public float effectDistance;
	public float effectDuration;

    public override void applyEffect(GameObject player)
    {
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
