using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


	private int _waveCounter;
	private bool _waveInProgress = false;

	private void Start() 
	{
		_waveCounter = 0;
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
		
	}

	private Vector3 RandomSpawner() 
	{
		return _spawnerLocations[Random.Range(0,_spawnerLocations.Count-1)].position;
	}

	private void SpawnEnemy() 
	{
		_spawnedEnemyList.Add(Instantiate(_enemy, RandomSpawner(), Quaternion.identity));
	}

	private IEnumerator SpawnWaveRoutine(float delay) 
	{
		int enemyCount = _enemyStartCount;
		while(enemyCount > 0) 
		{
			enemyCount--;
			SpawnEnemy();
			yield return new WaitForSeconds(delay);
		}
	StartCoroutine(WaveInProgressRoutine());
	}

	private IEnumerator WaveInProgressRoutine() 
	{
		short remainingEnemies = (short)_spawnedEnemyList.Count;
		while(_spawnedEnemyList.Count != 0) 
		{
			yield return new WaitForSeconds(_waveCheckDelay);
			short counter = 0;
			for(int i = 0; i<=_spawnedEnemyList.Count-1;i++) 
			{
				if(_spawnedEnemyList[i] == null) 
				{
					counter++;
					if(counter == _spawnedEnemyList.Count) 
					{
						_spawnedEnemyList.Clear();
						FinishWave();
						yield return null;
					}
				}
			}
		}  
			yield return null;
	}
}