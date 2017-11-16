using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUp : Ability {

    public override void applyEffect()
    {
		Debug.Log ("TANK UP");
		GameObject player = GameObject.Find ("remy");
		player.GetComponent<HealthManager> ().invincible = !player.GetComponent<HealthManager> ().invincible;
    }
}
