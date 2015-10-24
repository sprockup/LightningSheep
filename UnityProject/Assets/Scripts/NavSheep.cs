using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class NavSheep : MonoBehaviour {

	NavMeshAgent navMesh;
	IBehavior behavior;

	void Start () {
		navMesh = GetComponent<NavMeshAgent>();
		behavior = new RandomBehavior ();
		StartCoroutine(UpdatePath());
	}

	void Update () {

	}

	IEnumerator UpdatePath() {
		float UpdateRate = 2;

		while (true) {
			navMesh.SetDestination(behavior.GetNextPosition());
			yield return new WaitForSeconds(UpdateRate);
		}
	}
}

