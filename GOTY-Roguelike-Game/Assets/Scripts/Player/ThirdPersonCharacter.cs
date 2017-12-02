using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonCharacter : MonoBehaviour
{
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.1f;

    //Game Object for players hand, used for equipping and the radius that a player can be
    //from an object and still pick it up
    public GameObject rightHand;
    public GameObject leftHand;
    public float grabRadius = 1f;

    Rigidbody m_Rigidbody;
	Animator m_Animator;
	bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_isDead;
	AbilityController abilities;
    Vector3 moveDirection;

	//Status of the Use Key 'E'
	bool m_Use;
	PlayerInventory inventory;

	void Start()
	{
		inventory = GetComponent<PlayerInventory>();
		abilities = GetComponent<AbilityController>();
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
        //Debug.Log (gunController);

        GameObject parent = new GameObject("Player");
        this.transform.SetParent(parent.transform);

		//Initialize the weapon held at the start of the game
		initializeEquip(inventory.getCurrentWeapon());
		inventory.getCurrentWeapon ().SetActive (true);
		setWeaponAnimations();
	}

    public Vector3 getMoveDirection()
    {
        print(moveDirection);
        return moveDirection;
    }

    public void Move(Vector3 move, bool jump, bool atk, bool[] abilityInputs)
    {
        moveDirection = move;
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired direction.
        if (move.magnitude > 1f)
            move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_ForwardAmount = move.z;

        // if attacking with ranged weapon, have camera over shoulder
        if (atk && inventory.getCurrentWeapon().GetComponent<WeaponData>() is GunData)
        {
            // apply turn amount relative to direction controls rather than cartesian
            m_TurnAmount = (move.z >= 0) ? Mathf.Atan2(move.x, move.z) : Mathf.Atan2(move.x, -move.z);
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {   // camera is independent of player rotation, use cartesian controls
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            ApplyExtraTurnRotation();
        }

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded) {
			HandleGroundedMovement (jump);
		} else {
			HandleAirborneMovement ();
		}

		// send input and other state parameters to the animator
		UpdateAnimator(move, atk, abilityInputs);
	}
			
	void UpdateAnimator(Vector3 move, bool atk, bool[] abilityInputs)
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool("OnGround", m_IsGrounded);
		m_Animator.SetBool("Attack", atk);

        // skip abilities if they are unavailible, else update bool, and only for one ability
        bool oneAbilityUsed = false;
        for(int i = 0; i < abilityInputs.Length; i++)
        {
            if (!abilityInputs[i] || oneAbilityUsed || (oneAbilityUsed = abilities.useAbility(i)))
            {
                m_Animator.SetBool("Ability"+(i+1), abilityInputs[i]);
            }
        }

        //m_Animator.SetBool ("Dead", m_isDead);

        if (!m_IsGrounded)
		{
			m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle = Mathf.Repeat(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		if (m_IsGrounded)
		{
			m_Animator.SetFloat("JumpLeg", jumpLeg);
		}

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}

	protected void OnAnimationComplete(AnimationEvent animationEvent) {
		if (animationEvent.stringParameter.StartsWith("reset_")) {
			string animationName = animationEvent.stringParameter.Substring(6);
			m_Animator.SetBool(animationName, false);
		}
	}


	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);

		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}


	void HandleGroundedMovement(bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_Animator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}
			
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}


	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}

	//Initiates the various functionality of the Use key when pressed
	public void isUse(bool E_Press) {
		m_Use = E_Press;
		if (m_Use) {
			Debug.Log("Pressed");
			pickupNearby(transform.position, grabRadius);
		}
	}

	/*This method is called when the use key is pressed. It returns an array of gameobjects located within a defined
	radius of the player location. The first object it finds with the tag "Pickup" (if any are found) are added
	to the player's inventory*/
	void pickupNearby(Vector3 center, float radius){
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);
		for (int x = 0; x < hitColliders.Length; x++) {
			Debug.Log (hitColliders [x]);
		}
		int i = 0;
		while (i < hitColliders.Length) {
			GameObject temp = hitColliders [i].gameObject;
			if (/*hitColliders != null &&*/ temp.CompareTag ("Pickup") && !inventory.isFull ()) {

				// If statements for actual weapon purchase
				if (temp.GetComponent<WeaponData> ().cost > GameObject.Find ("remy").GetComponent<PlayerInventory> ().gold) {
					break;
				} else {
					GameObject.Find ("remy").GetComponent<PlayerInventory> ().gold -= (int) temp.GetComponent<WeaponData> ().cost;
					GameObject.Find ("PlayerMoney").GetComponent<Text> ().text = GameObject.Find ("remy").GetComponent<PlayerInventory> ().gold.ToString ();
					GameObject.Find ("WeaponPickupMessage").GetComponent<Text> ().text = "";
				}

				if (temp.GetComponent<Floating> () != null) {
					temp.GetComponent<Floating> ().enabled = false;
				}
				initializeEquip (temp);
				Debug.Log (temp.name);
				inventory.addWeapon (temp);
				temp.gameObject.SetActive (false);
				//Move into Weapon class later
				inventory.getCurrentWeapon ().SetActive (true);
				setWeaponAnimations();
				break;
			}
			i++;
		}
	}

	/*Changes characteristics of an object that has been picked up or equipped. Most notably, setting the parents
	and transforms.*/
	void initializeEquip(GameObject temp){
		temp.tag = "Equipped";
		//temp.GetComponent<Rigidbody>().useGravity = false;
		GameObject hand;
		WeaponData weaponData = temp.GetComponent<WeaponData>();
		if (weaponData is GunData) {
			hand = rightHand;
		//} else if (temp.name.Contains ("sword")) {
		} else if (weaponData is SwordData) {
			hand = leftHand;
		} else {
			hand = rightHand;
		}
		temp.gameObject.transform.parent = hand.transform;
		temp.gameObject.transform.position = hand.transform.position;
		temp.gameObject.transform.rotation = hand.transform.rotation;
		//8, 83.5, 89
		if (weaponData is GunData) {
			//m_Animator.runtimeAnimatorController = gunController;
			//temp.gameObject.transform.localEulerAngles = temp.gameObject.GetComponent<WeaponData>().rotation;//new Vector3(8f, 83.5f, 89f);
			//inventory.setCurrentWeapon (editCollider (inventory.getCurrentWeapon (), false));
		}

	}

	/*Drops an item from the player inventory if the drop button has been pressed and there is more than
	one item in the inventory. The dropped item is changed to be a pickup again and can be re-picked up
	if so desired.*/
	public void drop(bool dropPress){
		if(dropPress && !inventory.lastItem()){
			inventory.dropCurrentWeapon ();
			setWeaponAnimations();
		}
	}

	/*Checks the characteristics of the currently held weapon and sets the appropriate animator controller to match it*/
	public void setWeaponAnimations(){
		if (inventory.getCurrentWeapon() != null) {
            WeaponData weapon = inventory.getCurrentWeapon().GetComponent<WeaponData>();
            if (weapon is GunData)
            {
                m_Animator.SetLayerWeight(1, 1); // turn on Gun weapon layer
                m_Animator.SetLayerWeight(2, 0); // turn off sword weapon layer
                m_Animator.SetLayerWeight(3, 0); // turn off staff weapon layer
            }
            else if(weapon is SwordData)
            {
                m_Animator.SetLayerWeight(2, 1); // turn on sword weapon layer
                m_Animator.SetLayerWeight(1, 0); // turn off gun weapon layer
                m_Animator.SetLayerWeight(3, 0); // turn off staff weapon layer
            }
            else
            {
                m_Animator.SetLayerWeight(3, 1); // turn on staff weapon layer
                m_Animator.SetLayerWeight(1, 0); // turn off gun weapon layer
                m_Animator.SetLayerWeight(2, 0); // turn off sword weapon layer
            }
        }
	}

	public Animator getAnimatorController() {
		return m_Animator;
	}
				
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
			m_IsGrounded = true;
			m_Animator.applyRootMotion = true;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}
	}

    public void ProcessAttack() {
		inventory.getCurrentWeapon().GetComponent<WeaponData>().Attack();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Potion"))
		{
			Destroy (other.gameObject);
			GameObject.Find ("remy").GetComponent<HealthManager> ().Heal (20);
		}

		if (other.gameObject.CompareTag ("Currency")) {
			Destroy (other.gameObject);
			GameObject.Find ("remy").GetComponent<PlayerInventory> ().gold += 1;
			GameObject.Find ("PlayerMoney").GetComponent<Text> ().text = GameObject.Find ("remy").GetComponent<PlayerInventory> ().gold.ToString ();
		}

		if (other.gameObject.CompareTag ("Pickup")) {
			GameObject.Find ("WeaponPickupMessage").GetComponent<Text> ().text = "Press \'E\' to pick up " + other.gameObject.GetComponent<WeaponData>().FullName;
			if (other.gameObject.GetComponent<WeaponData> ().cost > 0) {
				GameObject.Find ("WeaponPickupMessage").GetComponent<Text> ().text = "Press \'E\' to pick up " + other.gameObject.GetComponent<WeaponData>().FullName + "\nCosts " + other.gameObject.GetComponent<WeaponData> ().cost + " gems.";
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("Pickup")) {
			GameObject.Find ("WeaponPickupMessage").GetComponent<Text> ().text = "";
		}
	}
}
