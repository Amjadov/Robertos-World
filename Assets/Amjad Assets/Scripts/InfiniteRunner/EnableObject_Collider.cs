using UnityEngine;
using System.Collections;

public class EnableObject_Collider : MonoBehaviour {
	public Transform ObjectToEnable;
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
			ObjectToEnable.gameObject.SetActive(true); 
		}
		
	}
}
