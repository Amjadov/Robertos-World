using UnityEngine;
using System.Collections;

public class Bull_Charge : MonoBehaviour {

	public Transform bull;
	private bool AlreadyEntered = false;
	void OnTriggerEnter2D (Collider2D col) 
	{

		if (col.gameObject.tag == "Player" && bull.GetComponent<Bull_Rodeo>().isChasing == false && AlreadyEntered == false) {
			bull.GetComponent<Bull_Rodeo>().LookAtPlayer();
			bull.GetComponent<Bull_Rodeo>().isChasing = true;
			bull.GetComponent<Animator>().SetTrigger("Charge");
			bull.GetComponent<Bull_Rodeo>().ChaseRunningNow = true;
			bull.GetComponent<Bull_Rodeo>().TargetX = col.transform.position;
			AlreadyEntered = true;
		}
		
	}
	void OnTriggerExit2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player" && AlreadyEntered == true ) {

			AlreadyEntered = false;
//			if (bull){ 
//			bull.GetComponent<Enemy>().isChasing = false; 
//			}
		}
		
	}

}
