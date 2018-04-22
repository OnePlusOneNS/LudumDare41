using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpot : MonoBehaviour {

	private GameObject _plantedSeed;
	private bool _spotFree = true;

	private int _plantStage;

	public bool GetSpotFree() 
	{
		return _spotFree;
	}

	public void PlantPlantSeed(GameObject plantSeed) {
		_spotFree = false;
		_plantedSeed = plantSeed;
		_plantedSeed.transform.parent = this.transform;
		StartCoroutine(SeedToSeedSpotRoutine());
	}

	private IEnumerator SeedToSeedSpotRoutine() 
	{
		float steps = 0f;
		while(_plantedSeed.transform.position != transform.position) {
			_plantedSeed.transform.position = Vector3.Slerp(_plantedSeed.transform.position, transform.position, steps+=0.01f);
			_plantedSeed.transform.rotation = Quaternion.Slerp(_plantedSeed.transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y + Random.Range(0,360), transform.rotation.z, transform.rotation.w), steps+=0.01f);
			yield return null;
		}		
	}
}
