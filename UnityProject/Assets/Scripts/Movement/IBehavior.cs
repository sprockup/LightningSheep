using UnityEngine;
using System.Collections;

interface IBehavior {

	// Use this to time when to get the next position
	//IEnumerator PickNewPosition();

	// Use this to get the location of the next position to move too
	Vector3 GetNextPosition();
	
}
