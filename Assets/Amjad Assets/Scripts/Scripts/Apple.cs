using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour 
{
	public bool UseObjectPooler = false;

	void Start () 
	{

		Invoke ("isFinished", 2);

	}
	void OnEnable () {
		if (UseObjectPooler) 
		Invoke ("isFinished", 2);
		}
	void OnCollisionEnter2D (Collision2D col) 
	{
		// If it hits an enemy...
		if(col.gameObject.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(transform);

			CollideOFF();
			Invoke("CollideON",2);
			Invoke("isFinished",2);

		}
		
	}
	void isFinished(){
		if (UseObjectPooler) {
						gameObject.SetActive (false);
				} else {
						Destroy (gameObject, 0);
				}


	}
	void CollideON(){
			collider2D.enabled = true;
	}
	void CollideOFF(){
			collider2D.enabled = false;
		
	}

}
