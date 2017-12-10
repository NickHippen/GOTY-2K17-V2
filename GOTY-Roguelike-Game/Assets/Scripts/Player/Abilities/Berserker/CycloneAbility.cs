using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneAbility : Ability {

    public float damage;
	//Radius of damage the effect has
	public float damageRadius;

    public float bonusEffectDuration;
    public float bonusDamageOverTime;
    public float bonusTicksPerSecond;
    
    //Distance of particle effect from player
    public Vector2 effectPosition;

    ParticleSystem particleEffect;

    protected override void Start(){
        base.Start();
        applyOnFrame = true;
		sfx = GetComponent<SoundData> ();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainEffect = particleEffect.main;
        mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
        mainEffect = particleEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        mainEffect.startSize = damageRadius * mainEffect.startSize.constant;
	}

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        this.transform.position = player.transform.position + player.transform.forward*effectPosition.x + player.transform.up*effectPosition.y;

		Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
        foreach(Collider collider in colliders)
        {
			RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider> ();
			Debug.Log (collider);
			if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit) {
				AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                if (bonusEffect) {
                    monster.ApplyStatus(new StatusPoison(monster, bonusEffectDuration, bonusDamageOverTime, bonusTicksPerSecond));
                }
				monster.Damage(damage, player.transform);
			}
        }
		//sfx.playSound ();
        particleEffect.Play();
    }
}
