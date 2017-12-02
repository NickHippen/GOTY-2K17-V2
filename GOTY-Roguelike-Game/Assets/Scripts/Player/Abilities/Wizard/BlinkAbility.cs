using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAbility : Ability {

    public float effectDistance;
    public float effectDuration;
    public float particleHeight = 1f;
    public float particleChargeSize = 5f;
    public float raycastHeight = 0.3f;
    public float range = 5f;
    //public float collisionOffset = 0.01f; noticed walls have width so not necessary, but commented underneath if needed

    Vector3 move;
    GameObject particleObject;

    protected override void Start()
    {
        base.Start();
        particleObject = transform.GetChild(0).gameObject;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        GameObject playerParticle = Instantiate(particleObject, player.transform, false);
        playerParticle.transform.position += player.transform.up * particleHeight;
        ParticleSystem.MainModule partMain = playerParticle.GetComponent<ParticleSystem>().main;
        partMain.startSize = particleChargeSize;
        playerParticle.SetActive(true);
        StartCoroutine(WaitForParticleExplosion(player));
        StartCoroutine(DisableParticle(playerParticle));
    }

    IEnumerator WaitForParticleExplosion(GameObject player)
    {
        yield return new WaitForSeconds(0.79f); // particle explosion start is at 0.8f
        move = player.GetComponent<ThirdPersonCharacter>().getMoveDirection();
        RaycastHit hit;

        Vector3 goalPoint = new Vector3(player.transform.position.x + move.x * range, player.transform.position.y + raycastHeight, player.transform.position.z + move.z * range);

        int layerMask = LayerMask.GetMask("Unwalkable"); // Only collisions in layer Unwalkable
        if (Physics.Raycast(player.transform.position + player.transform.up * raycastHeight, goalPoint, out hit, range, layerMask))
        {
            player.transform.position = hit.point; //+ (player.transform.position - hit.transform.position)*collisionOffset;
        }
        else
        {
            player.transform.position = goalPoint;
        }
    }

    IEnumerator DisableParticle(GameObject particleSys)
    {
        yield return new WaitUntil(() => !particleSys.GetComponent<ParticleSystem>().IsAlive(true));
        print("Destroyed");
        Destroy(particleSys);
    }
}
