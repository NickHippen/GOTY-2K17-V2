using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPrefabList : MonoBehaviour {
    
    public List<AbilityData> gunslingerList;
    public List<AbilityData> berserkerList;

	public List<AbilityData> getGunslingerAbilities()
    {
        List<AbilityData> list = new List<AbilityData>();
        foreach(AbilityData item in gunslingerList)
        {
            list.Add(Instantiate(item));
        }
        return list;
    }

    public List<AbilityData> getBerserkerAbilities()
    {
        // return an instantated list
        List<AbilityData> list = new List<AbilityData>();
        foreach (AbilityData item in berserkerList)
        {
            list.Add(Instantiate(item));
        }
        return list;
    }
}
