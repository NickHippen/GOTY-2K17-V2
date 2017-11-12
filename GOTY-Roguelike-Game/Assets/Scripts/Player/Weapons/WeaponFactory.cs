using UnityEngine;
using UnityEngine.Assertions;

public class WeaponFactory {

	//public static WeaponData GetRandomWeapon(int level, float quality) {
	//	int roll = Random.Range(0, 2);
	//	switch (roll) {
	//		case 0:
	//			return GetRandomSword(level, quality);
	//		case 1:
	//			return GetRandomGun(level, quality);
	//		default:
	//			return null;
	//	}
	//}

	public static SwordData CreateRandomSword(GameObject swordBase, Vector3 position, int level, float quality) {
		GameObject swordObj = GameObject.Instantiate(swordBase, position, Quaternion.identity);
		SwordData swordData = swordObj.GetComponent<SwordData>();

		// Generate stats
		float damage = 10 + level * 5;
		
		ApplyBonuses(swordData, level, quality);
		switch (swordData.modifier) {
			case WeaponModifier.Weak:
				damage *= 0.9f; // 10% less damage
				break;
			case WeaponModifier.Strong:
				damage *= 1.3f; // 30% more damage
				break;
			case WeaponModifier.Karma:
				damage *= 1.5f; // 50% more damage (but health may be lost on hit)
				break;
		}

		swordData.damage = damage;

		return swordData;
	}

	public static GunData CreateRandomGun(GameObject gunBase, Vector3 position, int level, float quality) {
		GameObject gunObj = GameObject.Instantiate(gunBase, position, Quaternion.identity);
		GunData gunData = gunObj.GetComponent<GunData>();

		// Generate stats
		float damage = 4 + level * 2;
		float bulletsPerSecond = 2;

		ApplyBonuses(gunData, level, quality);
		switch (gunData.modifier) {
			case WeaponModifier.Weak:
				damage *= 0.9f; // 10% less damage
				break;
			case WeaponModifier.Strong:
				damage *= 1.3f; // 30% more damage
				break;
			case WeaponModifier.Karma:
				damage *= 1.5f; // 50% more damage (but health may be lost on hit)
				break;
		}
		switch (gunData.emotion) {
			case WeaponEmotion.Inspiration:
				bulletsPerSecond *= 1.3f; // 30% faster fire rate
				break;
		}
		
		gunData.damage = damage;
		gunData.bulletsPerSecond = bulletsPerSecond;

		return gunData;
	}

	private static WeaponData ApplyBonuses(WeaponData weaponData, int level, float quality) {
		int modifierCount = System.Enum.GetNames(typeof(WeaponModifier)).Length;
		WeaponModifier modifier = (WeaponModifier)Random.Range(0, modifierCount);

		int emotionCount = System.Enum.GetNames(typeof(WeaponEmotion)).Length;
		WeaponEmotion emotion = (WeaponEmotion)Random.Range(0, emotionCount);

		weaponData.modifier = modifier;
		weaponData.emotion = emotion;
		return weaponData;
	}

}
