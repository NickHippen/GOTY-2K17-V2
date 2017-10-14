using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	public GameObject[] walls;
	public GameObject[] doors;
	public GameObject player;
	static int mapw = 60;
	static int maph = 40;
	public GenerateMap mapGenerator;

	private Map mymap;
	string[,] maparr;
	List<Room> roomList;

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3> ();

	void Start(){

	}

	//Creates a board object. Places cubes as children of that board object based on our map array
	void BoardSetup ()
	{
		mymap = mapGenerator.generate (mapw, maph);
		maparr = mymap.maparr;
		roomList = mymap.roomList;

		boardHolder = new GameObject ("Board").transform;
		for (int i = 0; i < mapw; i++) {
			for (int j = 0; j < maph; j++) {
				Debug.Log (i + " " + j);
				int index = 0;
				if (maparr [i, j] == "hall")
					index = 2;
				if (maparr [i, j] == "door")
					index = 2;
				if (maparr [i, j] == "room")
					index = 2;
				GameObject toInstantiate = walls [index];
				GameObject instance = Instantiate (toInstantiate, new Vector3 (i * 5, 0f, j * 5), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);

				if (maparr [i, j] == "wall")
					continue;
				if (i < mapw - 1 && maparr[i + 1, j] == "wall") {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * 5 + 2.2f, -0.2f, j * 5), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if (i > 0 && maparr[i - 1, j] == "wall") {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * 5 - 2.2f, -0.2f, j * 5), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if (j < maph - 1 && maparr[i, j + 1] == "wall") {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * 5, -0.2f, j * 5 + 2.2f), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				if (j > 0 && maparr [i, j - 1] == "wall") {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * 5, -0.2f, j * 5 - 2.2f), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				//Vector3 position = new Vector3 (j, 0, i);
				//Instantiate (walls[0], position, Quaternion.identity);
			}
		}

		//Spawn player
		int randomSpot = Random.Range (0,roomList.Count);

		Spawn (roomList [randomSpot].startx, roomList [randomSpot].starty, player);

	}

	void Spawn(int x, int z, GameObject tospawn){
		GameObject instance = Instantiate (tospawn, new Vector3 (x * 5, .5f, z * 5), Quaternion.identity) as GameObject;
		GameObject mycam = GameObject.Find ("Main Camera");
		mycam.GetComponent<CameraController> ().lookAt = instance.transform;
	}

	//Creates a new map and builds the scene with our objects
	public void SetupScene (int level)
	{
		Random.seed = 0;
		BoardSetup ();
	}
}
	
