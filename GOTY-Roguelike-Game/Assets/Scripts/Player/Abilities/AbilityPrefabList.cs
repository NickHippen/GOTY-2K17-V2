using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPrefabList : MonoBehaviour {
    
    public List<Ability> gunslingerList;
    public List<Ability> berserkerList;

	public List<Ability> getGunslingerAbilities(Transform parent)
    {
        List<Ability> list = new List<Ability>();
        foreach(Ability item in gunslingerList)
        {
            Ability clone = Instantiate(item);
            clone.name = item.name;
            clone.transform.parent = parent;
            list.Add(clone);
        }
        return list;
    }

    public List<Ability> getBerserkerAbilities(Transform parent)
    {
        // return an instantated list
        List<Ability> list = new List<Ability>();
        foreach (Ability item in berserkerList)
        {
            Ability clone = Instantiate(item);
            clone.name = item.name;
            clone.transform.parent = parent;
            list.Add(clone);
        }
        return list;
    }
}
