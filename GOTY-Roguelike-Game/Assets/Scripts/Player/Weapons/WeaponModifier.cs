using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum WeaponModifier {

	None=0, Weak, Strong, Siphon, Light, Heavy, Karma, Focused

}

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

	public static float ApplySiphon(WeaponData weaponData, LivingUnit target, float damage) {
		// TODO Weapons need reference to user
		return damage;
	}

	public static float ApplyKarma(WeaponData weaponData, LivingUnit target, float damage) {
		// TODO Weapons need reference to user
		return damage;
	}

	public static float ApplyFocused(WeaponData weaponData, LivingUnit target, float damage) {
		if (Random.Range(0f, 1f) < 0.35f) {
			damage *= 2f;
		}
		return damage;
	}

}

public delegate float ModifierAction(WeaponData weaponData, LivingUnit target, float damage);
