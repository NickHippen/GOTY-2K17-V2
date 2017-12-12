﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerData : WeaponData
{
    public float daggerRange = 1f;
    public float daggerRadius = 2f;
    public float hitsPerSecond = 10f;

    float timeSinceLastAttack = 0f;
    Vector3 leftDaggerRotation = new Vector3(-35, 90, -100);
    Vector3 leftDaggerPosition = new Vector3(-0.25f, -0.04f, 0.1f);
    Transform leftHand;
    GameObject secondDagger;

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public override void Attack()
    {
        if (timeSinceLastAttack >= 1f / hitsPerSecond)
        {
            PlayAttackAudio(0);
            timeSinceLastAttack = 0f;
            SlashAttack();
        }
    }
    void SlashAttack() {
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
		Collider[] colliders = Physics.OverlapCapsule(player.transform.position + player.transform.forward * daggerRange,
			player.transform.position + player.transform.up*2f + player.transform.forward * daggerRange, daggerRadius);
        
        foreach (Collider collider in colliders)
        {
            RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
            if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
            {
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
        Destroy(secondDagger);
    }
    private void OnDisable()
    {
        if (secondDagger != null)
        {
            Destroy(secondDagger);
        }
    }

    private void OnEnable()
    {
        if(this.tag == "Equipped")
        {
            this.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            leftHand = this.GetComponentInParent<ThirdPersonCharacter>().leftHand.transform;
            secondDagger = Instantiate(this.transform.GetChild(0).gameObject, leftHand, false);
            secondDagger.transform.localPosition = leftDaggerPosition;
            secondDagger.transform.localEulerAngles = leftDaggerRotation;
            StartCoroutine(WaitForDrop());
        }
    }

    private void OnDestroy()
    {
        Destroy(secondDagger);
    }
}
