using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : WeaponData
{
    public float swordRange = 2f;
    public float swordRadius = 2.5f;

    protected override void Start()
    {
        base.Start();
        if(particleEffect != null && particleEffect.gameObject.activeInHierarchy)
        {
            Instantiate(particleEffect.gameObject, this.transform.GetChild(0), false).SetActive(true);
        }
    }

    public override void Attack()
    {
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
		Collider[] colliders = Physics.OverlapCapsule(player.transform.position + player.transform.forward * swordRange,
			player.transform.position + player.transform.up*2f + player.transform.forward * swordRange, swordRadius);

		PlayAttackAudio (0);
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
            {
				PlayAttackAudio (1);
                AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                float damage = this.damage;
                damage *= damageMultiplier;
                damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(this, monster, damage);
                damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(this, monster, damage);
                monster.Damage(damage, Player.transform);
            }
        }
    }

    IEnumerator WaitForDrop()
    {
        yield return new WaitWhile(() => this.tag == "Equipped");
        this.GetComponent<MeshRenderer>().enabled = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        if (particleEffect != null)
        {
            particleEffect.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (this.tag == "Equipped")
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(true);
            if(particleEffect != null)
            {
                particleEffect.gameObject.SetActive(false);
            }
            StartCoroutine(WaitForDrop());
        }
    }
}
