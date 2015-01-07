using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin skin;

	// Use this for GUI elements
	/*void OnGUI()
	{
		// Apply skin
		GUI.skin = skin;

		// Dynamic width & height based on screen size
		float boxHeight, boxWidth;
		boxWidth = Screen.width;
		boxHeight = Screen.height * .4f;
		// Create rect on top of menu
		Rect winScreenRect = new Rect(10, 
		                              10, boxWidth, boxHeight);
		GUI.Label(winScreenRect, "Lightening Sheep!");
		
		if (GUI.Button(new Rect(10,150,300,80), "Play"))
		{
			// Play Clicked
			Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(10,240,300,80), "Quit"))
		{
			// Quit Clicked
			Application.Quit();
		}
	}*/
	
	void OnStartButtonClick()
	{
		// Play Clicked
		Application.LoadLevel("level_selector");
	}
	
	void OnQuitButtonClick()
	{
		// Quit Clicked
		Application.Quit();
	}
}
