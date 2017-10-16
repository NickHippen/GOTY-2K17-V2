using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawMiniMap : MonoBehaviour {

	public GameObject wallIcon;
	public GameObject doorIcon;
	public GameObject hallIcon;
	public GameObject roomIcon;
	public GameObject playerIcon;

	public void Draw(string[,] maparr){
		GameObject map = GameObject.Find ("MiniMap");
		GameObject toInstantiate = null;
		GameObject instance = null;
		Vector3 v3;
		for (int i = 0; i < 60; i++) {
			for (int j = 0; j < 40; j++) {
				switch (maparr [i, j]) {
				case "wall":
					toInstantiate = wallIcon;
					break;
				case "hall":
					toInstantiate = hallIcon;
					break;
				case "room":
					toInstantiate = roomIcon;
					break;
				case "door":
					toInstantiate = doorIcon;
					break;
				default:
					toInstantiate = wallIcon;
					break;
				}
				instance = Instantiate (toInstantiate, new Vector3 (i * 5f, j * 5f, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (map.transform);
				instance.transform.localPosition = new Vector3 (i * 5f, j * 5f, 0f);
				v3 = instance.transform.position;
				v3.y -= 200;
				instance.transform.position = v3;
			}
		}
		instance = Instantiate (playerIcon, new Vector3 (0 * 5f, 0 * 5f, 0f), Quaternion.identity) as GameObject;
		instance.transform.SetParent (map.transform);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 remyPos = GameObject.Find ("remy(Clone)").transform.localPosition;
		if (GameObject.Find ("playerIcon(Clone)") != null) {
			GameObject instance = GameObject.Find ("playerIcon(Clone)");
			instance.transform.localPosition = new Vector3 (remyPos.x * 5 / 8, remyPos.z * 5 / 8, 0f);
			Vector3 v3 = instance.transform.position;
			v3.y -= 200;
			instance.transform.position = v3;
		}
	}
}
