using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory {

	public static SwordData GetRandomSword(int level, float quality) {
		SwordData swordData = new SwordData();
		int damage = 10 + level * 5;
		return swordData;
	}

}
