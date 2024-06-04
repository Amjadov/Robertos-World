using UnityEngine;
using System.Collections;

public class PointCrateScript : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public GameObject ObjectInsideCrate;

	void Start () 
	{

	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerExit2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.gameObject.name == "HeadCollider")
		{
			audio.Play ();
			Instantiate(ObjectInsideCrate, transform.position,Quaternion.identity); 

//			do{
//
//			} while(audio.isPlaying); 

			DestroyCrate();

		}
		// Otherwise if it hits a bomb crate...

	}
	void DestroyCrate()
	{
		Destroy (gameObject);
	}

}
