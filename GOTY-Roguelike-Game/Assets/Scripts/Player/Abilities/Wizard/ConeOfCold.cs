using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfCold : MonoBehaviour {

    float freezeDuration;
    bool bonusEffect;
    float bonusDuration;
    float bonusSlowPercent;

    public float FreezeDuration
    {
        get { return freezeDuration; }
        set { freezeDuration = value; }
    }
    public bool BonusEffect
    {
        get { return bonusEffect; }
        set { bonusEffect = value; }
    }
    public float BonusDuration
    {
        get { return bonusDuration; }
        set { bonusDuration = value; }
    }
    public float BonusSlowPercent
    {
        get { return bonusSlowPercent; }
        set { bonusSlowPercent = value; }
    }

    void OnTriggerEnter(Collider collision)
    {
        RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
        if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
        {
            AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
            monster.ApplyStatus(new StatusStun(monster, freezeDuration));
            if(bonusEffect)
            {
                monster.ApplyStatus(new StatusSlow(monster, bonusDuration, bonusSlowPercent));
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
        if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
        {
            AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
            monster.ApplyStatus(new StatusStun(monster, freezeDuration));
        }
    }
}
