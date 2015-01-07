using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	public float speed = 0.75F;
	
	void Update() {
/*
#if UNITY_EDITOR	
		if (ChargeManager.GetState() == ChargeManager.ChargeState.NotReady && Input.GetMouseButton(0) && Input. == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
		}
#endif
*/
		if (ChargeManager.GetState() == ChargeManager.ChargeState.NotReady && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			transform.Translate(-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);
			transform.position = new Vector3(transform.position.x, 40f, transform.position.z);
		}
		
		//Cast a ray downward, return distance        
		/*RaycastHit hit;
		Debug.DrawRay(transform.position, -Vector3.up);
		if(Physics.Raycast(transform.position,Vector3.down,out hit))
		{
			Debug.Log("Distance to ground is " + hit.distance);
		}*/
	}
}
