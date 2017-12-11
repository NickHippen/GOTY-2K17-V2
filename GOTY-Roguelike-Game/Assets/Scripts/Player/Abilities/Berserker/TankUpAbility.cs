using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpAbility : Ability {
    
	public float effectDuration;
    public float bonusHealAmount;
    public float bonusHealRadius = 20;

    ParticleSystem particleEffect;

    protected override void Start()
    {
        base.Start();
		sfx = GetComponent<SoundData>();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleEffect.Stop();
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

		this.transform.position = player.transform.position;
		this.transform.parent = player.transform;

        if(bonusEffect)
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
		StartCoroutine(TankTimer(player));
	}

	private IEnumerator TankTimer(GameObject player){
		particleEffect.Play ();
		//sfx.playSound ();
        player.GetComponent<HealthManager>().invincible = true;
		yield return new WaitForSeconds (effectDuration);
        player.GetComponent<HealthManager>().invincible = false;
		particleEffect.Stop ();
	}
		
}
