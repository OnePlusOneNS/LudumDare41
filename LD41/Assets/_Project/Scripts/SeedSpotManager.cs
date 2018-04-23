using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpotManager : MonoBehaviour {
	
	[SerializeField]
	private GameObject _waterStream;
	[SerializeField]
	private List<GameObject> _activeSeedSpots = new List<GameObject>();

	[SerializeField]
	private List<GameObject> _nonActiveSeedSpots = new List<GameObject>();

	public void AddSeedSpotsToActiveSeedSpotsList(SeedSpot activatedSeedSpot) 
	{
		if(!(_activeSeedSpots.Contains(activatedSeedSpot.gameObject))) 
		{
			_activeSeedSpots.Add(activatedSeedSpot.gameObject);
			activatedSeedSpot.GetComponentInChildren<ParticleSystem>().Stop();
			_nonActiveSeedSpots.Remove(activatedSeedSpot.gameObject);
		}
	}

	public void CheckIfDone() 
	{
		int counter = 0;
		if(_nonActiveSeedSpots.Count <= 0) 
		{
			for(int i = 0; i<_activeSeedSpots.Count; i++) 
			{
				if(_activeSeedSpots[i].GetComponentInChildren<Plant>().GetCurrentStage() == 4) 
				{
					counter++;
				} else 
				{
					return;
				}
			}
			if(counter == _activeSeedSpots.Count) 
			{
				Time.timeScale = 0;
			}
		}
	}

	public int GetActiveSeedSpotsCount() 
	{
		return _activeSeedSpots.Count;
	}

	public void WaterPlants() 
	{
		_waterStream.SetActive(true);
	}
	public void RemoveSeedSpotsFromActiveSeedSpotList(SeedSpot deactivatedSeedSpot) 
	{
		if(!(_nonActiveSeedSpots.Contains(deactivatedSeedSpot.gameObject))) 
		{
			deactivatedSeedSpot.SetSpotFree();
			_nonActiveSeedSpots.Add(deactivatedSeedSpot.gameObject);
			deactivatedSeedSpot.GetComponentInChildren<ParticleSystem>().Play();
			_activeSeedSpots.Remove(deactivatedSeedSpot.gameObject);
		}
	}

	public void StopWater() 
	{
		_waterStream.SetActive(false);
	}

	public void PlantsGrow() 
	{
		for(int i = 0; i<_activeSeedSpots.Count; i++) 
		{
			_activeSeedSpots[i].GetComponentInChildren<Plant>().Grow();
		}
	}

	public Vector3 GetClosestActiveSeedSpot(Vector3 agentPosition) 
	{
		Vector3 destinationSeedSpot = new Vector3();
		float minDistance = float.MaxValue;
		for(int i = 0; i<=_activeSeedSpots.Count-1;i++) 
		{
			float tempDistance = Vector3.Distance(_activeSeedSpots[i].transform.position, agentPosition);
			if(tempDistance < minDistance) 
			{
				minDistance = tempDistance;
				destinationSeedSpot = _activeSeedSpots[i].transform.position;
			}
		}
		return destinationSeedSpot;
	}

	public bool SeedSpotsNotEmpty() 
	{
		if(_activeSeedSpots.Count != 0) 
		{
			return true;
		} else 
		{
			return false;
		}
	}
}
