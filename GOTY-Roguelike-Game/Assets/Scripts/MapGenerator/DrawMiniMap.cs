using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class DrawMiniMap : MonoBehaviour {

	public GameObject wallIcon;

	public GameObject playerIcon;

	Vector3 v3;
	public void Draw(string[,] maparr){
		GameObject map = GameObject.Find ("MiniMap");
		GameObject toInstantiate = null;
		GameObject instance = null;
	
		Vector3 v3;
		float offset = 10f;
		float offset2 = 5f;
		for (int i = 0; i < maparr.GetLength(0); i++) {
			for (int j = 0; j < maparr.GetLength (1); j++) {
				toInstantiate = wallIcon;

				if (maparr [i, j] == "hall") {
					//Left wall
					if (i - 1 >= 0 && maparr [i - 1, j] == "wall") {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset, j * offset, 0f);
					}
					//Right wall
					if ((i + 1 < maparr.GetLength (0) && maparr [i + 1, j] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset, j * offset, 0f);
					}
					//Above wall
					if ((j - 1 >= 0 && maparr [i, j - 1] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset - offset2, 0f);
					}
					//Below wall
					if ((j + 1 < maparr.GetLength (1) && maparr [i, j + 1] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset + offset2, 0f);
					}
				}

				if (maparr [i, j] == "room") {
					//Left wall
					if (i - 1 >= 0 && maparr [i - 1, j] == "wall" || maparr [i - 1, j] == "hall") {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset, j * offset, 0f);
					}
					//Right wall
					if ((i + 1 < maparr.GetLength (0) && maparr [i + 1, j] == "wall" || maparr [i + 1, j] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset, j * offset, 0f);
					}
					//Above wall
					if ((j - 1 >= 0 && maparr [i, j - 1] == "wall" || maparr [i, j - 1] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset - offset2, 0f);
					}
					//Below wall
					if ((j + 1 < maparr.GetLength (1) && maparr [i, j + 1] == "wall" || maparr [i, j + 1] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset + offset2, 0f);
					}
				}
			}

		}
		instance = Instantiate (playerIcon, new Vector3 (0 * offset, 0 * offset, 0f), Quaternion.identity) as GameObject;
		instance.transform.SetParent (map.transform);

	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 remyPos = GameObject.Find ("remy").transform.localPosition;
		GameObject instance;
		if (GameObject.Find ("playerIcon(Clone)") != null) {
			instance = GameObject.Find ("playerIcon(Clone)");
			instance.transform.localPosition = new Vector3 (remyPos.x * 5 / 8, remyPos.z * 5 / 8, 0f);
			v3 = instance.transform.position;
			v3.y -= 200;
			instance.transform.position = v3;
		}
	}
}
