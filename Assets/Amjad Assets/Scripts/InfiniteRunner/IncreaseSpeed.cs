using UnityEngine;
using System.Collections;

public class IncreaseSpeed : MonoBehaviour {
	public float NewMaxSpeed = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
			col.transform.GetComponent<PlayerControl_InfiniteRunner>().maxSpeed = NewMaxSpeed;
		}
		
	}
}
