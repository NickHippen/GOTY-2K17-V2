using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpAbility : Ability {
    
	public float effectDuration;
    public float bonusHealAmount;
    public float bonusHealRadius = 20;

    GameObject shieldEffect;

    protected override void Start()
    {
        base.Start();
		sfx = GetComponent<SoundData>();
        shieldEffect = transform.GetChild(0).gameObject;
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        GameObject newShield = Instantiate(shieldEffect, player.transform, false);
        newShield.transform.position += player.transform.forward * 0.05f;
        newShield.transform.Rotate(0f, 90f, 0f);
        newShield.SetActive(true);

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
		StartCoroutine(TankTimer(player, newShield));
	}

	private IEnumerator TankTimer(GameObject player, GameObject shield){
		//sfx.playSound ();
        player.GetComponent<HealthManager>().invincible = true;
		yield return new WaitForSeconds (effectDuration);
        player.GetComponent<HealthManager>().invincible = false;
        Destroy(shield);
	}
		
}
