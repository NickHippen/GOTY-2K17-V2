using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPrefabList : MonoBehaviour {
    
    public List<Ability> gunslingerList;
    public List<Ability> berserkerList;

	public List<Ability> getGunslingerAbilities()
    {
        List<Ability> list = new List<Ability>();
        foreach(Ability item in gunslingerList)
        {
            list.Add(Instantiate(item));
        }
        return list;
    }

    public List<Ability> getBerserkerAbilities()
    {
        // return an instantated list
        List<Ability> list = new List<Ability>();
        foreach (Ability item in berserkerList)
        {
            list.Add(Instantiate(item));
        }
        return list;
    }
}
