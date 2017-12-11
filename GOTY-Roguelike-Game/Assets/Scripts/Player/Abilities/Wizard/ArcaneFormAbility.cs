using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneFormAbility : Ability {

    public float cooldownMuliplier = 3f;
    public float duration;

    GameObject particleEffect;

     protected override void Start()
    {
        base.Start();
        particleEffect = this.transform.GetChild(0).gameObject;
		sfx = GetComponent<SoundData> ();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        List<Ability> abilities = player.GetComponent<AbilityController>().getAbilityList();
        GameObject particles = Instantiate(particleEffect, player.transform, false);
        particles.SetActive(true);
        foreach(Ability ability in abilities)
        {
            if (ability is ArcaneFormAbility) continue;
            if (bonusEffect) ability.IsAvailible = true;
            ability.CooldownMultiplier *= cooldownMuliplier;
        }
        StartCoroutine(StopBuff(abilities, particles));
    }

    private IEnumerator StopBuff(List<Ability> abilities, GameObject particles)
    {
		sfx.playLoop ();
        yield return new WaitForSeconds(duration);
        foreach (Ability ability in abilities)
        {
            if (ability is ArcaneFormAbility) continue;
            ability.CooldownMultiplier /= cooldownMuliplier;
        }
        Destroy(particles);
		sfx.stopLoop ();
    }
}
