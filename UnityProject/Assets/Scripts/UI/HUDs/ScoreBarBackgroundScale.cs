using UnityEngine;
using System.Collections;

public class ScoreBarBackgroundScale : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3(Screen.width, this.transform.localScale.y);
	}
}
