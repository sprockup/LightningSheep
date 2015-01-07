using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour {

	public Color defaultColor;
	public Color selectedColor;
	private Material material;

	private const float MAX_MOVE_SPEED = 20f;
	public float moveSpeed = 20f;
	
	private float height = 5;
	//private float size = 45.0f;
	private Vector3 currentPosition;
	private Vector3 nextPosition;

	public GameObject deathParticles;
	public GameObject lighteningBolt;
	public GameObject fadingMessagePreFab;
	
	private IBehavior behavior;
	
	// Health related variables
	private bool isAlive = true;
	private int healthPoints = 100;
	
	public GameObject HUDTextPrefab;
	HUDText mText = null;
	private GameObject hudChild;
	
	// Use this for initialization
	void Start () 
	{
		behavior = new RandomBehavior();
	
		// Grab material for color changing
		material = renderer.material;

		// Start somewhere random
		// SelectNextPosition ();
		nextPosition = behavior.GetNextPosition();
		currentPosition = nextPosition;
		transform.position = currentPosition;

		// Pick random speed based on initial moveSpeed
		moveSpeed = Random.Range((MAX_MOVE_SPEED - 8), MAX_MOVE_SPEED);

		// Now select the first place to move to
		// Before IF 
		// 	SelectNextPosition ();
		nextPosition = behavior.GetNextPosition();

		// Start coroutine to time out the next position to move towards		
		StartCoroutine(PickNewPosition());
		
		// We need the HUD object to know where in the hierarchy to put the element
		if (HUDRoot.go == null)
		{
			GameObject.Destroy(this);
			return;
		}
		
		GameObject hudChild = NGUITools.AddChild(HUDRoot.go, HUDTextPrefab);
		mText = hudChild.GetComponentInChildren<HUDText>();
		
		// Make the UI follow the target
		hudChild.AddComponent<UIFollowTarget>().target = transform;
	}
	
	IEnumerator PickNewPosition()
	{
		// Select random time
		float time = Random.Range(0.5f, 5f);
		
		// Wait for that amount of time
		yield return new WaitForSeconds(time);
		
		// Get a new position to move to
		nextPosition = behavior.GetNextPosition();

		transform.LookAt(nextPosition);
		
		// Move if not dead
		if (isAlive)
			StartCoroutine(PickNewPosition());
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Update our position
		if (isAlive)
		{
			transform.position = 
				Vector3.MoveTowards(transform.position, 
					nextPosition, 
					moveSpeed * Time.deltaTime);
			// This fix keeps the sheep on the ground for now but will not work with any
			// elevation to their containing field
			transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
			
			//Debug.Log("Update Pos: " + transform.position);
		}
	}

	void LateUpdate()
	{
		if (!isAlive)
		{
			// Remove sheep
			//Destroy(HUDTextPrefab, 0f);
			//hudChild.
			//NGUITools.Destroy(hudChild);
			Destroy(gameObject, 0f);
		}
	}

	void OnCollisionEnter(Collision other)
	{

	}

	void OnTouchDown() 
	{
		material.color = selectedColor;

		// Clear all charge if a sheep is touched first
		ChargeManager.SetCharge(0f);
		ChargeManager.SetState(ChargeManager.ChargeState.NotReady);
	}
	
	void OnTouchUp() 
	{
		//Debug.Log ("Sheep OnTouchUp");
		material.color = defaultColor;
		if (ChargeManager.GetState() == ChargeManager.ChargeState.ReadyToDischarge)
		{
			DirectStrike();

			UpdateHealthPoints();
			
			// There is still enough charge left to hop to neighboring sheep
			//else
			{
				GameObject[] sheep = GameObject.FindGameObjectsWithTag("Sheep");
				Sheep closestSheep = null;
				float closestSheepDist = 100000f;
				bool found = false;
				foreach (GameObject s in sheep)
				{
					// Compute distance from this sheep to all others
					float dist = Vector3.Distance(transform.position, s.transform.position);
					//Debug.Log("Dist = " + dist + ", ClosestSheepDist = " + closestSheepDist 
					//	+ ", Target = " + 0.75f * ChargeManager.GetChargeLevel());
					
					// Find the closest sheep for this hop
					if (this.gameObject != s && 
						dist <= 0.3f * ChargeManager.GetChargeLevel() &&
						dist < closestSheepDist)
					{
						closestSheep = s.GetComponent<Sheep>();
						closestSheepDist = dist;
						found = true;
					}
				}
				
				if (found) 
				{
					Debug.Log("Closest sheep found at " + closestSheepDist + ". Hopping to that sheep");
					closestSheep.SecondaryStrike(this, closestSheepDist);
				}
			}

			// Update ChargeManager for the next zap
			ChargeManager.SetCharge(0f);
			ChargeManager.SetState(ChargeManager.ChargeState.NotReady);
		}
	}
	
	/*
	*	cloud - a strike from the lightening bolt  
	*		with full power to this sheep
	*/
	private void DirectStrike()
	{
		// Get Cloud cylinder
		GameObject[] cloud = GameObject.FindGameObjectsWithTag("CloudBase");
		
		CreateBoltBetweenObjects(cloud[0], this.gameObject);
		
		string msg = "Zapped with " + ChargeManager.GetChargeLevel() + "!";
		ScoreManager.AddToScore(ChargeManager.GetChargeLevel());
		Debug.Log(msg);
		
		// Create the fading message
		GameObject fadingMsg = (GameObject)Instantiate(fadingMessagePreFab);
		// Set the message's text
		fadingMsg.guiText.text = msg;
	}
	
	/*
	* SecondaryStrike - a lightening bolt hop from one sheep (fromSheep) 
	* 		to this sheep
	*/
	void SecondaryStrike(Sheep fromSheep, float distance)
	{
		Debug.Log("SecondaryStrike");
		CreateBoltBetweenObjects(fromSheep.gameObject, this.gameObject);
		
		// Reduce the power of the charge for the strike/damage
		float chargeModifier = -0.5f;
		ChargeManager.AddToCharge(ChargeManager.GetChargeLevel() * chargeModifier);
		
		// Increase the charge for the score though
		ScoreManager.AddToScore(ChargeManager.GetChargeLevel() * 4.0f);
		
		UpdateHealthPoints();
	}

	/*
	* CreateBoltBetweenObjects - creates a lightening bolt prefab between 
	* 		the two objects, scaled and aligned
	*/
	private void CreateBoltBetweenObjects(GameObject a, GameObject b)
	{
		// Create new lightenbolt cylinder from PreFab
		GameObject bolt = (GameObject)Instantiate(lighteningBolt, b.transform.position, Quaternion.identity);
		// Face the lightening bolt from sheep to cloud
		bolt.transform.LookAt(a.transform);
		// Position the bolt's center point half way between the cloud and sheep
		bolt.transform.position = Vector3.Lerp(a.transform.position, b.transform.position, 0.5f);
		// Scale the bolt from sheep to cloud
		Vector3 newScale = bolt.transform.localScale;
		newScale.z = Vector3.Distance(b.transform.position, a.transform.position);
		bolt.transform.localScale = newScale;
	}
	
	private void KillSheep()
	{
		// Sheep died :(
		isAlive = false;
		
		// Create the death particles animation at the sheeps last position
		Instantiate(deathParticles, transform.position, Quaternion.identity);
	}
	
	private void UpdateHealthPoints()
	{
		// Update health points based on chargelevel
		healthPoints -= (int)ChargeManager.GetChargeLevel();
		if (mText)
			mText.Add(-1f * ChargeManager.GetChargeLevel(), Color.red, 0f);
			
		// If there is no more health left then kill the sheep
		// and create the death particles
		if (healthPoints <= 0)
		{
			KillSheep();
		}
	}
	
	void OnTouchStay() 
	{
		material.color = selectedColor;
	}
	
	void OnTouchExit() 
	{
		material.color = defaultColor;
	}
}
