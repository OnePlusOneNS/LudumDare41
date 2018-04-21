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

	public void GetInfo()
	{
		Debug.Log("Name " + _name + "Damage " + _damage + "Type " + _type);
	}
}
