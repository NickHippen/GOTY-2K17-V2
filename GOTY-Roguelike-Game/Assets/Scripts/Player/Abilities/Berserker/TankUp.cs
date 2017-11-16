using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : Ability {

    public override void applyEffect(GameObject player)
    {
		Debug.Log ("TANK UP");
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
    }
}
