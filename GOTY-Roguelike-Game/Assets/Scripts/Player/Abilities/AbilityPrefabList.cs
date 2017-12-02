using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPrefabList : MonoBehaviour {
    
    public List<Ability> gunslingerList;
    public List<Ability> berserkerList;
    public List<Ability> wizardList;

	public List<Ability> getGunslingerAbilities(Transform parent)
    {
        List<Ability> list = new List<Ability>();
        foreach(Ability item in gunslingerList)
        {
            Ability clone = Instantiate(item, parent);
            clone.name = item.name;
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
            Ability clone = Instantiate(item, parent);
            clone.name = item.name;
            list.Add(clone);
        }
        return list;
    }

    public List<Ability> getWizardAbilities(Transform parent)
    {
        // return an instantated list
        List<Ability> list = new List<Ability>();
        foreach (Ability item in wizardList)
        {
            Ability clone = Instantiate(item, parent);
            clone.name = item.name;
            list.Add(clone);
        }
        return list;
    }
}
