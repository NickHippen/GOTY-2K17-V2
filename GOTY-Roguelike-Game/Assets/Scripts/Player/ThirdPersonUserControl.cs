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
		private bool m_Ability1, m_Ability2, m_Ability3, m_Ability4;
        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

			m_Attacking = Input.GetMouseButton (0);
			m_Ability1 = Input.GetKeyDown (KeyCode.Z);
			m_Ability2 = Input.GetKeyDown (KeyCode.X);
			m_Ability3 = Input.GetKeyDown (KeyCode.C);
			m_Ability4 = Input.GetKeyDown (KeyCode.V);

			if (!m_Character.gameObject.GetComponent<PlayerInventory>().isEmpty ()) {
				m_Character.gameObject.GetComponent<PlayerInventory>().getCurrentWeapon ().transform.localPosition = new Vector3 (0, 0, 0);
			}
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			//Debug.Log (scroll);
			if (Input.anyKeyDown || scroll != 0) {
				//Debug.Log ("Pressed");
				WeaponSelect (scroll);
			}
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

			if (selection != -1 || scrolled != 0) {
				m_Character.setAnimatorController ();
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

			m_Character.isUse(Input.GetKeyDown(KeyCode.E));
			m_Character.drop (Input.GetKeyDown (KeyCode.Q));

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
			m_Character.Move(m_Move, m_Jump, m_Attacking, m_Ability1, m_Ability2, m_Ability3, m_Ability4);
            m_Jump = false;
        }
    }
}
