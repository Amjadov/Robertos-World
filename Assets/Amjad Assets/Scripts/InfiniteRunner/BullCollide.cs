using UnityEngine;
using System.Collections;

public class BullCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ground")  ) {
			transform.parent.audio.Stop ();
//			transform.parent.GetComponent<PlayerHealth>().PushPlayerBack(col.transform);  
			transform.parent.GetComponent<PlayerHealth>().KillPlayer();  
		}

	}
}
