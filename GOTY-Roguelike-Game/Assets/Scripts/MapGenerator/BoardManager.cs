using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	public LevelManager levelmanager;

	public GameObject[] monsters;
	public GameObject boss;
	public GameObject bossTrigger;
	public GameObject[] items;
	public GameObject tile;
	public GameObject wall;
	public GameObject door;
	public GameObject shop;
	public GameObject torch;

	public GameObject exitPortal;
	public Texture texture;

	public DrawMiniMap miniMap;

	public int mapw = 60;
	public int maph = 40;
	public GenerateMap mapGenerator;

	private Map mymap;
	string[,] maparr;
	List<Room> roomList;

	private Transform boardHolder;
	private Transform monsterHolder;
	private Transform exitHolder;
	private Transform bossHolder;
	private Transform bossTriggerHolder;

	private AggressiveUnit spawnedBoss;
	private GameObject spawnedPortal;

	private List <Vector3> gridPositions = new List<Vector3> ();
	private int tilesize = 8;

	private float walloffset = 3.72f;
	private float torchoffset = 3.44f;
	void Start(){
		Random.seed = (int)System.DateTime.Now.Ticks;
	}

	//Creates a board object. Places cubes as children of that board object based on our map array
	void BoardSetup ()
	{
		BuildMap:
			mymap = mapGenerator.generate (mapw, maph);
			maparr = mymap.maparr;
			roomList = mymap.roomList;
		if (roomList.Count <= 6)
			goto BuildMap;

		GameObject instance;
		boardHolder = new GameObject ("Board").transform;
		monsterHolder = new GameObject ("Monsters").transform;
		exitHolder = new GameObject ("Exit Portal").transform;
		bossTriggerHolder = new GameObject("Boss Trigger").transform;

		for (int i = 0; i < mapw; i++) {
			for (int j = 0; j < maph; j++) {
				if (maparr [i, j] == "wall")
					continue;

				//Make a tile on the floor
				instance = Instantiate (tile, new Vector3 (i * tilesize, 0f, j * tilesize), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
				foreach (Renderer child in instance.GetComponentsInChildren<Renderer>()) {
					child.material.mainTexture = texture;
				}

				//East wall
				if ((i < mapw - 1 && maparr[i + 1, j] == "wall") || (i < mapw - 1 && maparr[i,j] == "room" && maparr[i+1,j] == "hall")) {
					instance = Instantiate (wall, new Vector3 (i * tilesize + walloffset, 3, j * tilesize), Quaternion.Euler(0,0,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;
					if ((i + j) % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize + torchoffset, 3f, j * tilesize), Quaternion.Euler(0,0,0)) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}
				//West wall
				if ((i > 0 && maparr[i - 1, j] == "wall")  || (i > 0 && maparr[i,j] == "room" && maparr[i-1,j] == "hall")) {
					instance = Instantiate (wall, new Vector3 (i * tilesize - walloffset, 3, j * tilesize), Quaternion.Euler(0,180,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;

					if ((i + j) % 2 == 0) {
						instance = Instantiate (torch,  new Vector3 (i * tilesize - torchoffset, 3f, j * tilesize), Quaternion.Euler(0,180,0))as GameObject;
						instance.transform.SetParent (boardHolder);
					}
				}

				//North wall
				if ((j < maph - 1 && maparr[i, j + 1] == "wall")  || (j < maph - 1 && maparr[i,j] == "room" && maparr[i,j+1] == "hall")){
					instance = Instantiate (wall, new Vector3 (i * tilesize, 3, j * tilesize + walloffset), Quaternion.Euler(0,-90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;

					if ((i + j) % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize, 3f, j * tilesize + torchoffset), Quaternion.Euler(0,-90,0)) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}

				//South wall
				if ((j > 0 && maparr [i, j - 1] == "wall")  || (j > 0 && maparr[i,j] == "room" && maparr[i,j-1] == "hall")) {
					instance = Instantiate (wall, new Vector3 (i * tilesize, 3, j * tilesize - walloffset), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;

					if ((i + j) % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize, 3f, j * tilesize - torchoffset), Quaternion.Euler(0,90,0)) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}

				//South door
				if (j > 0 && maparr [i, j - 1] == "door" && maparr[i,j] == "hall") {
					instance = Instantiate (door, new Vector3 (i * tilesize, -0.2f, j * tilesize - walloffset), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;
				}

				//West door
				if (i > 0 && maparr [i-1, j ] == "door" && maparr[i,j] == "hall") {
					instance = Instantiate (door, new Vector3 (i * tilesize - walloffset, -0.2f, j * tilesize), Quaternion.Euler(0,180,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;
				}

				//East door
				if (i < mapw-1 && maparr [i+1, j ] == "door" && maparr[i,j] == "hall") {
					instance = Instantiate (door, new Vector3 (i * tilesize + walloffset, -0.2f, j * tilesize), Quaternion.Euler(0,0,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;
				}

				//North door
				if (j < maph-1 && maparr [i, j + 1] == "door" && maparr[i,j] == "hall") {
					instance = Instantiate (door, new Vector3 (i * tilesize, -0.2f, j * tilesize + walloffset), Quaternion.Euler(0,-90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					instance.GetComponent<Renderer>().material.mainTexture = texture;
				}

				//Vector3 position = new Vector3 (j, 0, i);
				//Instantiate (walls[0], position, Quaternion.identity);
			}
		}

		//Spawn player
		Room playerRoom;
		int playerSpawn = Random.Range (0,roomList.Count);
		GameObject player = GameObject.Find ("Player");
		player.transform.position = new Vector3((roomList[playerSpawn].startx + (roomList[playerSpawn].width/2)) * tilesize, .6f, (roomList[playerSpawn].starty + (roomList[playerSpawn].height/2)) * tilesize);


		//GameObject mycam = GameObject.Find ("Main Camera");
		//mycam.GetComponent<CameraController> ().lookAt = player.transform;
		GameObject miniMap = GameObject.Find("MiniMap");
		if (miniMap != null) {
			miniMap.GetComponent<DrawMiniMap>().Draw(maparr);
		}
		playerRoom = roomList[playerSpawn];
		roomList.Remove(playerRoom);

		//Spawn shops
		int numShops = roomList.Count / 4;
		for (int i = 0; i < numShops; i++) {
			int shopSpawn = Random.Range (0, roomList.Count);
			Room shoproom = roomList [shopSpawn];
			instance = Instantiate(shop, new Vector3((shoproom.startx + shoproom.width / 2) * tilesize, 1f, (shoproom.starty + shoproom.height / 2) * tilesize), Quaternion.Euler(0,0,0));
			roomList.Remove (shoproom);
		}

		//Spawn exit portal
		//Spawn boss
		Room farthestRoom = roomList [0];
		float maxDist = 0;
		foreach (Room room in roomList) {
			float thisDist = Math.Abs (playerRoom.startx + playerRoom.width / 2 - room.startx + room.width / 2) + Math.Abs (playerRoom.startx + playerRoom.width / 2 - room.startx + room.width / 2);
			if (thisDist > maxDist) {
				maxDist = thisDist;
				farthestRoom = room;
			}
		}
		instance = Instantiate(boss, new Vector3((farthestRoom.startx + farthestRoom.width / 2) * tilesize, .26f, (farthestRoom.starty + farthestRoom.height / 2) * tilesize), Quaternion.Euler(0, 0, 0));
		spawnedBoss = (AggressiveUnit)instance.GetComponent<Unit>();
		instance.GetComponent<Unit>().pathRequestManager = GameObject.Find("A_").GetComponent<PathRequestManager>();
		instance.transform.SetParent(bossHolder);
		
		instance = Instantiate(exitPortal, new Vector3((farthestRoom.startx + farthestRoom.width / 2) * tilesize, .26f, (farthestRoom.starty + farthestRoom.height / 2) * tilesize), Quaternion.Euler(-90, 0, 0));
		instance.transform.SetParent(exitHolder);
		spawnedPortal = instance;
		spawnedPortal.SetActive(false);

		instance = Instantiate(bossTrigger, new Vector3((farthestRoom.startx + farthestRoom.width / 2) * tilesize, .26f, (farthestRoom.starty + farthestRoom.height / 2) * tilesize), Quaternion.Euler(-90, 0, 0));
		instance.transform.SetParent(bossTriggerHolder);
		instance.transform.localScale = new Vector3(farthestRoom.width * 8, farthestRoom.width * 8, 8); // 8 for full room trigger

		// Calculate pathfinding walk regions
		Grid grid = GameObject.Find("A_").GetComponent<Grid>();
		grid.CreateGrid();

		//Spawn monsters
		PathRequestManager prm = GameObject.Find("A_").GetComponent<PathRequestManager>();
		foreach (Room room in roomList) {
			int roomsize = room.width;
			if (room.height > roomsize)
				roomsize = room.height;
			int[] sizes = { roomsize, roomsize - 1, roomsize - 2 };
			int randomsize = Random.Range (0, 3);

			for (int i = 0; i < sizes [randomsize]; i++) {
				int randomMonster = Random.Range (0, monsters.Length);
				int randomx = Random.Range (0, room.width);
				int randomy = Random.Range (0, room.height);
				instance = Instantiate (monsters[randomMonster], new Vector3 ((room.startx + randomx) * tilesize, .2f, (room.starty + randomy) * tilesize), Quaternion.Euler(0,90,0)) as GameObject;
				instance.GetComponent<Unit> ().pathRequestManager = GameObject.Find ("A_").GetComponent<PathRequestManager>();
				instance.transform.SetParent(monsterHolder);
			}
		}
	}

	GameObject Spawn(int x, int z, GameObject tospawn){
		GameObject instance = Instantiate (tospawn, new Vector3 (x * tilesize, .6f, z * tilesize), Quaternion.identity) as GameObject;
		return instance;
	}

	//Creates a new map and builds the scene with our objects
	public void SetupScene (int level)
	{
		Random.seed = 0;
		BoardSetup ();
	}

	private GameObject remy;

	public void Update()
	{
		if (remy == null) {
			remy = GameObject.Find("Player");
		}
		Vector3 remyPos = remy.transform.localPosition;
		Vector3 exitPos = spawnedPortal.transform.GetChild(0).transform.localPosition;
		if (Math.Abs(remyPos.x - exitPos.x) + Math.Abs(remyPos.z - exitPos.z) < 1) {
			levelmanager.LoadNextLevel();
		}
		if (!spawnedBoss.Living) {
			spawnedPortal.SetActive(true);
		}
	}
}
	
