using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum WeaponModifier {

	None=0, Weak, Strong, Siphon, Light/*, Heavy*/, Karma, Focused

}
// Weak and Strong are on create, Light is in WeaponData

public class WeaponModifierActionHandler {

	public static EmotionAction GetOnDamageAction(WeaponModifier modifier) {
		MethodInfo method = (new WeaponEmotionActionHandler()).GetType().GetMethod("Apply" + modifier.ToString());
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

	public static float ApplySiphon(WeaponData weaponData, LivingUnit target, float damage)
    {
        if (weaponData.RandomChanceHit(0.2f)) weaponData.Player.GetComponent<HealthManager>().Heal(damage * 0.05f); // 25% chance to heal 5% of damage dealt
        return damage;
	}

	public static float ApplyKarma(WeaponData weaponData, LivingUnit target, float damage) {
        if (weaponData.RandomChanceHit(0.2f)) weaponData.Player.GetComponent<HealthManager>().Heal(3); // 25% chance take 3 damage on hit
        return damage;
	}

	public static float ApplyFocused(WeaponData weaponData, LivingUnit target, float damage) {
		if (Random.Range(0f, 1f) < 0.35f) { // 35% chance critical damage
			damage *= 2f;
		}
		return damage;
	}

}

public delegate float ModifierAction(WeaponData weaponData, LivingUnit target, float damage);
