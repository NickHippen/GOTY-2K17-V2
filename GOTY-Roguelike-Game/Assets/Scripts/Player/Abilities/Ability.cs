using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    
    public float cooldownTime;
    public bool applyOnFrame;

    private float cooldownTimer;
    private bool isAvailible = true;

	// Use this for initialization
	public virtual void Start () {
        cooldownTimer = cooldownTime;
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

    public bool ApplyOnFrame
    {
        get { return applyOnFrame; }
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

    public abstract void applyEffect(GameObject player);

}
