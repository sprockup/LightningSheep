using UnityEngine;
using System.Collections;

public class FadingMessage : MonoBehaviour {

	const float DURATION = 1.25f;

	float startTime;

	void Start() 
	{
		Color newColor = guiText.material.color;
		newColor.a = 1;
		guiText.material.color = newColor;

		startTime = Time.time;

		//Debug.Log("FadingMessage::Start() @" + startTime);
	}

	// Update is called once per frame
	void Update () 
	{
		if( (Time.time - startTime) > DURATION){
			Destroy(gameObject);
		}

		Color newColor = guiText.material.color;
		float proportion = ((Time.time - startTime) / DURATION);
		newColor.a = Mathf.Lerp (1, 0, proportion);
		guiText.material.color = newColor;
	}
}
