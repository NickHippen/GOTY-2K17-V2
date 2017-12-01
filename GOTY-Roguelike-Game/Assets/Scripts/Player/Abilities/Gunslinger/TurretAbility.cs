﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbility : Ability
{
    public float damageMultiplier = 2f;
    public float duration = 5f;
    public ParticleSystem particleEffect;
    public float particleEffectSize = 5f;

    Animator anim;
    PlayerInventory playerInv;
    bool isActive;

    protected override void Start()
    {
        base.Start();
        ParticleSystem.MainModule pMain = particleEffect.main;
        pMain.startSize = particleEffectSize;

        
    }

    protected override void Update()
    {
        base.Update();
        if (isActive) {
            if (playerInv.getCurrentWeapon().GetComponent<WeaponData>() is GunData)
            {
                anim.SetLayerWeight(5, 1);  // turn on gun turret
                anim.SetLayerWeight(6, 0);
            }
            else
            {
                anim.SetLayerWeight(6, 1); // turn on sword turret
                anim.SetLayerWeight(5, 0);
            }
        }
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);

        anim = player.GetComponent<Animator>();
        playerInv = player.GetComponent<PlayerInventory>();

        isActive = true;
        foreach (GameObject weapon in playerInv.weapons)
        {
            weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
        }
        particleEffect.transform.position = player.transform.position;
        particleEffect.Play();
        StartCoroutine(CrouchEffect());
    }
    
    private IEnumerator CrouchEffect()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
        anim.SetLayerWeight(4, 0); // turn off turret
        anim.SetLayerWeight(5, 0);
        particleEffect.Stop();
    }
}
