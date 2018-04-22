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
	private float _attackDelay = 4f;
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
	}

	private void FixedUpdate() 
	{
		_agent.SetDestination(_seedSpotManager.GetClosestActiveSeedSpot(transform.position));
	}

	public int GetAttackDamage() 
	{
		return _attackDamage;
	}

	private void Update() 
	{
		if(_health <= 0) 
		{
			
			Destroy(this.gameObject);
		}
	}

	private void UpdateDestination(Vector3 newTargetPosition) 
	{
		_agent.SetDestination(newTargetPosition);
	}

	private void OnTriggerEnter(Collider c) 
	{
		if(c.tag == "Player") 
		{

		} 
		if(c.GetComponentInParent<PickUpItem>() != null) 
		{
			if(c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Weapon || c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Tool) 
			{
				Debug.Log("Weapon Hit");
				DecreaseHealth(c.GetComponentInParent<PickUpItem>().GetDamageAmount());
			}  
		}
		if(c.GetComponent<SeedSpot>() != null) 
		{
			StartCoroutine(EnemyAttackRoutine(c.GetComponent<SeedSpot>()));
		}	
	}

	private void DecreaseHealth(short hitGotten) 
	{
		_health -= hitGotten;
	}

	private IEnumerator EnemyAttackRoutine(SeedSpot g) 
	{
		while(g.gameObject.GetComponentInChildren<Plant>() != null) 
		{
			yield return new WaitForSeconds(_attackDelay);
			_animator.SetTrigger("attack");
			if(g.gameObject.GetComponentInChildren<Plant>() != null) 
			{
				g.gameObject.GetComponentInChildren<Plant>().DecreaseHealth(_attackDamage);
				
			} else 
			{
				yield return null;
			}
			_animator.SetTrigger("idle");
			if(!(g.gameObject.GetComponentInChildren<Plant>() != null)) 
			{
				yield return null;
			}
		}
	}
}
