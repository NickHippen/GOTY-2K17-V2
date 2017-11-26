using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigCollider : MonoBehaviour {

	public GameObject RootObject { get; set; }
	public Unit RootUnit { get; set; }
	
	protected virtual void Start () {
		GameObject currObject = transform.gameObject;
		Unit unit = currObject.GetComponent<Unit>();
		while (unit == null) {
			currObject = currObject.transform.parent.gameObject;
			unit = currObject.GetComponent<Unit>();
		}
		RootObject = currObject;
		RootUnit = unit;
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		RootUnit.OnRigTriggerEnter(collider);
	}

}
