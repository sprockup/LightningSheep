using UnityEngine;
using System.Collections;

public class ChargeBarScale : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
		float newScale = (Screen.width - 166.0f);
	
		foreach( Transform child in transform)
		{
			if (child.name == "Background")
			{
				child.localScale = new Vector3(newScale, child.localScale.y);
				ChargeManager.SetNewMaxWidth(newScale);		
				break;	
			}
		}
	}
}
