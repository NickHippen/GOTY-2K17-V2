using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum WeaponEmotion {

	None=0, Sorrow, Isolation, Rage, Inspiration, Compassion, Confidence, Shame, Anxiety, Envy, Elation

}
// Inpiration and Compassion are applied in WeaponData, Same and Confidence in HealthManager

public class WeaponEmotionActionHandler {

	public static EmotionAction GetOnDamageAction(WeaponEmotion emotion) {
		MethodInfo method = (new WeaponEmotionActionHandler()).GetType().GetMethod("Apply" + emotion.ToString());
		if (method == null) {
			return ApplyNone;
		}
		return (WeaponData weaponData, LivingUnit target, float damage) => {
			return (float)method.Invoke(null, new object[] { weaponData, target, damage });
		};
	}

	public static float ApplyNone(WeaponData weaponData, LivingUnit target, float damage) {
		return damage;
	}

	public static float ApplySorrow(WeaponData weaponData, LivingUnit target, float damage) {
        if (weaponData.RandomChanceHit(0.2f)) target.ApplyStatus(new StatusSlow(target, 2f, 0.2f));
		return damage;
	}

	public static float ApplyIsolation(WeaponData weaponData, LivingUnit target, float damage) {
        if (weaponData.RandomChanceHit(0.2f)) target.ApplyKnockback(2f);
		return damage;
	}

	public static float ApplyRage(WeaponData weaponData, LivingUnit target, float damage) {
        if(weaponData.RandomChanceHit(0.2f)) target.ApplyStatus(new StatusVulnerable(target, 2f, 1.05f), false);
		return damage;
	}

	public static float ApplyAnxiety(WeaponData weaponData, LivingUnit target, float damage) {
		if(weaponData.RandomChanceHit(0.2f)) target.ApplyStatus(new StatusPoison(target, 4f, 2f, 1), false);
		return damage;
	}

	public static float ApplyEnvy(WeaponData weaponData, LivingUnit target, float damage) {
        if (weaponData.RandomChanceHit(0.2f)) target.ApplyStatus(new StatusStun(target, 1f), false);
		return damage;
	}

    public static float ApplyElation(WeaponData weaponData, LivingUnit target, float damage)
    {
        target.DropIncrease = 3;
        return damage;
    }
}

public delegate float EmotionAction(WeaponData weaponData, LivingUnit target, float damage);
