using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	[SerializeField]
	private List<PlantStage> _plantStagesList = new List<PlantStage>();
	[SerializeField]
	private short _health;

	private int _currentPlantStage;

	void Start () 
	{
		_currentPlantStage = 0;
	}
	
	[ContextMenu("Grow")]
	public void Grow() 
	{
		_plantStagesList[_currentPlantStage].gameObject.SetActive(false);
		_currentPlantStage++;
		if(_currentPlantStage >= _plantStagesList.Count) {
			Harvest();
		} else 
		{
			_plantStagesList[_currentPlantStage].gameObject.SetActive(true);
		}
		
	}

	private void OnTriggerEnter(Collider c) 
	{
		if(c.tag == "Player") 
		{

		} else
		{	/* 
			if(c.GetComponentInParent<PickUpItem>().GetPickUpItemType() == PickUpItemType.Weapon) 
			{
				Debug.Log("Weapon Hit");
				DecreaseHealth(c.GetComponentInParent<PickUpItem>().GetDamageAmount());
			} 
			*/
		}	
	}

	private void Harvest() 
	{
	}
}