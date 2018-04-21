using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	[SerializeField]
	private GameObject _handSpot;
	[SerializeField]
	private GameObject _interactableObjects;
	[SerializeField]
	private SeedSpotManager _seedSpotManager;
	private bool _handSpotFree = true;
	private GameObject _pickedItem;

	private List<GameObject> _collidedGameobjectsList = new List<GameObject>();
	private GameObject _collidedGameobjectEnter;
	private GameObject _collidedGameobjectExit;
	private void OnTriggerEnter(Collider c) 
	{
		_collidedGameobjectEnter = c.gameObject;
		if(_collidedGameobjectEnter.GetComponent<PickUpItem>() != null) 
		{
			Debug.Log("Gameobject -> " + _collidedGameobjectEnter);
			//c.gameObject.GetComponent<PickUpItem>().GetInfo();
			AddToColliderList(_collidedGameobjectEnter);
		} else 
		{
		}
	}

	private void OnTriggerStay(Collider c) 
	{
		if(_collidedGameobjectEnter.GetComponent<PickUpItem>() != null) 
		{
			GameObject lastGameObjectInList = _collidedGameobjectsList[_collidedGameobjectsList.Count-1];
			if(Input.GetButton("Pickup") && !lastGameObjectInList.GetComponent<PickUpItem>().GetPickedUp())
			{
				lastGameObjectInList.GetComponent<Collider>().enabled = false;
				PickUpItem(lastGameObjectInList);

			}
		} else if (_collidedGameobjectEnter.GetComponent<SeedSpot>() != null) 
		{
			SeedSpot seedSpot = c.gameObject.GetComponent<SeedSpot>();
			if(Input.GetButton("Use") && seedSpot.GetSpotFree())
			{
				_handSpotFree = true;
				if(_pickedItem.GetComponent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Seed);
				seedSpot.PlantPlantSeed(_pickedItem);
				_seedSpotManager.AddSeedSpotsToActiveSeedSpotsList(seedSpot);
			}
		}
	}

	private void OnTriggerExit(Collider c) 
	{
		_collidedGameobjectExit = c.gameObject;
		if(_collidedGameobjectExit.GetComponent<PickUpItem>() != null)
			Debug.Log("Gameobject -> " + _collidedGameobjectExit); 
			if(CheckItemInColliderList(_collidedGameobjectExit)) 
			{
				RemoveFromColliderList(_collidedGameobjectExit);
			}
	}

	private void PickUpItem(GameObject g) 
	{
		if(_handSpotFree) 
		{
			_handSpotFree = false;
			g.transform.parent = _handSpot.transform;
			g.transform.position = _handSpot.transform.position;
			_pickedItem = g;
		} else 
		{
			DropCurrentItem(_pickedItem);
			g.transform.parent = _handSpot.transform;
			g.transform.position = _handSpot.transform.position;
			_pickedItem = g;
		}
	}

	private void DropCurrentItem(GameObject g)
	{
		g.transform.parent = _interactableObjects.transform;

	}

	private void AddToColliderList(GameObject g) 
	{
		_collidedGameobjectsList.Add(g);
		Debug.Log("Added " + g + "to the List");
		Debug.Log("List now " + _collidedGameobjectsList.Count + "groß");
	}

	private void RemoveFromColliderList(GameObject g) 
	{
		_collidedGameobjectsList.Remove(g);
		Debug.Log("Removed  " + g + "from the List");
	}

	private bool CheckItemInColliderList(GameObject g) 
	{
		if(_collidedGameobjectsList.Contains(g)) 
		{
			return true;
		} else {
			return false;
		}
	}
}