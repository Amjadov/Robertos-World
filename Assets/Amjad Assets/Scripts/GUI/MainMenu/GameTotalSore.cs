using UnityEngine;
using System.Collections;

public class GameTotalSore : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float totalscore = PlayerPrefs.GetFloat("TotalScore");
		GameManager.instance.TotalScore = totalscore;
		transform.guiText.text = totalscore.ToString ("0000000"); 
	}
	

}
