
using UnityEngine;
using System.Collections;

public class CandyPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.

	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the Candy is Collected.
	private Animator anim;				// Reference to the animator component.
	private bool landed = false;		// Whether or not the crate has landed yet.
	private Score score;				// Reference to the Score script.

	void Awake()
	{
		// Setting up the reference.
		//anim = transform.root.GetComponent<Animator>();
		anim = transform.GetComponent<Animator>();
		score = GameObject.Find("Score").GetComponent<Score>();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			//anim.SetTrigger("Collected");
			// Increase the number of bombs the player has.
			//other.GetComponent<LayBombs>().bombCount++;
			//yield new WaitForSeconds(anim("Collected").length);//anim("Collected").length);
			//StartCoroutine(DoAnimation());  
			// Destroy the crate.
			score.score += 10;
			// Create a vector that is just above the Candy.
			Vector3 scorePos;
			scorePos = transform.position;
			scorePos.y += 1.5f;
			// Instantiate the 10 points prefab at this point.
			Instantiate(hundredPointsUI, scorePos, Quaternion.identity);

			Destroy(transform.gameObject);
		}

	}


}
