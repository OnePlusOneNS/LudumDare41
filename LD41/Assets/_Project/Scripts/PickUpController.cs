using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {
	private void OnTriggerEnter(Collider c) 
	{
		if(c.gameObject.GetComponent<PickUpItem>() != null) 
		{
			c.gameObject.GetComponent<PickUpItem>().GetInfo();
		} else {
			
		}
	}

	private void OnTriggerStay(Collider c) 
	{

	}
}
