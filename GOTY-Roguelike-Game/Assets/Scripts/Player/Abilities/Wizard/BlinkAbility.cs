using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAbility : Ability {

    public float range = 15f;
    public float particleHeight = 1f;
    public float particleChargeSize = 5f;
    public float raycastHeight = 0.4f;
    public float bonusHealAmount = 5f;
    public float bonusHealRadius = 20f;
    //public float collisionOffset = 0.01f; noticed walls have width so not necessary, but commented underneath if needed

    Vector3 move;
    GameObject particleObject;

    protected override void Start()
    {
        base.Start();
        particleObject = transform.GetChild(0).gameObject;
		sfx = GetComponent<SoundData> ();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        GameObject playerParticle = Instantiate(particleObject, player.transform, false);
        playerParticle.transform.position += player.transform.up * particleHeight;
        ParticleSystem.MainModule partMain = playerParticle.GetComponent<ParticleSystem>().main;
        partMain.startSize = particleChargeSize;
        playerParticle.SetActive(true);
        if (bonusEffect)
        {
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, bonusHealRadius);
            foreach (Collider collider in colliders)
            {
                RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
                Debug.Log(collider);
                if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
                {
                    AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                    if (bonusEffect)
                    {
                        player.GetComponent<HealthManager>().Heal(bonusHealAmount);
                        break;
                    }
                }
            }
        }
        StartCoroutine(WaitForParticleExplosion(player));
        StartCoroutine(DisableParticle(playerParticle));
    }

    IEnumerator WaitForParticleExplosion(GameObject player)
    {
        yield return new WaitForSeconds(particleObject.GetComponentsInChildren<ParticleSystem>()[1].main.startDelay.constant - 0.1f);
        move = player.GetComponent<ThirdPersonCharacter>().getMoveDirection();
        RaycastHit hit;
        Vector3 goalPoint = new Vector3(player.transform.position.x + move.x * range, player.transform.position.y + raycastHeight, player.transform.position.z + move.z * range);
        Vector3 playerPoint = player.transform.position + player.transform.up * raycastHeight;
        int layerMask = LayerMask.GetMask("Unwalkable"); // Only collisions in layer Unwalkable
        if (Physics.Raycast(playerPoint, (goalPoint - playerPoint).normalized, out hit, range, layerMask))
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
        Destroy(particleSys);
    }
}
