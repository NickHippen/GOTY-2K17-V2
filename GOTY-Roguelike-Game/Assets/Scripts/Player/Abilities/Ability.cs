using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    public Sprite icon;
    public float cooldownTime;
    public bool faceCameraDirection;
    protected bool applyOnFrame;
    private float cooldownTimer;
    private bool isAvailible = true;
    private float cooldownMultiplier = 1f;
	public SoundData sfx;

	// Use this for initialization
	protected virtual void Start () {
		//sfx.GetComponent<SoundData> ();
        cooldownTimer = cooldownTime;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(!isAvailible)
        {
            cooldownTimer -= Time.deltaTime * cooldownMultiplier;
            if(CooldownTimer <= 0f)
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
    public float CooldownMultiplier
    {
        get { return cooldownMultiplier; }
        set { cooldownMultiplier = value; }
    }

    public virtual void applyEffect(GameObject player)
    {
        if(faceCameraDirection) player.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
		if (sfx != null) {
			sfx.playSound ();
		}
    }
}
