using UnityEngine;
using System.Collections;

public class RodeoFence : MonoBehaviour
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
	private Animator anim;						// Reference to the Animator on the player
	public SpriteRenderer healthBar;
	private bool isDead = false;
	private bool MoveCamera = false;
	void Awake ()
	{

		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;

	 
	}
		
	void FixedUpdate(){

//		if (transform.position.y < 5f) {
//			KillPlayer(); 
//
//				}
		if (MoveCamera)
		Camera.main.transform.position =  new Vector3(Mathf.Lerp (Camera.main.transform.position.x, 68f, Time.deltaTime),Camera.main.transform.position.y,Camera.main.transform.position.z);

		}
	public void KillPlayer(){
		if (isDead)
			return;


		GameObject.Find ("music").audio.Stop ();
		AudioSource.PlayClipAtPoint(LoseClip, transform.position);

		health = 0f;
		UpdateHealthBar ();
		// Find all of the colliders on the gameobject and set them all to be triggers.
//		Collider2D[] cols = GetComponents<Collider2D>();
//		foreach(Collider2D c in cols)
//		{
//			c.isTrigger = true;
//		}

		MoveCamera = true;
		// Move all sprite parts of the player to the front
		SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer s in spr)
		{
			s.sortingLayerName = "UI";
		}
		
		

		
		// ... Trigger the 'Die' animation state
		anim.SetTrigger("Die");
		isDead = true;
	

		}
	
//	void OnCollisionEnter2D (Collision2D col)
//	{
//		// If the colliding gameobject is an Enemy...
//		if(col.gameObject.name == "Bull" && col.gameObject.transform.GetComponent<Bull_Rodeo>().isChasing == true)
//		{
//
//				// ... and if the player still has health...
//				if(health > 0f)
//				{
//					// ... take damage and reset the lastHitTime.
//					TakeDamage(col.transform); 
//				}
//				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
//				else
//				{
//					KillPlayer ();
//				}
//			
//		}
//	}


	public void TakeDamage (Transform enemy)
	{
		if (isDead == true)
						return;

		// Reduce the player's health by 10.
		health -= damageAmount;

		//this is if you are using a health bar animator
		//Healthp.SetFloat ("Health", health / 10);
		int hl = (int)Mathf.FloorToInt (health / 10);
		if (hl > 0)

		// Update what the health bar looks like.
		UpdateHealthBar();

		if (health < 1) {
						KillPlayer (); 
				} else {

			anim.SetTrigger("Move"); 
				}
//		// Play a random clip of the player getting hurt.
//		int i = Random.Range (0, ouchClips.Length);
//		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
