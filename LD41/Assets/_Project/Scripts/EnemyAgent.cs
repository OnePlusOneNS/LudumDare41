using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {
	
	[SerializeField]
	private short _health = 7;
	[SerializeField]
	private SeedSpotManager _seedSpotManager;
	[SerializeField]
	private int _attackDelay = 4;
	[SerializeField]
	private int _attackDamage;
	private Animator _animator;
	private NavMeshAgent _agent;
	private UnityAction DeathAction;

	private void Start() 
	{
		_animator = GetComponent<Animator>();
		_seedSpotManager = GameObject.FindGameObjectWithTag("SeedSpotManager").GetComponent<SeedSpotManager>();
		_agent = GetComponent<NavMeshAgent>();
		_agent.SetDestination(_seedSpotManager.GetClosestActiveSeedSpot(transform.position));
		_animator.SetTrigger("walk");
		StartCoroutine(TargetReachedRoutine());
	}

	public int GetAttackDamage() 
	{
		return _attackDamage;
	}

	public int GetAttackDelay() 
	{
		return _attackDelay;
	}

	private void Update() 
	{
		if(_health <= 0) 
		{
			
			Destroy(this.gameObject);
		}
	}

	private IEnumerator TargetReachedRoutine() 
	{
		while(_agent.remainingDistance > 2) 
		{
			yield return null;
		}
		_animator.SetTrigger("idle");
		StartCoroutine(EnemyAttackRoutine());
	}

	private void UpdateDestination(Vector3 newTargetPosition) 
	{
		_agent.SetDestination(newTargetPosition);
	}

	private void OnTriggerEnter(Collider c) 
	{
		if(c.tag == "Player") 
		{

		} else 
		{
			if(c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Weapon || c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Tool) 
			{
				Debug.Log("Weapon Hit");
				DecreaseHealth(c.GetComponentInParent<PickUpItem>().GetDamageAmount());
			}
		}	
	}

	private void DecreaseHealth(short hitGotten) 
	{
		_health -= hitGotten;
	}

	private IEnumerator EnemyAttackRoutine() 
	{
		//_currentWeapon.GetComponentInChildren<Collider>().enabled = true;
		_animator.SetTrigger("attack");
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
		//_currentWeapon.GetComponentInChildren<Collider>().enabled = false;
		yield return new WaitForSeconds(_attackDelay);
	}
}
