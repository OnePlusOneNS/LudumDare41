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

	private void Start () {
		_animator = GetComponentInChildren<Animator>();
	}

	public void RecievedWeapon(GameObject g) 
	{
		_hasWeapon = true;
		_currentWeapon = g;
		Debug.Log("Has weapon and set it in PlayFightcontroller");
	}
	
	private void Update() 
	{
		if(Input.GetButton("Attack") && _hasWeapon && !_attackButtonLock) 
		{
			_attackButtonLock = true;
			_animator.SetTrigger("attack");
			StartCoroutine(PlayerAttackRoutine());
		}
	}

	private IEnumerator PlayerAttackRoutine() 
	{
		_currentWeapon.GetComponentInChildren<Collider>().enabled = true;
		yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
		_currentWeapon.GetComponentInChildren<Collider>().enabled = false;
		yield return new WaitForSeconds(_currentWeapon.GetComponent<PickUpItem>().GetAttackDelay());
		_attackButtonLock = false;
	}
}
