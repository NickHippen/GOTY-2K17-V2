using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Ability
{
    public float damageMultiplier = 2f;
    public float duration = 5f;

    public override void Start()
    {
        base.Start();
    }

    public override void applyEffect(GameObject player)
    {
        Animator anim = player.GetComponent<Animator>();
        ThirdPersonCharacter character = player.GetComponent<ThirdPersonCharacter>();

        anim.SetLayerWeight(5, 1);  // turn on crouching mask

        character.StopMovement = true;
        StartCoroutine(CrouchEffect(anim, character));
    }
    
    private IEnumerator CrouchEffect(Animator anim, ThirdPersonCharacter character)
    {
        yield return new WaitForSeconds(duration);
        anim.SetLayerWeight(5, 0);  // turn off crouching mask
        character.StopMovement = false;
    }
}
