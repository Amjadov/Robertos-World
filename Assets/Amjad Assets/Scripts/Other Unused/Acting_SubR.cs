using UnityEngine;
using System.Collections;

public class Acting_SubR : MonoBehaviour {
	public float HeroPositionRight = 1.236332f;
	public float HeroPositionLeft = -20.62f;
	public float Enemy1PositionRight = 5.990457f;
	public float Enemy1PositionLeft = -22.4f;
	public float Enemy2PositionRight = 8.424181f;
	public float Enemy2PositionLeft = -25.3f;
	public float Enemy3PositionRight = 11.7f;
	public float Enemy3PositionLeft = -29.43f;
	private Transform  trns;
	public Transform HeroAct;
	public Transform Enemy1Act;
	public Transform Enemy2Act;
	public Transform Enemy3Act;
	public Transform HeroSpawner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	
		void OnTriggerEnter2D (Collider2D col) 
		{
		//Debug.Log (col.gameObject.GetComponent<ActingMain>().facingRight);
			// If it hits an enemy...
		if(col.tag == "Player" | col.tag == "Enemy1" | col.tag == "Enemy2" | col.tag == "Enemy3")
			{
			if (!col.gameObject.GetComponent<ActingMain>().facingRight)
			{
				// ... find the Enemy script and call the Hurt function.
				col.gameObject.rigidbody2D.isKinematic = true;  //GetComponent<ActingMain>().enabled = false;
			float seconds = 5;	
			//	Debug.Log (seconds); 
				while (seconds <= 0)
				{
			seconds -= 1 * Time.deltaTime;
				}
			//	Debug.Log (seconds); 
			//if(seconds <= 0) {
			//		Debug.Log (seconds); 
				//yield return new WaitForSeconds(5.0F);
				//trns = col.gameObject.transform; 

				//Debug.Log (trns.name);
				if (col.tag == "Player"){
					Destroy(col.gameObject);
					Instantiate(HeroAct,HeroSpawner.position  , HeroSpawner.rotation) ; 
					//trns.GetComponent<ActingMain>().facingRight = true; 
					//trns.position = new Vector3(HeroPositionLeft,trns.position.y);
					//Debug.Log (trns.position.x);
				} else if(col.tag == "Enemy1"){
					Destroy(col.gameObject);
					Instantiate(Enemy1Act,HeroSpawner.position  , HeroSpawner.rotation) ; 
					//trns.position = new Vector3(Enemy1PositionLeft,trns.position.y);
				} else if(col.tag == "Enemy2"){
					Destroy(col.gameObject);
					Instantiate(Enemy2Act,HeroSpawner.position  , HeroSpawner.rotation) ; 
					//trns.position = new Vector3(Enemy2PositionLeft,trns.position.y);
				} else if(col.tag == "Enemy3"){
					Destroy(col.gameObject);
					Instantiate(Enemy3Act,HeroSpawner.position  , HeroSpawner.rotation) ; 
					//trns.position = new Vector3(Enemy3PositionLeft,trns.position.y);
				}
				//col.gameObject.GetComponent<ActingMain>().facingRight = true;
				//col.gameObject.rigidbody2D.isKinematic = false;
			//}
			}
				
			}
			
			
	
	}
}
