﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	[SerializeField]
	private List<PlantStage> _plantStagesList = new List<PlantStage>();

	private SeedSpotManager _seedSpotManager;
	private int _health;
	private bool _beenPlaced;
	private bool _attackInProgress = false;
	private int _currentPlantStage;
	private bool _isAboutToDie = false;
	
	[SerializeField]
	private GameObject _plant;

	public int GetCurrentStage() 
	{
		return _currentPlantStage;
	}

	public bool GetBeenPlaced() 
	{
		return _beenPlaced;
	}

	public void SetBeenPlaced(bool b) 
	{
		_beenPlaced = b;
	}

	private void Start () 
	{
		_currentPlantStage = 0;
		_beenPlaced = false;
		_health = 15;
		_seedSpotManager = GameObject.FindGameObjectWithTag("SeedSpotManager").GetComponent<SeedSpotManager>();
	}
	[ContextMenu("Grow")]
	public void Grow() 
	{
		_plantStagesList[_currentPlantStage].gameObject.SetActive(false);
		_currentPlantStage++;
		if(_currentPlantStage >= _plantStagesList.Count) {
			//Harvest();
		} else 
		{
			_plantStagesList[_currentPlantStage].gameObject.SetActive(true);
		}	
	}

	private void Cease() 
	{
		_plantStagesList[_currentPlantStage].gameObject.SetActive(false);
		if(_currentPlantStage <= 0) {
			GetComponent<MeshRenderer>().enabled = false;
			_seedSpotManager.RemoveSeedSpotsFromActiveSeedSpotList(GetComponentInParent<SeedSpot>());
			StartCoroutine(DieRoutine());
		} else {
			_currentPlantStage--;
			_isAboutToDie = false;
			_health = 15;
			_plantStagesList[_currentPlantStage].gameObject.SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider c) 
	{
		if(c.tag == "Player") 
		{
		
		} else if(c.GetComponent<EnemyAgent>() != null) 
		{
			_attackInProgress = true;
			StartCoroutine(GetDamagedRoutine(c.GetComponent<EnemyAgent>()));
		} 
	}	

	public bool IsAboutToDie() 
	{
		return _isAboutToDie;
	}

	private IEnumerator DieRoutine() 
	{
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}

	public void DecreaseHealth(int damage) 
	{
		if(_health <= 0) 
		{
			_isAboutToDie = true;
			Cease();
		} else 
		{
			_health = _health - damage;
		}
	}

	private IEnumerator GetDamagedRoutine(EnemyAgent enemyAgentAttacker) 
	{
			
		yield return null;		
	}

	public void ResetStage() 
	{
	}

	public void Harvest()
	{
		for(int i = 0; i<Random.Range(2,4); i++) 
		{
			GameObject g = Instantiate(_plant , new Vector3(0,0,i) , Quaternion.identity);
			g.transform.localScale = new Vector3(1,1,1);
		}
		_seedSpotManager.RemoveSeedSpotsFromActiveSeedSpotList(gameObject.GetComponentInParent<SeedSpot>());
		Destroy(this.gameObject);
	}
}