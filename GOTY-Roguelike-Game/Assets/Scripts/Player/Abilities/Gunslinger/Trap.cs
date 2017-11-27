using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Ability
{
    public TrapObject trapObject;
    public float trapDuration = 300f;
    public float trapDiameter = 1f;
    public float slowDuration = 5f;
    public float slowPercent = .7f;
    public int durability = 10;

    public override void applyEffect(GameObject player)
    {
        TrapObject trap = Instantiate(trapObject);
        trap.Duration = trapDuration;
        trap.Diameter = trapDiameter;
        trap.SlowDuration = slowDuration;
        trap.SlowPercent = slowPercent;
        trap.Durability = durability;
        trap.transform.position = player.transform.position;
        trap.transform.localScale = new Vector3(trapDiameter, .1f, trapDiameter);
    }
}
