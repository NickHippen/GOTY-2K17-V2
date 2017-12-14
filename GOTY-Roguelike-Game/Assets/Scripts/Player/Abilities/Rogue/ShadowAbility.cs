using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAbility : Ability {
    
    public float duration;
    public float bonusDuration;
    public Vector2 effectPosition = new Vector2(.2f, 1.1f);
    GameObject particleEffect;

    protected override void Start()
    {
        base.Start();
        sfx = GetComponent<SoundData>();
        particleEffect = transform.GetChild(0).gameObject;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        player.GetComponent<ThirdPersonCharacter>().IsInvisible = true;
        GameObject playerEffect = Instantiate(particleEffect, player.transform, false);
        playerEffect.transform.position += player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        playerEffect.gameObject.SetActive(true);
        StartCoroutine(endEventOnAction(player, playerEffect));
        StartCoroutine(effectTimer(player, playerEffect));
    }

    IEnumerator endEventOnAction(GameObject player, GameObject effect)
    {
        yield return new WaitUntil(() => player.GetComponent<Animator>().GetBool("Attack") == true);
        particleEffect.GetComponent<ParticleSystem>().Stop();
        player.GetComponent<ThirdPersonCharacter>().IsInvisible = false;
        yield return new WaitWhile(() => particleEffect.GetComponent<ParticleSystem>().IsAlive());
        if (effect != null)
        {
            Destroy(effect);
        }
    }

    IEnumerator effectTimer(GameObject player, GameObject effect)
    {
        yield return new WaitForSeconds(bonusEffect ? duration + bonusDuration : duration);
        particleEffect.GetComponent<ParticleSystem>().Stop();
        player.GetComponent<ThirdPersonCharacter>().IsInvisible = false;
        yield return new WaitWhile(() => particleEffect.GetComponent<ParticleSystem>().IsAlive());
        if (effect != null) {
            Destroy(effect);
        }
    }
}
