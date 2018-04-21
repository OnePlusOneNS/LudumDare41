using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {
	
	[SerializeField]
	private SeedSpotManager _seedSpotManager;
	private NavMeshAgent agent;

	private void Start() 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(_seedSpotManager.GetClosestActiveSeedSpot(transform.position));
	}
}
