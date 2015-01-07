using UnityEngine;
using System.Collections;

public class LightningBolt {

	// Base of the cloud used to start the lightning bolt from to the first sheep
	GameObject cloud;

	// Collection of sheep found during the first strike
	GameObject[] sheep;

	// Use this for initialization
	LightningBolt () {
		// Get Cloud empty
		GameObject[] clouds = GameObject.FindGameObjectsWithTag("CloudBase");
		cloud = clouds [0];

	}
	
	public void DoFirstStrike(GameObject sheep0)
	{
		CreateBoltBetweenObjects(cloud, sheep0);
		
		string msg = "Zapped with " + ChargeManager.GetChargeLevel() + "!";
		ScoreManager.AddToScore(ChargeManager.GetChargeLevel());
		Debug.Log(msg);
		
		// Create the fading message
		GameObject fadingMsg = (GameObject)Instantiate(fadingMessagePreFab);
		// Set the message's text
		fadingMsg.guiText.text = msg;



	}

	private int UpdateSheepArray()
	{
		// Collect all sheep objects into the array for processing
		GameObject[] sheep = GameObject.FindGameObjectsWithTag("Sheep");

		return sheep.Length ();
	}

	/*
	* CreateBoltBetweenObjects - creates a lightening bolt prefab between 
	* 		the two objects, scaled and aligned
	*/
	private void CreateBoltBetweenObjects(GameObject a, GameObject b)
	{
		// Create new lightenbolt cylinder from PreFab
		GameObject bolt = (GameObject)Instantiate(lighteningBolt, b.transform.position, Quaternion.identity);
		// Face the lightening bolt from sheep to cloud
		bolt.transform.LookAt(a.transform);
		// Position the bolt's center point half way between the cloud and sheep
		bolt.transform.position = Vector3.Lerp(a.transform.position, b.transform.position, 0.5f);
		// Scale the bolt from sheep to cloud
		Vector3 newScale = bolt.transform.localScale;
		newScale.z = Vector3.Distance(b.transform.position, a.transform.position);
		bolt.transform.localScale = newScale;
	}

}
