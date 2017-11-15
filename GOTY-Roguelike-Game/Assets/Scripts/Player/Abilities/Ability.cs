using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public float damage;
    public float cooldownTime;
	public ParticleSystem effect;
	public float effectDistance;

    private float cooldownTimer;

	// Use this for initialization
	void Start () {
		//effect = Instantiate (effect);
		//effect.transform.SetParent (gameObject.transform);
		//effect.transform.position = effectPos;
		//effect.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float CooldownTimer
    {
        get { return cooldownTimer; }
        set { cooldownTimer = value; }
    }

    public abstract void applyEffect();

}
