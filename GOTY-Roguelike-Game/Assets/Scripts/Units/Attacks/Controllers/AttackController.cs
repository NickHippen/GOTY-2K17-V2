using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackController : MonoBehaviour {

	/**
	 * <summary>Determines whether or not this controller should be triggered</summary>
	 * <returns>whether or not the controller should be triggered</returns>
	 * <remarks>Assumes the check is called every frame</remarks>
	 */
	public abstract bool Check();
	
}
