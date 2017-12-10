using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffProjectile : MonoBehaviour
{
    ParticleSystem missileParticle;
    float timer;
    float damage;
    float radius;
    bool hit;
    float damageMultiplier;
    StaffData staff;
    WeaponEmotion emotion;
    WeaponModifier modifier;
    

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }
    public float DamageMultiplier
    {
        get { return damageMultiplier; }
        set { damageMultiplier = value; }
    }
    public StaffData StaffObject
    {
        get { return staff; }
        set { staff = value; }
    }
    public WeaponEmotion Emotion
    {
        get { return emotion; }
        set { emotion = value; }
    }
    public WeaponModifier Modifier
    {
        get { return modifier; }
        set { modifier = value; }
    }

    void Start()
    {
        missileParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        StartCoroutine(Active());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") || other.gameObject.layer == LayerMask.NameToLayer("Unwalkable") ||
            other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            
            missileParticle.transform.gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = new Vector3();

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
            foreach (Collider collider in colliders)
            {
                RigCollider rigCollider = collider.gameObject.GetComponent<RigCollider>();
                if (rigCollider != null && rigCollider.RootUnit is AggressiveUnit)
                {
                    AggressiveUnit monster = ((AggressiveUnit)rigCollider.RootUnit);
                    float damage = this.damage;
                    damage *= damageMultiplier;
                    damage = WeaponEmotionActionHandler.GetOnDamageAction(emotion)(staff, monster, damage);
                    damage = WeaponModifierActionHandler.GetOnDamageAction(modifier)(staff, monster, damage);
                    monster.Damage(damage);
                }
            }
            Remove();
        }
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(timer);
        Remove();
    }

    void Remove()
    {
        Destroy(this.gameObject);
    }
}
