using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUpItem : MonoBehaviour {
	
	[SerializeField]
	private string _name;
	[SerializeField]
	private float _damage, _attackDelay;
	[SerializeField]
	private PickUpItemType _type;
	private bool _PickedUp = false;

	public bool GetPickedUp() 
	{
		return _PickedUp;
	}

	public void SetPickedUp(bool b) 
	{
		_PickedUp = b;
	}

	public PickUpItemType GetPickUpItemType() 
	{
		return _type;
	}

	public void GetInfo()
	{
		Debug.Log("Name " + _name + "Damage " + _damage + "Type " + _type);
	}
}
