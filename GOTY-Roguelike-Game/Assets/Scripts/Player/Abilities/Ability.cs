using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    public Sprite icon;
    public float cooldownTime;
    public bool faceCameraDirection;
    public string bonusDescription;
    protected bool applyOnFrame;
    protected bool bonusEffect;
    private float cooldownTimer;
    private bool isAvailible = true;
    private float cooldownMultiplier = 1;
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
    public float CooldownMultiplier
    {
        get { return cooldownMultiplier; }
        set { cooldownMultiplier = value; }
    }
    public bool BonusEffectActive
    {
        get { return bonusEffect; }
        set { bonusEffect = value; }
    }

    public virtual void applyEffect(GameObject player)
    {
        if (faceCameraDirection)
        {
            player.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            player.GetComponent<ThirdPersonCharacter>().LockTurnRotation = true;
            StartCoroutine(AbilityAvailible(player));
        }
		if (sfx != null && sfx.soundEffects.Count > 0) {
			sfx.playSound ();
		}
    }

    IEnumerator AbilityAvailible(GameObject player)
    {
        int layerNum = player.GetComponent<AbilityController>().getLayerNumber();
        yield return new WaitWhile(() => player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(layerNum).IsName("Grounded"));
        yield return new WaitUntil(() => player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(layerNum).IsName("Grounded"));
        player.GetComponent<ThirdPersonCharacter>().LockTurnRotation = false;
    }
}
