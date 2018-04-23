using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightController : MonoBehaviour {

	[SerializeField]
	private short _health = 15;

	private bool _hasWeapon;
	private bool _attackButtonLock = false;
	private GameObject _currentWeapon;
	private Collider _weaponCollider;
	private Animator _animator;
	private int _plantLayer;

	private void Start () {
		_animator = GetComponentInChildren<Animator>();
		_plantLayer = LayerMask.GetMask("PlantLayer");
	}

	public void RecievedWeapon(GameObject g) 
	{
		_hasWeapon = true;
		_currentWeapon = g;
		_weaponCollider = g.GetComponentInChildren<Collider>();
	}
	
	private void Update() 
	{
		if(Input.GetButton("Attack") && _hasWeapon && !_attackButtonLock) 
		{
			_attackButtonLock = true;
			_animator.SetTrigger("attack");
			StartCoroutine(PlayerAttackRoutine());
		} else if(Input.GetButton("Use") && _hasWeapon && !_attackButtonLock && _currentWeapon.GetComponent<PickUpItem>().GetPickUpItemType().Equals(PickUpItemType.Tool)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 2, _plantLayer)) 
			{
				_animator.SetTrigger("attack");
				Plant plant = hit.collider.gameObject.GetComponent<Plant>();
				plant.Harvest();
			}
		}
	}

	private IEnumerator PlayerAttackRoutine() 
	{
		_weaponCollider.enabled = true;
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
		_weaponCollider.enabled = false;
		yield return new WaitForSeconds(_currentWeapon.GetComponent<PickUpItem>().GetAttackDelay());
		_attackButtonLock = false;
	}
}
