using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class NavSheep : MonoBehaviour {

	NavMeshAgent navMesh;

	void Start () {
		navMesh = GetComponent<NavMeshAgent>();

		StartCoroutine(UpdatePath());
	}

	void Update () {

	}

	IEnumerator UpdatePath() {
		float UpdateRate = 2;

		while (true) {
			navMesh.SetDestination(new Vector3 (Random.Range (-7, -200), 0, Random.Range (-170, 9)));
			yield return new WaitForSeconds(UpdateRate);
		}
	}
}

