using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	[SerializeField]
	private List<PlantStage> _plantStagesList = new List<PlantStage>();

	private short _health;
	private bool _beenPlaced;
	private bool _attackInProgress = false;
	private int _currentPlantStage;

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
		_currentPlantStage--;
		if(_currentPlantStage <= 0) {
			Destroy(this.gameObject);
		} else {
			_plantStagesList[_currentPlantStage].gameObject.SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider c) 
	{
		if(c.tag == "Player") 
		{
		
		} else if(c.GetComponentInParent<PickUpItem>()!= null)
		{	
			if(c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Tool && _currentPlantStage == _plantStagesList.Count)
				Harvest();
		} else if(c.GetComponent<EnemyAgent>() != null) 
		{
			_attackInProgress = true;
			StartCoroutine(GetDamagedRoutine(c.GetComponent<EnemyAgent>()));
		} 
	}	


	private void OnTriggerExit(Collider c) 
	{
		if(c.GetComponent<EnemyAgent>() != null) 
			{
				_attackInProgress = false;
			}
	}

	private void DecreaseHealth(int damage) 
	{
		_health -= (short)damage;
	}

	private IEnumerator GetDamagedRoutine(EnemyAgent enemyAgentAttacker) 
	{
		if(_attackInProgress) 
		{
			while(_health > 0) 
			{
					yield return new WaitForSeconds(enemyAgentAttacker.GetAttackDelay());
					DecreaseHealth(enemyAgentAttacker.GetAttackDamage());
					yield return null;
			}
			if(_health <= 0) 
			{
				Cease();
			}
		}
		yield return null;		
	}

	public void ResetStage() 
	{
	}

	private void Harvest() 
	{
		Instantiate(this.gameObject).GetComponent<Plant>();
		Destroy(this.gameObject);
	}
}