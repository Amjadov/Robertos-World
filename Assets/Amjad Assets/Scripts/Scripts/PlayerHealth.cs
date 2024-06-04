using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
		public float health = 100f;					// The player's health.
		public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
		public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
		public AudioClip LoseClip;				// Array of clips to play when the player is damaged.
		public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
		public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

		//private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
		private float lastHitTime;					// The time at which the player was last hit.
		private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
		private PlayerControl playerControl;		// Reference to the PlayerControl script.
		private Animator anim;						// Reference to the Animator on the player
		public SpriteRenderer healthBar;
		public Animator Healthp;
		private MainHealthBar HealthProgress;
		private bool isDead = false;

		void Awake ()
		{
				// Setting up references.
				playerControl = GetComponent<PlayerControl> ();


				//healthBar = transform.Find("HealthBar").GetComponent<SpriteRenderer>();
				anim = GetComponent<Animator> ();

				// Getting the intial scale of the healthbar (whilst the player has full health).
				healthScale = healthBar.transform.localScale;
				HealthProgress = GameObject.Find ("MainHealthBar").GetComponent<MainHealthBar> ();

	 
		}
		
		void FixedUpdate ()
		{

				if (transform.position.y < 5f) {
						KillPlayer (); 

				}



		}

		public void KillPlayer ()
		{
				if (isDead)
						return;
				if (GameManager.instance.invincibleMode && transform.position.y > 5f)
						return;

				GameObject.Find ("music").audio.Stop ();
				AudioSource.PlayClipAtPoint (LoseClip, transform.position);

				rigidbody2D.gravityScale = 1f;
				health = 0f;
				UpdateHealthBar ();
				// Find all of the colliders on the gameobject and set them all to be triggers.
//		Collider2D[] cols = GetComponents<Collider2D>();
//		foreach(Collider2D c in cols)
//		{
//			c.isTrigger = true;
//		}


				// Move all sprite parts of the player to the front
				SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer s in spr) {
						s.sortingLayerName = "UI";
				}
		
		
				if (transform.GetComponent<PlayerControl> ()) 
						GetComponent<PlayerControl> ().enabled = false;

				if (transform.GetComponent<PlayerControl_InfiniteRunner> ()) 
						GetComponent<PlayerControl_InfiniteRunner> ().enabled = false;
				//... disable the Gun script to stop a dead guy shooting a nonexistant Gun
				if (transform.GetComponent<Gun> ()) 
						GetComponentInChildren<Gun> ().enabled = false;
		
				// ... Trigger the 'Die' animation state
				anim.SetTrigger ("Die");
				isDead = true;
				StartCoroutine (ReloadLevel ());
	

		}

		IEnumerator ReloadLevel ()
		{
		

				yield return new WaitForSeconds (2);
				
				Application.LoadLevel (Application.loadedLevelName);    
				
		
		}

		void OnCollisionEnter2D (Collision2D col)
		{
		if (!col.transform.gameObject)
						return;
				// If the colliding gameobject is an Enemy...
				if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "HurtfulObjects") {
						if (col.gameObject.tag == "Enemy") {

								if (col.transform.GetComponent<EnemyHealth> () != null) {
										if (Time.time <= col.transform.GetComponent<EnemyHealth> ().lastHitTime + col.transform.GetComponent<EnemyHealth> ().repeatDamagePeriod) {
												PushPlayerBack (col.transform);
												return;
										}
								}

						}
						// ... and if the time exceeds the time of the last hit plus the time between hits...
						if (Time.time > lastHitTime + repeatDamagePeriod) {
								// ... and if the player still has health...
								if (health > 0f) {
										// ... take damage and reset the lastHitTime.
										TakeDamage (col.transform); 
										lastHitTime = Time.time; 
								}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else {
										KillPlayer ();
								}
						}
				} else if (col.gameObject.tag == "Bomb") {
			col.gameObject.GetComponent<BombScript> ().OnExplode();
			col.gameObject.GetComponent<BombScript> ().DestroyObj();
			TakeDamage (col.transform);
			KillPlayer ();


				}
		}

		public void GainHealth (float AddedHealth = 0)
		{
				if (isDead == true)
						return;

				health += AddedHealth;
				health = Mathf.Clamp (health, 0f, 100f);
				int hl = (int)Mathf.FloorToInt (health / 10);
				if (hl >= 0)
						HealthProgress.NewTexture = HealthProgress.textures [hl]; 

				UpdateHealthBar ();

		}

		public void TakeDamage (Transform enemy)
		{
				if (isDead == true)
						return;
	
				// Make sure the player can't jump.
				if (playerControl) 
						playerControl.jump = false;

				PushPlayerBack (enemy);

				if (GameManager.instance.invincibleMode) 
						return;
				// Reduce the player's health by 10.
				if (enemy.gameObject.name == "Bull") {

						health -= damageAmount;
						health -= damageAmount;
						health -= damageAmount;


				} else {
						health -= damageAmount;
				}
				//this is if you are using a health bar animator
				//Healthp.SetFloat ("Health", health / 10);
				int hl = (int)Mathf.FloorToInt (health / 10);
				if (hl >= 0)
						HealthProgress.NewTexture = HealthProgress.textures [hl]; 

				// Update what the health bar looks like.
				UpdateHealthBar ();

				// Play a random clip of the player getting hurt.
				int i = Random.Range (0, ouchClips.Length);
				AudioSource.PlayClipAtPoint (ouchClips [i], transform.position);
				if (health < 0)
						KillPlayer ();
		}

		public void PushPlayerBack (Transform enemy)
		{
				// Create a vector that's from the enemy to the player with an upwards boost.
				Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f; //* enemy.rigidbody2D.velocity.x ;
		
				// Add a force to the player in the direction of the vector and multiply by the hurtForce.
				rigidbody2D.AddForce (hurtVector * hurtForce);
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y); //+ enemy.rigidbody2D.velocity.x);  
		}

		public void UpdateHealthBar ()
		{
				// Set the health bar's colour to proportion of the way between green and red based on the player's health.
				healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);

				// Set the scale of the health bar to be proportional to the player's health.
				healthBar.transform.localScale = new Vector3 (healthScale.x * health * 0.01f, 1, 1);
		}
}
