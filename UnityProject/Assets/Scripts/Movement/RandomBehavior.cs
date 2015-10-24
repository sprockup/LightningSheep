using UnityEngine;
using System.Collections;

public class RandomBehavior : IBehavior {

	/*IEnumerator PickNewPosition()
	{
		// Select random time
		float time = Random.Range(0.5f, 5f);
		// Wait for that amount of time
		yield return new WaitForSeconds(time);
		// Get a new position to move to
		SelectNextPosition();
		
		// Always move sheep
		StartCoroutine(PickNewPosition());
	}*/

	public Vector3 GetNextPosition()
	{
		return new Vector3 (Random.Range (-7, -200), 0.62f, Random.Range (-170, 9));
	}
}
