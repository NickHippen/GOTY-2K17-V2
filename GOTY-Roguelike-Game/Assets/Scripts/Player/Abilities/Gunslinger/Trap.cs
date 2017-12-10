using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    float duration;
    float diameter;
    float slowDuration;
    float slowPercent;
    int durability;
    bool bonusEffect;
    float poisonDuration;
    float poisonDamage;
    float poisonTPS;

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
    public bool BonusEffect
    {
        get { return bonusEffect; }
        set { bonusEffect = value; }
    }
    public float PoisonDuration
    {
        get { return poisonDuration; }
        set { poisonDuration = value; }
    }
    public float PoisonDamage
    {
        get { return poisonDamage; }
        set { poisonDamage = value; }
    }
    public float PoisonTPS
    {
        get { return poisonTPS; }
        set { poisonTPS = value; }
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
            if(bonusEffect)
            {
                monster.ApplyStatus(new StatusPoison(monster, poisonDuration, poisonDamage, PoisonTPS));
            }
            durability--;
        }
    }
}
