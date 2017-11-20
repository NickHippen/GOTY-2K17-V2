using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Ability
{
    public float damage;
    public GrenadeObject grenadeObject;

    private GrenadeObject grenade;

    public override void applyEffect(GameObject player)
    {
        //if(!applyOnFrame)
        //{   // ability press, instantiate grenade
        //    applyOnFrame = true;
        //    grenade = Instantiate(grenadeObject);
        //}
        //else
        //{   // on frame, throw grenade
        //    applyOnFrame = false;
        //    grenade.Release();
        //}
    }
}
