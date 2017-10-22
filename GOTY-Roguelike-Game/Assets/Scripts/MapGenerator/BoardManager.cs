﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	public GameObject[] walls;
	public GameObject[] doors;
	public GameObject[] monsters;
	public GameObject torch;

	public GameObject player;
	public DrawMiniMap miniMap;

	static int mapw = 60;
	static int maph = 40;
	public GenerateMap mapGenerator;

	private Map mymap;
	string[,] maparr;
	List<Room> roomList;

	private Transform boardHolder;
	private Transform monsterHolder;

	private List <Vector3> gridPositions = new List<Vector3> ();
	private int tilesize = 8;

	private float walloffset = 3.72f;
	private float torchoffset = 3.44f;
	void Start(){

	}

	//Creates a board object. Places cubes as children of that board object based on our map array
	void BoardSetup ()
	{
		mymap = mapGenerator.generate (mapw, maph);
		maparr = mymap.maparr;
		roomList = mymap.roomList;
		GameObject instance;

		boardHolder = new GameObject ("Board").transform;
		monsterHolder = new GameObject ("Monsters").transform;
		for (int i = 0; i < mapw; i++) {
			for (int j = 0; j < maph; j++) {
				int index = 0;
				if (maparr [i, j] == "hall")
					index = 2;
				if (maparr [i, j] == "door")
					index = 2;
				if (maparr [i, j] == "room")
					index = 2;
				GameObject toInstantiate = walls [index];
				instance = Instantiate (toInstantiate, new Vector3 (i * tilesize, 0f, j * tilesize), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);

				if (maparr [i, j] == "wall")
					continue;
				if ((i < mapw - 1 && maparr[i + 1, j] == "wall") || (i < mapw - 1 && maparr[i,j] == "room" && maparr[i+1,j] == "hall")) {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * tilesize + walloffset, -0.2f, j * tilesize), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					if (j % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize + torchoffset, 3f, j * tilesize), Quaternion.identity) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}
				if ((i > 0 && maparr[i - 1, j] == "wall")  || (i > 0 && maparr[i,j] == "room" && maparr[i-1,j] == "hall")) {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * tilesize - walloffset, -0.2f, j * tilesize), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					if (j % 2 == 0) {
						instance = Instantiate (torch,  new Vector3 (i * tilesize - torchoffset, 3f, j * tilesize), Quaternion.identity) as GameObject;
						instance.transform.SetParent (boardHolder);
					}
				}
				if ((j < maph - 1 && maparr[i, j + 1] == "wall")  || (j < maph - 1 && maparr[i,j] == "room" && maparr[i,j+1] == "hall")){
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * tilesize, -0.2f, j * tilesize + walloffset), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					if (j % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize, 3f, j * tilesize + torchoffset), Quaternion.Euler(0,90,0)) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}
				if ((j > 0 && maparr [i, j - 1] == "wall")  || (j > 0 && maparr[i,j] == "room" && maparr[i,j-1] == "hall")) {
					toInstantiate = walls [1];
					instance = Instantiate (toInstantiate, new Vector3 (i * tilesize, -0.2f, j * tilesize - walloffset), Quaternion.Euler(0,90,0)) as GameObject;
					instance.transform.SetParent(boardHolder);
					if (j % 2 == 0) {
						instance = Instantiate(torch, new Vector3 (i * tilesize, 3f, j * tilesize - torchoffset), Quaternion.Euler(0,90,0)) as GameObject;
						instance.transform.SetParent(boardHolder);
					}
				}
				//Vector3 position = new Vector3 (j, 0, i);
				//Instantiate (walls[0], position, Quaternion.identity);
			}
		}

		//Spawn player
		int randomSpot = Random.Range (0,roomList.Count);
		Spawn (roomList [randomSpot].startx, roomList [randomSpot].starty, player);
		GameObject.Find("MiniMap").GetComponent<DrawMiniMap>().Draw (maparr);

		foreach (Room room in roomList) {
			int randomMonster = Random.Range (0, monsters.Length);
			instance = Instantiate (monsters[randomMonster], new Vector3 (room.startx * tilesize, 2f, room.starty * tilesize), Quaternion.Euler(0,90,0)) as GameObject;
			instance.transform.SetParent(monsterHolder);
		}
	}

	void Spawn(int x, int z, GameObject tospawn){
		GameObject instance = Instantiate (tospawn, new Vector3 (x * tilesize, .6f, z * tilesize), Quaternion.identity) as GameObject;
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
	
