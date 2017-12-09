using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAbility : Ability {
    
    public float duration;

    ParticleSystem particleEffect;

    protected override void Start()
    {
        base.Start();
        sfx = GetComponent<SoundData>();
        applyOnFrame = true;
        //particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        player.GetComponent<ThirdPersonCharacter>().IsInvisible = true;
        StartCoroutine(effectTimer(player));
    }

    private IEnumerator effectTimer(GameObject player)
    {
        yield return new WaitForSeconds(duration);
        
    }
}
