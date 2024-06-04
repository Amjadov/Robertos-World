using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
		public float healthBonus;				// How much health the crate gives the player.
		public AudioClip collect;				// The sound of the crate being collected.


		void Awake ()
		{

		}

		void OnTriggerEnter2D (Collider2D other)
		{
				// If the player enters the trigger zone...
				if (other.tag == "Player") {
						// Get a reference to the player health script.
						PlayerHealth playerHealth = other.GetComponent<PlayerHealth> ();

						// Increasse the player's health by the health bonus but clamp it at 100.
						playerHealth.GainHealth (healthBonus);
		
						// Play the collection sound.
						AudioSource.PlayClipAtPoint (collect, transform.position);

						// Destroy the crate.
						Destroy (gameObject);
				}
				

		}
}
