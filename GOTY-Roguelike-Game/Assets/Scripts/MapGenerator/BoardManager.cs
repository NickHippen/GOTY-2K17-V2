using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapNode
{
	public int x { get; set; }
	public int y { get; set; }
	public string dirpref { get; set; }
	public MapNode(int x, int y, string dirpref)
	{
		this.x = x;
		this.y = y;
		this.dirpref = dirpref;
	}

	public MapNode(int x, int y)
	{
		this.x = x;
		this.y = y;
		this.dirpref = "none";
	}
}

public class Room
{
	public int startx { get; set; }
	public int starty { get; set; }
	public int width { get; set; }
	public int height { get; set; }

	public Room(int x, int y, int w, int h)
	{
		this.startx = x;
		this.starty = y;
		this.width = w;
		this.height = h;
	}
}


public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public GameObject ground;
	public GameObject[] walls;
	public GameObject[] doors;
	static int mapw = 60;
	static int maph = 40;
	string[,] maparr = new string[mapw,maph];

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitialiseMap()
	{
		GenerateMap();
	}

	void BoardSetup()
	{
		boardHolder = new GameObject ("Board").transform;

		for (int i = 0; i < mapw; i++) {
			for (int j = 0; j < maph; j++) {
				int index = 0;
				if (maparr [i, j] == "hall")
					index = 1;
				if (maparr [i, j] == "door")
					index = 2;
				if (maparr [i, j] == "room")
					index = 3;
				GameObject toInstantiate = walls[index];
				GameObject instance = Instantiate(toInstantiate, new Vector3 (i,0f,j), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
					//Vector3 position = new Vector3 (j, 0, i);
					//Instantiate (walls[0], position, Quaternion.identity);

			}
		}
	}

	public void SetupScene(int level)
	{
		Random.seed = 0;
		InitialiseMap();
		BoardSetup();
	}

	MapNode CheckFree(MapNode currentnode)
	{
		var x = currentnode.x;
		var y = currentnode.y;
		var pref = currentnode.dirpref;

		List<string> randdir = new List<string>(new string[] {"up","down","left","right"});
		if (pref != "none")
		{
			for (int i = 0; i < 10; i++)
				randdir.Add(pref);
		}
		for (int i = 0; i < randdir.Count ; i++) {
			string temp = randdir[i];
			int randomIndex = Random.Range(i, randdir.Count-1);
			randdir[i] = randdir[randomIndex];
			randdir[randomIndex] = temp;
		}
		foreach (var dir in randdir)
		{
			switch (dir)
			{
			case "up":
				if (y <= 1) continue;
				if (maparr[x, y - 2] == "wall")
				{
					maparr[x, y - 1] = "hall";
					maparr[x, y - 2] = "hall";
					return new MapNode(x,y-2,"up");
				}
				break;
			case "down":
				if (y >= maph - 3) continue;
				if (maparr[x, y + 2] == "wall")
				{
					maparr[x, y + 1] = "hall";
					maparr[x, y + 2] = "hall";
					return new MapNode(x,y+2,"down");
				}
				break;
			case "left":
				if (x <= 1) continue;
				if (maparr[x - 2, y] == "wall")
				{
					maparr[x - 1, y] = "hall";
					maparr[x - 2, y] = "hall";
					return new MapNode(x - 2, y, "left");
				}
				break;
			case "right":
				if (x >=  mapw - 3) continue;
				if (maparr[x + 2, y] == "wall")
				{
					maparr[x + 1, y] = "hall";
					maparr[x + 2, y] = "hall";
					return new MapNode(x+2,y,"right");
				}
				break;
			default: break;
			}
		}
		return null;
	}

	void RemoveEnd(int x, int y)
	{
		var count = 0;
		if (x + 1 < mapw && (maparr[x + 1, y] == "hall" || maparr[x + 1, y] == "door")) count++;
		if (x - 1 > 0 && (maparr[x - 1, y] == "hall" || maparr[x - 1, y] == "door")) count++;
		if (y + 1 < maph && (maparr[x, y + 1] == "hall" || maparr[x, y + 1] == "door")) count++;
		if (y - 1 > 0 && (maparr[x, y - 1] == "hall" || maparr[x, y - 1] == "door")) count++;

		if (count <= 1 )
		{
			maparr[x,y] = "wall";
			if (x + 1 < mapw && maparr[x + 1, y] == "hall") RemoveEnd(x + 1, y);
			if (x - 1 > 0 && maparr[x - 1, y] == "hall") RemoveEnd(x - 1, y);
			if (y + 1 < maph && maparr[x, y + 1] == "hall") RemoveEnd(x, y + 1);
			if (y - 1 > 0 && maparr[x, y - 1] == "hall") RemoveEnd(x, y - 1);
		}
	}

	public void GenerateMap(){
		Random.seed = System.DateTime.Now.Millisecond;

		for (var i = 0; i < mapw; i++)
		{
			for (var j = 0; j < maph; j++)
			{
				maparr[i, j] = "wall";
			}
		}

		//Generate the rooms of the dungeon
		List<Room> roomList = new List<Room>();
		var room_place_attempts = 1000;
		for (var i = 1; i < room_place_attempts; i++)
		{
			int[] roomsize = {5, 7, 9};
			var width = roomsize[Random.Range(0,roomsize.Length-1)];
			var height = roomsize[Random.Range(0,roomsize.Length-1)];

			var xpos = Random.Range(0, mapw-1);
			var ypos = Random.Range(0, maph-1);

			//If the room would go outside of our borders, go to the next attempt
			if (width + xpos - 1 > mapw) goto Next;
			if (height + ypos - 1 > maph) goto Next;

			//If a room already exists here (or within roomdist) go to the next attempt
			var roomdist = 1;
			for (var j = xpos - roomdist; j < xpos + width + roomdist; j++)
			{
				for (var k = ypos - roomdist; k < ypos + height + roomdist; k++)
				{
					if (j < 0 || k < 0 || j >= mapw || k >= maph) goto Next;
					if (maparr[j, k] != "wall") goto Next;
				}
			}

			//Fill in the room
			for (var j = xpos; j < xpos + width; j++)
			{
				for (var k = ypos; k < ypos + height; k++)
				{
					maparr[j, k] = "room";
				}
			}
			roomList.Add(new Room(xpos,ypos,width,height));
			Next:;
		}
			
		//Gets our starting node

		var randw = Random.Range(0, mapw-1);
		var randh = Random.Range(0, maph-1);
		while (maparr[randw, randh] != "wall" || randh % 2 == 0 || randw % 2 == 0)
		{
			randw = Random.Range(0, mapw-1);
			randh = Random.Range(0, maph-1);
		}

		maparr[randw, randh] = "hall";
		Stack<MapNode> visited = new Stack<MapNode>();
		visited.Push(new MapNode(randw,randh,"none"));
		while (visited.Count > 0)
		{
			MapNode next = CheckFree(visited.Peek());
			if (next == null) visited.Pop();
			else visited.Push(next);
		}

		//Creates doors

		foreach (Room room in roomList)
		{
			List<MapNode> hallup = new List<MapNode>();
			List<MapNode> halldown = new List<MapNode>();
			List<MapNode> hallleft = new List<MapNode>();
			List<MapNode> hallright = new List<MapNode>();

			var offset = 1; //I don't want doors on the corner of rooms                
			for (int i = room.startx+offset; i < room.startx + room.width-offset; i++)
			{
				if(room.starty - 1 > 0 && maparr[i,room.starty-1] == "hall") hallup.Add(new MapNode(i, room.starty));
				if(room.starty+room.height+1 < maph && maparr[i,room.starty+room.height] == "hall") halldown.Add(new MapNode(i,room.starty+room.height-1));
			}
			for (int i = room.starty+offset; i < room.starty + room.height-offset; i++)
			{
				if(room.startx - 1 > 0 && maparr[room.startx-1,i] == "hall") hallleft.Add(new MapNode(room.startx, i));
				if(room.startx+room.width+1 < mapw && maparr[room.startx+room.width,i] == "hall") hallright.Add(new MapNode(room.startx+room.width-1,i));
			}

			string[] randdir = {"up", "down", "left", "right"};
			for (int i = 0; i < randdir.Length ; i++) {
				string temp = randdir[i];
				int randomIndex = Random.Range(i, randdir.Length-1);
				randdir[i] = randdir[randomIndex];
				randdir[randomIndex] = temp;
			}

			var count = 0;

			for (int i = 0; i < 4; i++)
			{
				var doorpos = -1;
				switch (randdir[i])
				{
				case "up":
					if (hallup.Count == 0) continue;
					doorpos = Random.Range(0, hallup.Count-1);
					maparr[hallup[doorpos].x,hallup[doorpos].y] = "door";
					count++;
					break;
				case "down":
					if (halldown.Count == 0) continue;
					doorpos = Random.Range(0, halldown.Count-1);
					maparr[halldown[doorpos].x,halldown[doorpos].y] = "door";
					count++;
					break;
				case "left":
					if (hallleft.Count == 0) continue;
					doorpos = Random.Range(0, hallleft.Count-1);
					maparr[hallleft[doorpos].x,hallleft[doorpos].y] = "door";
					count++;
					break;
				case "right":
					if (hallright.Count == 0) continue;
					doorpos = Random.Range(0, hallright.Count-1);
					maparr[hallright[doorpos].x,hallright[doorpos].y] = "door";
					count++;
					break;
				default: break;
				}
				if (count == 2) break;
			}
		}

		for (var i = 0; i < mapw; i++)
		{
			for (var j = 0; j < maph; j++)
			{
				if (maparr[i,j] == "hall") RemoveEnd(i,j);
			}
		}

		foreach (Room room in roomList)
		{
			var count = 0;
			for (int i = room.startx; i < room.startx + room.width; i++)
			{
				for (int j = room.starty; j < room.starty + room.height; j++)
				{
					if (maparr[i, j] == "door") count++;
				}
			}

			if (count != 0) continue;
			for (int i = room.startx; i < room.startx + room.width; i++)
			{
				for (int j = room.starty; j < room.starty + room.height ; j++)
				{
					maparr[i, j] = "wall";
				}
			}
		}
	}
}
	
