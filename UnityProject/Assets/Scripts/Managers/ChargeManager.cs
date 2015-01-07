using UnityEngine;
using System.Collections;

public class ChargeManager : MonoBehaviour {

	private static float currentCharge;

	//private static float staticDischargeRate = 0.5f;
	private static float chargeKillLevel = 20.0f;
	
	public enum ChargeState {NotReady, Charging, ReadyToDischarge};
	private static ChargeState currentState;

	// Charge characteristics
	public const float CHARGEMULTIPLIER = 25.0f;
	public const float MAXCHARGE = 500f;
	
	// Charge bar
	private static UISlider slider;
	private static float maxWidth;
	
	void Awake()
	{
		slider = (UISlider)GameObject.Find("Charge Bar").GetComponent<UISlider>();
	
		if (slider == null)
		{
			Debug.LogError("No slider found in ChargeManger");
			return;
		}
		
		// Commented out after making bar scale w/GUI
		maxWidth = slider.foreground.localScale.x;
	}
	
	private static void UpdateChargeBar(float x)
	{
		//maxWidth = slider.background.localScale.x;
	
		slider.foreground.localScale = 
			new Vector3(maxWidth * x, 
				slider.foreground.localScale.y,
				slider.foreground.localScale.z);
	}
	
	public static void SetNewMaxWidth(float newMaxWidth)
	{
		maxWidth = newMaxWidth;
	}
	
	void OnGUI()
	{
		GUILayout.Label("Charge = " + currentCharge);
		GUILayout.Label("State  = " + currentState);
	}

	public static float AddToCharge(float amountToAdd)
	{
		// Add to the current charge and return the total
		currentCharge += amountToAdd;
		
		// Cap the charge at its maximum
		if (currentCharge > MAXCHARGE)
			currentCharge = MAXCHARGE;

		// Indicate we are ready for discharge
		//SetState (ChargeState.ReadyToDischarge);

		UpdateChargeBar(currentCharge / MAXCHARGE);

		//Debug.Log ("ChargeManager::AddToCharge(" + amountToAdd + "): " + currentCharge);
		return currentCharge;
	}

	public static void SetCharge(float newCharge)
	{
		if (newCharge <= MAXCHARGE)
		{
			currentCharge = newCharge;
			UpdateChargeBar(currentCharge / MAXCHARGE);
			
		}
		else
		{
			Debug.LogError("Attempted to set charge greater than MAXCHARGE");
			currentCharge = MAXCHARGE;
		}
	}

	public static float GetChargeLevel()
	{
		return currentCharge;
	}

	public static float GetChargeKillLevel()
	{
		return chargeKillLevel;
	}

	public static void SetState(ChargeState state)
	{
		// Update the current state from the passed value
		currentState = state;
		// If the current state is NotReady then reset the charge
		if (currentState == ChargeState.NotReady)
		{
			currentCharge = 0;
		}
		Debug.Log ("ChargeManager::SetState to " + state);
	}

	public static ChargeState GetState()
	{
		return currentState;
	}
}
