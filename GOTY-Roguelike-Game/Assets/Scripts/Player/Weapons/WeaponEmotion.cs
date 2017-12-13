using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum WeaponEmotion {

	None=0, Sorrow, Isolation, Rage, Inspiration, Compassion, Confidence, Shame, Anxiety, Envy, Elation

}
// Inpiration and Compassion are applied in WeaponData

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
		target.ApplyStatus(new StatusSlow(target, 2f, 0.2f));
		return damage;
	}

	public static float ApplyIsolation(WeaponData weaponData, LivingUnit target, float damage) {
        float chance;
        if (weaponData is GunData || weaponData is DaggerData)
        {
            chance = 0.075f;
        }
        else
        {
            chance = .25f;
        }
        if (UnityEngine.Random.Range(0f, 1f) < chance)
        {
            target.ApplyKnockback(3f);
        }
		return damage;
	}

	public static float ApplyRage(WeaponData weaponData, LivingUnit target, float damage) {
        target.ApplyStatus(new StatusVulnerable(target, 2f, 1.1f), false);
		return damage;
	}

	public static float ApplyAnxiety(WeaponData weaponData, LivingUnit target, float damage) {
		target.ApplyStatus(new StatusPoison(target, 2f, 3f, 1), false);
		return damage;
	}

	public static float ApplyEnvy(WeaponData weaponData, LivingUnit target, float damage) {
		float chance;
		if (weaponData is GunData || weaponData is DaggerData) {
			chance = 0.015f;
		} else {
			chance = 0.05f;
		}
		if (UnityEngine.Random.Range(0f, 1f) < chance) {
			target.ApplyStatus(new StatusStun(target, 1f), false);
		}
		return damage;
	}

    public static float ApplyElation(WeaponData weaponData, LivingUnit target, float damage)
    {
        target.DropIncrease = 3;
        return damage;
    }
}

public delegate float EmotionAction(WeaponData weaponData, LivingUnit target, float damage);
