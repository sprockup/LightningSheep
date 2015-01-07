using UnityEngine;
using System.Collections;

public class Clould : MonoBehaviour {

	//private Material material;
	private Vector3 lastHit;
	private enum TouchState {WAITING, ACCUMULATING, COMPLETED};
	private TouchState touchState;

	// Use this for initialization
	void Start () {
		//material = renderer.material;
		ChargeManager.SetCharge(0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ChargeManager.GetState() == ChargeManager.ChargeState.NotReady)
		{
			UpdateCloudColor();
		}
	}

	void OnTouchDown() 
	{
		// Clear the lastHit variable when we start
		touchState = TouchState.WAITING;
		UpdateCloudColor();
	}
	
	void OnTouchUp() 
	{
		//Debug.Log("Cloud::OnTouchUp");
		touchState = TouchState.COMPLETED;
		UpdateCloudColor();
	}
	
	void OnTouchStay(Vector3 currentHit) 
	{
		//Debug.Log("Cloud::OnTouchStay");
		if (touchState == TouchState.WAITING)
		{
			// This is the first time through so capture the current point
			// as the last hit
			lastHit = currentHit;
			// Update the state to start accumulating charge
			touchState = TouchState.ACCUMULATING;
			ChargeManager.SetState(ChargeManager.ChargeState.Charging);
		}
		else if (touchState == TouchState.ACCUMULATING)
		{
			// The distance between the last hit and current hit will be
			// added to the cloud's charge during each update
			float currCharge = 
				Vector3.Distance(lastHit, currentHit) * ChargeManager.CHARGEMULTIPLIER;
			ChargeManager.AddToCharge(currCharge);
			ChargeManager.SetState(ChargeManager.ChargeState.Charging);
			// Capture the current point as the last for the next iteration
			lastHit = currentHit;
		}
		
		UpdateCloudColor();
	}
	
	void OnTouchExit() 
	{
		//Debug.Log("Cloud::OnTouchExit");
		touchState = TouchState.COMPLETED;
		ChargeManager.SetState(ChargeManager.ChargeState.ReadyToDischarge);
		UpdateCloudColor();
	}
	
	void UpdateCloudColor()
	{
		//spotLight.intensity = 
		//	Mathf.Lerp (8, 0, ChargeManager.GetChargeLevel() / ChargeManager.MAXCHARGE);
	}
}
