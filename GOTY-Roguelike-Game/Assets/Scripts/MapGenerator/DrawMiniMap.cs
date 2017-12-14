using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class DrawMiniMap : MonoBehaviour {

	public GameObject wallIcon;
	public GameObject floorIcon;
	public GameObject playerIcon;
	private GameObject player;
	private GameObject playerToken;
	private GameObject map;
	private MiniMapNode[,] mapNodeArr;
	private bool active = true;
	private GameObject thiscanvas;

	public class MiniMapNode
	{
		public GameObject floor;
		public List<GameObject> walls { get; set; }

		public MiniMapNode (GameObject floor, List<GameObject> walls)
		{
			this.floor = floor;
			this.walls = walls;
		}

		public MiniMapNode (GameObject floor)
		{
			this.floor = floor;
			this.walls = null;
		}

		public void makeVisible(){
			this.floor.transform.GetComponent<RawImage>().color = new Color(255f,255f,255f,.3f);
			if (walls != null) {
				foreach (GameObject thiswall in walls){
					thiswall.transform.GetComponent<RawImage> ().color = new Color (255f, 255f, 255f, .8f);
				}
			}
		}
	}


	Vector3 v3;
	public void Draw(string[,] maparr){
		map = GameObject.Find ("MiniMap");
		GameObject toInstantiate = null;
		GameObject instance = null;
	
		mapNodeArr = new MiniMapNode[ maparr.GetLength(0),  maparr.GetLength(1)];
		for (int i = 0; i < maparr.GetLength (0); i++) {
			for (int j = 0; j < maparr.GetLength (1); j++) {
				mapNodeArr [i, j] = new MiniMapNode (null);
			}
		}

		Vector3 v3;
		float offset = 16f;
		float offset2 = 8f;

		for (int i = 0; i < maparr.GetLength(0); i++) {
			for (int j = 0; j < maparr.GetLength (1); j++) {
				toInstantiate = wallIcon;


				List<GameObject> walls = new List<GameObject> ();

				if (maparr [i, j] == "hall") {
					//Left wall
					if (i - 1 >= 0 && maparr [i - 1, j] == "wall") {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset, j * offset, 0f);
						walls.Add (instance);
					}
					//Right wall
					if ((i + 1 < maparr.GetLength (0) && maparr [i + 1, j] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset, j * offset, 0f);
						walls.Add (instance);
					}
					//Above wall
					if ((j - 1 >= 0 && maparr [i, j - 1] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset - offset2, 0f);
						walls.Add (instance);
					}
					//Below wall
					if ((j + 1 < maparr.GetLength (1) && maparr [i, j + 1] == "wall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset + offset2, 0f);
						walls.Add (instance);
					}
				}

				if (maparr [i, j] == "room") {
					//Left wall
					if (i - 1 >= 0 && maparr [i - 1, j] == "wall" || maparr [i - 1, j] == "hall") {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset, j * offset, 0f);
						walls.Add (instance);
					}
					//Right wall
					if ((i + 1 < maparr.GetLength (0) && maparr [i + 1, j] == "wall" || maparr [i + 1, j] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset, j * offset, 0f);
						walls.Add (instance);
					}
					//Above wall
					if ((j - 1 >= 0 && maparr [i, j - 1] == "wall" || maparr [i, j - 1] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset - offset2, 0f);
						walls.Add (instance);
					}
					//Below wall
					if ((j + 1 < maparr.GetLength (1) && maparr [i, j + 1] == "wall" || maparr [i, j + 1] == "hall")) {
						instance = Instantiate (toInstantiate, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0, 0, 90f)) as GameObject;
						instance.transform.SetParent (map.transform);
						instance.transform.localPosition = new Vector3 (i * offset + offset2, j * offset + offset2, 0f);
						walls.Add (instance);

					}
				}

				if (maparr [i, j] != "wall") {
					instance = Instantiate (floorIcon, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (map.transform);
					instance.transform.localPosition = new Vector3 (i * offset + 7f, j * offset + 0f, 0f);
					if (walls == null) {
						mapNodeArr [i,j] = new MiniMapNode (instance);
					} else {
						mapNodeArr [i,j] = new MiniMapNode (instance, walls);
					}
				}
			}

		}
	}


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("remy");
		playerToken = Instantiate (playerIcon, new Vector3 (0f, 0f, 0f), Quaternion.Euler(0f,0f,0f)) as GameObject;
		playerToken.transform.SetParent (map.transform);
		thiscanvas = GameObject.Find ("Map");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab)) {
			if (this.GetComponentInParent<CanvasGroup> ().alpha == 0)
				this.GetComponentInParent<CanvasGroup> ().alpha = 1;
			else
				this.GetComponentInParent<CanvasGroup> ().alpha = 0;

		}

		Vector3 playerpos = player.transform.position;
		playerToken.transform.localPosition = new Vector3 (playerpos.x * 2f + 16/thiscanvas.GetComponent<RectTransform>().rect.width + 8f, playerpos.z * 2f + 16/thiscanvas.GetComponent<RectTransform>().rect.height, 0);
		playerToken.transform.rotation = Quaternion.Euler(0f,0f,  -player.transform.rotation.eulerAngles.y);


		int offset =12;

		for (int i = -3; i <= 3; i++) {
			for (int j = -3; j <= 3; j++) {
				try{
					mapNodeArr[(int)Math.Round((playerpos.x / 8) + i), (int)Math.Round(playerpos.z / 8) + j].makeVisible();
				}
				catch{

				}
			}
		}
	}
}
