using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneFormAbility : Ability {

    public float cooldownMuliplier = 3f;
    public float duration;

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        List<Ability> abilities = player.GetComponent<AbilityController>().getAbilityList();
        foreach(Ability ability in abilities)
        {
            if (ability is ArcaneFormAbility) continue;
            ability.CooldownMultiplier += cooldownMuliplier;
        }
        StartCoroutine(StopBuff(abilities));
    }

    private IEnumerator StopBuff(List<Ability> abilities)
    {
        yield return new WaitForSeconds(duration);
        foreach (Ability ability in abilities)
        {
            if (ability is ArcaneFormAbility) continue;
            ability.CooldownMultiplier -= cooldownMuliplier;
        }
    }
}
