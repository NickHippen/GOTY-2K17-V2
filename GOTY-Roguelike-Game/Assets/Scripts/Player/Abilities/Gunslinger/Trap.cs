using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private float duration;
    private float diameter;
    private float slowDuration;
    private float slowPercent;
    private int durability;

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public float Diameter
    {
        get { return diameter; }
        set { diameter = value; }
    }
    public float SlowDuration
    {
        get { return slowDuration; }
        set { slowDuration = value; }
    }
    public float SlowPercent
    {
        get { return slowPercent; }
        set { slowPercent = value; }
    }
    public int Durability
    {
        get { return durability; }
        set { durability = value; }
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0 || durability <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        RigCollider rigCollider = other.gameObject.GetComponent<RigCollider>();
        if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
        {
            AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
            monster.ApplyStatus(new StatusSlow(monster, slowDuration, slowPercent));
            durability--;
        }
    }
}
