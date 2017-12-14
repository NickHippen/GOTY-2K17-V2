using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;					  // the world-relative desired move direction, calculated from the camForward and user input.
		private bool m_Jump;
		private bool m_Attacking;
		private bool[] m_Abilities = new bool[4];
		private GameObject pauseHUD;

        private void Start()
        {
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
			m_Cam = Camera.main.transform;
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

			m_Attacking = Input.GetMouseButton (0);
			m_Abilities[0] = Input.GetKeyDown (KeyCode.Z);
			m_Abilities[1] = Input.GetKeyDown (KeyCode.X);
			m_Abilities[2] = Input.GetKeyDown (KeyCode.C);
			m_Abilities[3] = Input.GetKeyDown (KeyCode.V);
            m_Character.isUse(Input.GetKeyDown(KeyCode.E));
            m_Character.drop(Input.GetKeyDown(KeyCode.Q));

            if (!m_Character.gameObject.GetComponent<PlayerInventory>().isEmpty())
            {
                m_Character.gameObject.GetComponent<PlayerInventory>().getCurrentWeapon().transform.localPosition = new Vector3(0, 0, 0);
            }
            float scroll = Input.GetAxis("Mouse ScrollWheel");
			if (Input.anyKeyDown || scroll != 0) {
				WeaponSelect (scroll);
			}
            WeaponData currentWeapon = m_Character.gameObject.GetComponent<PlayerInventory>().getCurrentWeapon().GetComponent<WeaponData>();
            if (m_Attacking && (currentWeapon is GunData || currentWeapon is DaggerData)) {
				m_Character.ProcessAttack();
			}
			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (pauseHUD == null) {
					pauseHUD = Instantiate(Resources.Load("PauseStuff")) as GameObject;
					pauseHUD.name = "PauseStuff";
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;

					string remyClass = GameObject.Find ("remy").GetComponent<AbilityController> ().classType;

					if (remyClass == "berserker") {
						GameObject.Find ("BerserkerPause").GetComponent<CanvasGroup> ().alpha = 1;
					}
					if (remyClass == "gunslinger") {
						GameObject.Find ("GunslingerPause").GetComponent<CanvasGroup> ().alpha = 1;
					}
					if (remyClass == "wizard") {
						GameObject.Find ("WizardPause").GetComponent<CanvasGroup> ().alpha = 1;
					}
					if (remyClass == "rogue") {
						GameObject.Find ("RoguePause").GetComponent<CanvasGroup> ().alpha = 1;
					}

					Time.timeScale = 0f;
				} else {
					Destroy(pauseHUD);
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
					Time.timeScale = 1f;
					GameObject.Find ("BerserkerPause").GetComponent<CanvasGroup> ().alpha = 1;
					GameObject.Find ("GunslingerPause").GetComponent<CanvasGroup> ().alpha = 1;
					GameObject.Find ("WizardPause").GetComponent<CanvasGroup> ().alpha = 1;
					GameObject.Find ("RoguePause").GetComponent<CanvasGroup> ().alpha = 1;
				}
			}
        }

        // frames of sword attacks call this function
        public void AttackOnFrame()
        {
            m_Character.ProcessAttack();
        }

        //Changes selected weapon based on key input or from scrolled wheel
        private void WeaponSelect(float scrolled){
			int selection = -1;
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				selection = 0;
				m_Character.gameObject.GetComponent<PlayerInventory> ().setCurrentWeapon (selection);
			}else if(Input.GetKeyDown(KeyCode.Alpha2)){
				selection = 1;
				m_Character.gameObject.GetComponent<PlayerInventory> ().setCurrentWeapon (selection);
			}else if(Input.GetKeyDown(KeyCode.Alpha3)){
				selection = 2;
				m_Character.gameObject.GetComponent<PlayerInventory> ().setCurrentWeapon (selection);
			}else if(Input.GetKeyDown(KeyCode.Alpha4)){
				selection = 3;
				m_Character.gameObject.GetComponent<PlayerInventory> ().setCurrentWeapon (selection);
			}

			m_Character.gameObject.GetComponent<PlayerInventory> ().scrollWeapon (scrolled);

			if (scrolled != 0 || selection != -1) {
				m_Character.setWeaponAnimations();
			}
		}

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			//Temporary test of attack animation
//			m_Character.attack (Input.GetMouseButton(0));

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }

#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
			m_Character.Move(m_Move, m_Jump, m_Attacking, m_Abilities);
            m_Jump = false;
        }

    }
}
