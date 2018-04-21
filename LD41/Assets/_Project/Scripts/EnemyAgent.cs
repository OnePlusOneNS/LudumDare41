using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {
	
	[SerializeField]
	private float _stoppingDistance = 2f;
	[SerializeField]
	private SeedSpotManager _seedSpotManager;
	[SerializeField]
	private float _attackDelay;
	private Animator _animator;
	private NavMeshAgent agent;

	private void Start() 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(_seedSpotManager.GetClosestActiveSeedSpot(transform.position));
		agent.stoppingDistance = 2f;
		StartCoroutine(TargetReachedRoutine());
	}

	private IEnumerator TargetReachedRoutine() 
	{
		while(agent.remainingDistance > 2) 
		{
			yield return null;
		}
		_animator.SetTrigger("idle");
		DamageTarget();
	}

	private void DamageTarget() 
	{
		_animator.SetTrigger("attack");
	}
}
