using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

	[SerializeField]
	private Texture2D _crossHair;

	void OnGUI() 
	{
		GUI.DrawTexture(new Rect(Screen.width/2 - _crossHair.width/2, Screen.height/2 - _crossHair.height/2, _crossHair.width, _crossHair.height), _crossHair);
	}
}
