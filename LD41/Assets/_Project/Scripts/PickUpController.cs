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
	[SerializeField]
	private WaveManager _waveManager;
	[SerializeField]
	private PlayerFightController _playerFightController;
	private bool _handSpotFree = true;
	private bool _pickUpLock = false, _useLock = false;
	private GameObject _pickedItem;

	private int _pickUpLayer, _seedSpotLayer, _interactableButtonLayer;

	private List<GameObject> _collidedGameobjectsList = new List<GameObject>();
	private GameObject _collidedGameobjectEnter;
	private GameObject _collidedGameobjectExit;

	private void Start() 
	{
		_pickUpLayer = LayerMask.GetMask("Pickup");
		_seedSpotLayer = LayerMask.GetMask("SeedSpot");
		_interactableButtonLayer = LayerMask.GetMask("InteractableButtonLayer");
	}

	private void Update() 
	{
		if(Input.GetButton("Pickup") && !_pickUpLock) 
		{
			_pickUpLock = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 2, _pickUpLayer)) 
			{
				GameObject hitGameObject = hit.collider.gameObject;
				hitGameObject.GetComponent<Collider>().enabled = false;
				PickUpItem(hitGameObject);
			}
		_pickUpLock = false;
		}
		if(Input.GetButton("Use") && !_useLock)
		{
			_useLock = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 3, _seedSpotLayer)) 
			{
				SeedSpot seedSpot = hit.collider.gameObject.GetComponent<SeedSpot>();
				if(_pickedItem.GetComponent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Seed && seedSpot.GetSpotFree())
				{
					seedSpot.PlantPlantSeed(_pickedItem);	
					_seedSpotManager.AddSeedSpotsToActiveSeedSpotsList(seedSpot);
					_handSpotFree = true;
				}
			} else if (Physics.Raycast(ray, out hit, 3, _interactableButtonLayer)) 
			{
				if(_seedSpotManager.SeedSpotsNotEmpty()) 
				{
					_seedSpotManager.WaterPlants();
					_waveManager.LaunchWave();
				}
			}
			_useLock = false;
		}		
	}

	private void PickUpItem(GameObject g) 
	{
		if(_handSpotFree) 
		{
			_handSpotFree = false;
			g.transform.parent = _handSpot.transform;
			g.transform.position = _handSpot.transform.position;
			g.transform.rotation = _handSpot.transform.rotation;
			_pickedItem = g;
			if(_pickedItem.GetComponent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Weapon) 
			{
				_playerFightController.RecievedWeapon(_pickedItem);
			}
		} else 
		{
			DropCurrentItem(_pickedItem);
			g.transform.parent = _handSpot.transform;
			g.transform.position = _handSpot.transform.position;
			g.transform.rotation = _handSpot.transform.rotation	;
			_pickedItem = g;
		}
	}

	private void DropCurrentItem(GameObject g)
	{
		g.transform.parent = _interactableObjects.transform;
		g.GetComponent<Collider>().enabled = true;
	}
}