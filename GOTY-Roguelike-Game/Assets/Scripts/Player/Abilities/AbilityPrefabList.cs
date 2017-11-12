using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPrefabList : MonoBehaviour {
    
    public List<AbilityData> gunslingerList;
    public List<AbilityData> berserkerList;

	public List<AbilityData> getGunslingerAbilities()
    {
        return gunslingerList;
    }

    public List<AbilityData> getBerserkerAbilities()
    {
        return berserkerList;
    }
}
