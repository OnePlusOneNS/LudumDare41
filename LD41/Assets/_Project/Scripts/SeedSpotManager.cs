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
		_activeSeedSpots.Add(activatedSeedSpot.gameObject);
		activatedSeedSpot.GetComponentInChildren<ParticleSystem>().Stop();
		_nonActiveSeedSpots.Remove(activatedSeedSpot.gameObject);
	}

	public void WaterPlants() 
	{
		StartCoroutine(WaterPlantsRoutine());
	}

	private IEnumerator WaterPlantsRoutine() 
	{
		_waterStream.SetActive(true);
		yield return new WaitForSeconds(8f);
		_waterStream.SetActive(false);
	}

	public void RemoveSeedSpotsFromActiveSeedSpotList(SeedSpot deactivatedSeedSpot) 
	{
		_nonActiveSeedSpots.Add(deactivatedSeedSpot.gameObject);
		deactivatedSeedSpot.GetComponentInChildren<ParticleSystem>().Play();
		_activeSeedSpots.Remove(deactivatedSeedSpot.gameObject);
	}

	public void PlantsGrow() 
	{
		for(int i = 0; i<_activeSeedSpots.Count-1; i++) 
		{
			_activeSeedSpots[i].GetComponent<Plant>().Grow();
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
