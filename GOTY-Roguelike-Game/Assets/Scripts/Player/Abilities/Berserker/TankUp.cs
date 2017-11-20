using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : Ability {

    public override void applyEffect(GameObject player)
    {
		Debug.Log ("TANK UP");
		//player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
		StartCoroutine(TankTimer(player));
	}

	private IEnumerator TankTimer(GameObject player){
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
		yield return new WaitForSeconds (3f);
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
	}
		
}
