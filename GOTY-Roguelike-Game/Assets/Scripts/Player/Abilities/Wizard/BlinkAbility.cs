using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAbility : Ability {

    public ParticleSystem particleEffect;
    public float effectDistance;
    public float effectDuration;

    protected override void Start()
    {
        base.Start();
        applyOnFrame = true;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        // Kevin's self-notes before starting again: get move vector from ThirdPersonCharacter to determine direction, else camera direction if move=0.
        // check if floor exists in one direction, move if true
        // else raycastAll for wall collisions and teleport to wall w/ a hair towards the player as to not move thru wall

        //player.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        //shootPoint = new Vector3(0, playerHeight, 0) + player.transform.position;

        //RaycastHit hit;
        //if (Physics.Raycast(shootPoint, Camera.main.transform.forward, out hit, range))
        //{
        //    particleEffect.transform.position = hit.point;

        //    GameObject mark = Instantiate(particleEffect.transform.gameObject, hit.transform, true);
        //    mark.gameObject.SetActive(true);

        //    RigCollider rigCollider = hit.transform.GetComponent<RigCollider>();
        //    if (rigCollider == null)
        //    {
        //        StartCoroutine(RemoveParticle(missDuration, mark));
        //        return;
        //    }

        //    Unit unit = rigCollider.RootUnit;
        //    if (unit is AggressiveUnit)
        //    {
        //        AggressiveUnit monster = (AggressiveUnit)unit;
        //        monster.ApplyStatus(new StatusVulnerable(monster, markDuration, damageMultiplier));
        //        StartCoroutine(RemoveParticle(markDuration, mark));
        //    }
        //    else
        //    {
        //        StartCoroutine(RemoveParticle(missDuration, mark));
        //    }
        //}
    }
}
