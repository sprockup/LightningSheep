using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
	
	
	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (camera.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				// Make sure the orthographic size never drops below 10.
				camera.orthographicSize = Mathf.Max(camera.orthographicSize, 10.0f);

				Vector3 touchToWorldA = camera.ScreenToWorldPoint (new Vector3 ((touchZero.position.x), (touchZero.position.y), camera.nearClipPlane));
				Vector3 touchToWorldB = camera.ScreenToWorldPoint (new Vector3 ((touchOne.position.x), (touchOne.position.y), camera.nearClipPlane));
				
				Vector3 center = (touchToWorldA + touchToWorldA) / 2.0f; 
				Vector3 position = new Vector3(center.x, center.y, 0);
				// Not yet ready for prime time...
				// transform.position = position;

			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				//camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
				camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 50f, 90f);
			}
		}
	}
}
