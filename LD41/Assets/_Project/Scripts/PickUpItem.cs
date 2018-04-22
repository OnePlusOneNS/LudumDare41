using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUpItem : MonoBehaviour {
	
	[SerializeField]
	private string _name;
	[SerializeField]
	private short _damageAmount, _attackDelay;
	[SerializeField]
	private PickUpItemType _type;
	private bool _pickedUp = false;

	public short GetDamageAmount() 
	{
		return _damageAmount;
	}

	public short GetAttackDelay() 
	{	
		return _attackDelay;
	}

	public bool GetPickedUp() 
	{
		return _pickedUp;
	}

	public void SetPickedUp(bool b) 
	{
		_pickedUp = b;
	}

	public PickUpItemType GetPickUpItemType() 
	{
		return _type;
	}
}
