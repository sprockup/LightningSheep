using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private int currentScore;
	private int highScore;

	private int currentLevel = 0;
	private int unlockedLevel;

	public GUISkin skin;
	public Rect timerRect;
	public Rect sheepCountRect;
	public float startTime = 60.0f;
	private string currentTime;
	
	private bool showWinScreen = false;
	private int sheepCount;
	
	public ScoreManager scoreManager;
	public GameObject spawnLocation;
	
	public GameObject levelCompletePanel;
	
	void Start()
	{

		scoreManager = new ScoreManager();
		/**
		totalTokenCount = tokenParent.transform.childCount;
		currentLevel = PlayerPrefs.GetInt("Level Completed");
		if (PlayerPrefs.GetInt("Level Completed") <= 0)
		{
			currentLevel = 0;
		}
		*/
		
		// Start function to spawn sheep
		//StartCoroutine(SpawnSheep());
		
		levelCompletePanel.SetActive(false);
	}

	void Update()
	{		
		// Count the sheep
		GameObject[] sheep = GameObject.FindGameObjectsWithTag("Sheep");
		sheepCount = sheep.GetLength(0);
		((UILabel)GameObject.Find("Sheep Count Label").GetComponent<UILabel>()).text = sheepCount.ToString();
		((UILabel)GameObject.Find("Score Label").GetComponent<UILabel>()).text = ScoreManager.GetScore().ToString();
		((UILabel)GameObject.Find("Timer Label").GetComponent<UILabel>()).text = currentTime;
		
		if (startTime <= 0 || sheepCount <= 0)
		{
			CompleteLevel();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("main_menu"); 
		}
		
		if (showWinScreen)
		{
			// After its updated then set it to visible
			levelCompletePanel.SetActive(true);
		
			// Configure the end of level screen
			((UILabel)GameObject.Find("FinalScoreLabel").GetComponent<UILabel>()).text = 
				"Final Score: " + ScoreManager.GetScore().ToString();
			((UILabel)GameObject.Find("HighScoreLabel").GetComponent<UILabel>()).text = 
				"High Score: " + ScoreManager.GetHighScore().ToString();
				
			// Disable Top HUD/etc to clean up screen
			// TODO ?
				
		}
		else
		{
			startTime -= Time.deltaTime;
			currentTime = string.Format("{0:0}", startTime);
		}
	}

	public void OnContinueButtonClick()
	{
		// Quit clicked
		showWinScreen = false;
		Application.LoadLevel("main_menu");
	}
	
	public void OnQuitButtonClick()
	{
		// Quit clicked
		showWinScreen = false;
		Application.LoadLevel("main_menu");
	}
	
	public void StartLevel01()
	{
		currentLevel = 2;
		Application.LoadLevel(currentLevel);
	}

	public void CompleteLevel()
	{
		showWinScreen = true;
	}
	
	public void OnSettingsButtonClick()
	{
		Application.LoadLevel("settings_menu");
	}
	
	void LoadNextLevel()
	{
		currentLevel = 0;
		Application.LoadLevel(currentLevel);
	}
	
	void SaveGame()
	{
		
	}
	
	IEnumerator SpawnSheep()
	{
		bool spawning = true;
		
		while (spawning)
		{
			GameObject barn = GameObject.Find("Barn");
			//barn.
			// Wait for that amount of time
			yield return new WaitForSeconds(30f);
		}
	}
}
