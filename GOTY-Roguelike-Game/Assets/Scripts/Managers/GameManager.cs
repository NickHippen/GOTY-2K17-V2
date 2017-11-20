using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	[HideInInspector] public bool playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 0;
	private bool enemiesMoving;
	private bool doingSetup;



	// Use this for initialization
	void Awake () 
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
	}

	//This is called each time a scene is loaded.

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
		//Add one to our level number.
		level++;
		//Call InitGame to initialize our level.
		InitGame();
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	public void InitGame()
	{
		doingSetup = true;
		boardScript.SetupScene(level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}

	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
		enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (playersTurn || enemiesMoving || doingSetup) return;
	}
}
