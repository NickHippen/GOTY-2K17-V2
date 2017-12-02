using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfCold : MonoBehaviour {

    float freezeDuration;

    public float FreezeDuration
    {
        get { return freezeDuration; }
        set { freezeDuration = value; }
    }

    void OnTriggerEnter(Collider collision)
    {
        RigCollider rigCollider = collision.gameObject.GetComponent<RigCollider>();
        if (rigCollider != null && !(rigCollider is AttackCollider) && rigCollider.RootUnit is AggressiveUnit)
        {
            AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
            monster.ApplyStatus(new StatusStun(monster, freezeDuration));
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
