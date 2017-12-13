using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffData : WeaponData {

    public float projectileDuration = 6f;
    public float projectileSpeed = 3000f;
    public float radius;
    public Vector2 effectPosition = new Vector2(0, 2);
    public float raycastRange = 100f;
    StaffProjectile staffProjectile;

	protected override void Start() {
		base.Start();
        staffProjectile = this.transform.GetChild(1).GetComponent<StaffProjectile>();
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(WaitForDrop());
    }

    public override void Attack()
    {
		base.Attack ();
        ThirdPersonCharacter player = this.GetComponentInParent<ThirdPersonCharacter>();
        StaffProjectile proj = Instantiate(staffProjectile).GetComponent<StaffProjectile>();
        proj.transform.position = player.transform.position + player.transform.forward * effectPosition.x + player.transform.up * effectPosition.y;
        proj.transform.rotation = player.transform.rotation;
        proj.Timer = projectileDuration;
        proj.Damage = damage;
        proj.Radius = radius;
        proj.DamageMultiplier = damageMultiplier;
        proj.StaffObject = this;
        proj.Emotion = emotion;
        proj.Modifier = modifier;
        proj.gameObject.SetActive(true);
		proj.Player = Player;

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Unwalkable", "Monster", "Ground");
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, raycastRange, layerMask))
        {
            Vector3 throwPointToHitPoint = hit.point - proj.transform.position;
            proj.GetComponent<Rigidbody>().AddForce(throwPointToHitPoint.normalized * projectileSpeed);
        }
        else
        {
            proj.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward) * projectileSpeed);
        }
    }
}
