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

	public static SwordData CreateRandomSword(GameObject swordBase, Vector3 position, int level, float quality, Transform spawner, int minCost, int maxCost) {
		GameObject swordObj = GameObject.Instantiate(swordBase, position, Quaternion.identity);
		swordObj.transform.SetParent (spawner);
		SwordData swordData = swordObj.GetComponent<SwordData>();

        // Generate stats
        float damage = swordBase.GetComponent<SwordData>().damage;
        damage += damage * level * 0.2f; // 20% damage increase per level
		
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
		swordData.cost = Random.Range (minCost, maxCost+1);
		return swordData;
	}

	public static GunData CreateRandomGun(GameObject gunBase, Vector3 position, int level, float quality, Transform spawner, int minCost, int maxCost) {
		GameObject gunObj = GameObject.Instantiate(gunBase, position, Quaternion.identity);
		gunObj.transform.SetParent (spawner);
		GunData gunData = gunObj.GetComponent<GunData>();

		// Generate stats
		float damage = gunBase.GetComponent<GunData>().damage * gunBase.GetComponent<GunData>().bulletsPerSecond; // DPS
        float bulletsPerSecond = Random.Range(2, 7); // Randomize rate
        damage /= bulletsPerSecond; // calculate amount of damage per bullet to match DPS
        damage += damage * level * 0.2f; // 20% damage increase per level

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
		gunData.cost = Random.Range (minCost, maxCost+1);
		return gunData;
	}

	public static StaffData CreateRandomStaff(GameObject staffBase, Vector3 position, int level, float quality, Transform spawner, int minCost, int maxCost)
    {
        GameObject staffObj = GameObject.Instantiate(staffBase, position, Quaternion.identity);
        staffObj.transform.SetParent(spawner);
        StaffData staffData = staffObj.GetComponent<StaffData>();

        // Generate stats
        float damage = staffBase.GetComponent<StaffData>().damage;
        damage += damage * level * 0.2f; // 20% damage increase per level

        ApplyBonuses(staffData, level, quality);
        switch (staffData.modifier)
        {
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

        staffData.damage = damage;
		staffData.cost = Random.Range (minCost, maxCost + 1);
        return staffData;
    }

	public static DaggerData CreateRandomDagger(GameObject daggerBase, Vector3 position, int level, float quality, Transform spawner, int minCost, int maxCost)
    {
        GameObject daggerObj = GameObject.Instantiate(daggerBase, position, Quaternion.identity);
        daggerObj.transform.SetParent(spawner);
        DaggerData daggerData = daggerObj.GetComponent<DaggerData>();

        // Generate stats
        float damage = daggerBase.GetComponent<DaggerData>().damage * daggerBase.GetComponent<DaggerData>().hitsPerSecond; // DPS
        float hitsPerSecond = Random.Range(4, 7); // Randomize rate
        damage /= hitsPerSecond; // calculate amount of damage per bullet to match DPS
        damage += damage * level * 0.2f; // 20% damage increase per level

        ApplyBonuses(daggerData, level, quality);
        switch (daggerData.modifier)
        {
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
        switch (daggerData.emotion)
        {
            case WeaponEmotion.Inspiration:
                hitsPerSecond *= 1.3f; // 30% faster slash rate
                break;
        }

        daggerData.damage = damage;
		daggerData.cost = Random.Range (minCost, maxCost+1);
        return daggerData;
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
