using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public float damage;
    public float cooldownTime;
	public ParticleSystem effect;
	public float effectDistance;

    private float cooldownTimer;
    private bool isAvailible;

	// Use this for initialization
	void Start () {
		//effect = Instantiate (effect);
		//effect.transform.SetParent (gameObject.transform);
		//effect.transform.position = effectPos;
		//effect.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isAvailible)
        {
            cooldownTimer -= Time.deltaTime;
            if(CooldownTimer < 0f)
            {
                cooldownTimer = cooldownTime;
                isAvailible = true;
            }
        }
	}

    public float CooldownTimer
    {
        get { return cooldownTimer; }
        set { cooldownTimer = value; }
    }

    public bool IsAvailible
    {
        get { return isAvailible; }
        set { isAvailible = value; }
    }

    public abstract void applyEffect();

}
