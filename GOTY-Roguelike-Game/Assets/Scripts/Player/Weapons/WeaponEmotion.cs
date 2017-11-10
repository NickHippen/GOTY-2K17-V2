using System;
using System.Reflection;
using UnityEngine;

public enum WeaponEmotion {

	None=0, Sorrow, Isolation, Rage, Inspiration, Compassion, Confidence, Shame, Anxiety, Envy, Elation

}

public class WeaponEmotionActionHandler {

	public static EmotionAction GetAction(WeaponEmotion emotion) {
		MethodInfo method = (new WeaponEmotionActionHandler()).GetType().GetMethod("Apply" + emotion.ToString());
		return (WeaponData weaponData, LivingUnit target, float damage) => {
			return (float)method.Invoke(null, new object[] { weaponData, target, damage });
		};
	}

	public static float ApplyNone(WeaponData weaponData, LivingUnit target, float damage) {
		return damage;
	}

	public static float ApplyAnxiety(WeaponData weaponData, LivingUnit target, float damage) {
		target.ApplyStatus(new StatusPoison(target, 2f, 3f, 1));
		return damage;
	}

}

public delegate float EmotionAction(WeaponData weaponData, LivingUnit target, float damage);