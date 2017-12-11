using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbility : Ability
{
    public float damageMultiplier = 2f;
    public float duration = 5f;
    public float particleEffectSize = 5f;

    ParticleSystem particleEffect;
    Animator anim;
    PlayerInventory playerInv;
    Ability grenade;
    bool isActive;
    float effectHeight;

    protected override void Start()
    {
        base.Start();
        particleEffect = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule pMain = particleEffect.main;
        pMain.startSize = particleEffectSize;
        effectHeight = particleEffect.transform.position.y;
        particleEffect.Stop();

		sfx = GetComponent<SoundData> ();
    }

    protected override void Update()
    {
        base.Update();
        if (isActive) {
            WeaponData currentWeapon = playerInv.getCurrentWeapon().GetComponent<WeaponData>();
            if (currentWeapon is GunData)
            {
                anim.SetLayerWeight(9, 1);  // turn on gun turret
                anim.SetLayerWeight(10, 0);
                anim.SetLayerWeight(11, 0);
                anim.SetLayerWeight(12, 0);
            }
            else if(currentWeapon is SwordData)
            {
                anim.SetLayerWeight(10, 1); // turn on sword turret
                anim.SetLayerWeight(9, 0);
                anim.SetLayerWeight(11, 0);
                anim.SetLayerWeight(12, 0);
            }

            else if (currentWeapon is StaffData)
            {
                anim.SetLayerWeight(11, 1); // turn on staff turret
                anim.SetLayerWeight(9, 0);
                anim.SetLayerWeight(10, 0);
                anim.SetLayerWeight(12, 0);
            }
            else
            {
                anim.SetLayerWeight(12, 1); // turn on dagger turret
                anim.SetLayerWeight(9, 0);
                anim.SetLayerWeight(10, 0);
                anim.SetLayerWeight(11, 0);
            }

            if (bonusEffect)
            {
                grenade.IsAvailible = true;
            }
        }
    }

    public override void applyEffect(GameObject player)
    {
        base.applyEffect(player);
        anim = player.GetComponent<Animator>();
        playerInv = player.GetComponent<PlayerInventory>();
        grenade = player.GetComponent<AbilityController>().getAbilityList()[0];
        if (!isActive) {
            isActive = true;
            StartCoroutine(WaitForFrame());
            foreach (GameObject weapon in playerInv.weapons)
            {
                weapon.GetComponent<WeaponData>().ApplyDamageMultiplier(duration, damageMultiplier);
            }
            particleEffect.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + effectHeight, player.transform.position.z);
            particleEffect.Play();

        }
        else
        {
            isActive = false;
            anim.SetLayerWeight(9, 0); // turn off turret
            anim.SetLayerWeight(10, 0);
            anim.SetLayerWeight(11, 0);
            anim.SetLayerWeight(12, 0);
            particleEffect.Stop();
        }
    }
    
    // prevent applyEffect from being called twice in one frame
    IEnumerator WaitForFrame()
    {
        yield return new WaitForEndOfFrame();
        IsAvailible = true;
    }
}
