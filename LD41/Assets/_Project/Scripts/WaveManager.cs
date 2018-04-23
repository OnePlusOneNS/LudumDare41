using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {

	[SerializeField]
	private List<Transform> _spawnerLocations = new List<Transform>();

	[SerializeField]
	private GameObject _enemy;
	private List<GameObject> _spawnedEnemyList = new List<GameObject>();

	[SerializeField]
	private float _spawnDelayLow = 3f, _spawnDelayMedium = 4f, _spawnDelayHigh = 5f, _spawnDelayVeryHigh = 6f;
	[SerializeField]
	private int _enemyStartCount = 2;
	[SerializeField]
	private float _waveCheckDelay;
	[SerializeField]
	private SeedSpotManager _seedSpotManager;
	[SerializeField]
	private GameObject _enemyParent;


	private int _enemyCounter;
	private int _waveCounter;
	private bool _waveInProgress = false;

	private void Start() 
	{
		_waveCounter = 0;
	}

	void Update()	
	{
		if(_waveInProgress) 
		{
			if(_enemyParent.transform.childCount == 0) 
			{
				_spawnedEnemyList.Clear();
				FinishWave();
			} else if(_seedSpotManager.GetActiveSeedSpotsCount() <= 0) 
			{
				Time.timeScale = 0;
			}
		}
	}

	public void LaunchWave() 
	{
		if(!_waveInProgress) 
		{
			_waveInProgress = true;
			if(_waveCounter < 5) 
			{
				_waveCounter++;
				StartCoroutine(SpawnWaveRoutine(_spawnDelayVeryHigh));
			} else if (_waveCounter >= 5 && _waveCounter < 10) 
			{
				_waveCounter++;
				StartCoroutine(SpawnWaveRoutine(_spawnDelayHigh));
			} else if (_waveCounter >= 10 && _waveCounter < 15) 
			{
				_waveCounter++;
				StartCoroutine(SpawnWaveRoutine(_spawnDelayMedium));
			} else if (_waveCounter > 15) 
			{
				_waveCounter++;
				StartCoroutine(SpawnWaveRoutine(_spawnDelayLow));
			}
		}
	}

	private void FinishWave() 
	{	
		_seedSpotManager.PlantsGrow();
		_seedSpotManager.StopWater();
		_waveInProgress = false;
		_seedSpotManager.CheckIfDone();
	}

	private Vector3 RandomSpawner() 
	{
		return _spawnerLocations[Random.Range(0,_spawnerLocations.Count-1)].position;
	}

	private void SpawnEnemy() 
	{
		Instantiate(_enemy, RandomSpawner(), Quaternion.identity).gameObject.transform.parent = _enemyParent.transform;
	}

	private IEnumerator SpawnWaveRoutine(float delay) 
	{
		int enemyCount = _enemyStartCount + _waveCounter;
		enemyCount--;
		SpawnEnemy();
		while(enemyCount > 0) 
		{
			yield return new WaitForSeconds(delay);
			SpawnEnemy();
			enemyCount--;
		}
	}
}