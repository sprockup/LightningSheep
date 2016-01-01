using UnityEngine;
using System.Collections;

public class FadingMessage : MonoBehaviour {

	const float DURATION = 1.25f;

	float startTime;

	void Start() 
	{
		Color newColor = GetComponent<GUIText>().material.color;
		newColor.a = 1;
		GetComponent<GUIText>().material.color = newColor;

		startTime = Time.time;

		//Debug.Log("FadingMessage::Start() @" + startTime);
	}

	// Update is called once per frame
	void Update () 
	{
		if( (Time.time - startTime) > DURATION){
			Destroy(gameObject);
		}

		Color newColor = GetComponent<GUIText>().material.color;
		float proportion = ((Time.time - startTime) / DURATION);
		newColor.a = Mathf.Lerp (1, 0, proportion);
		GetComponent<GUIText>().material.color = newColor;
	}
}
