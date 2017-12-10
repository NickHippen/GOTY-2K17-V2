using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAbility : Ability {
    
    public float duration;
    public float bonusDuration;
    public float effectHeight = 1.5f;
    GameObject particleEffect;

    protected override void Start()
    {
        base.Start();
        sfx = GetComponent<SoundData>();
        applyOnFrame = true;
        particleEffect = transform.GetChild(0).gameObject;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        player.GetComponent<ThirdPersonCharacter>().IsInvisible = true;
        GameObject playerEffect = Instantiate(particleEffect, player.transform, false);
        playerEffect.transform.position += player.transform.up * effectHeight;
        playerEffect.gameObject.SetActive(true);
        StartCoroutine(effectTimer(player, particleEffect));
    }

    private IEnumerator effectTimer(GameObject player, GameObject effect)
    {
        yield return new WaitForSeconds(bonusEffect ? duration + bonusDuration : duration);
        player.GetComponent<ThirdPersonCharacter>().IsInvisible = true;
        if (effect != null) {
            Destroy(effect);
        }
    }
}
