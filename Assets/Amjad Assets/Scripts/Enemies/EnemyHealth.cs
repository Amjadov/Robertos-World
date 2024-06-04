using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
		public float moveSpeed = 2f;		// The speed the enemy moves at.
		public int HP = 2;					// How many times the enemy can be hit before it dies.
		public float hurtForce = 10f;
		public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
		public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
		public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
		public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
		public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
		public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
		public Animator AnimatorObj;
		private SpriteRenderer ren;			// Reference to the sprite renderer.
		private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	[HideInInspector] 
		public bool dead = false;			// Whether or not the enemy is dead.

		private Score score;				// Reference to the Score script.
		private SpriteRenderer HealthBar;
		private Vector3 healthScale;
		public float repeatDamagePeriod = 1f;
		public float lastHitTime = 0f;
	public bool UseObjectPooler = false;
	private int OriginalHP;
		void Awake ()
		{
		OriginalHP = HP;
				// Setting up the references.
				ren = transform.Find ("body").GetComponent<SpriteRenderer> ();
				frontCheck = transform.Find ("frontCheck").transform;
				score = GameObject.Find ("Score").GetComponent<Score> ();
				HealthBar = transform.FindChild ("HealthLine").FindChild ("HealthBar").GetComponent<SpriteRenderer> ();
				healthScale = HealthBar.transform.localScale;

		}

		public void Hurt (Transform tPlayer, bool AirAttack = false)
		{

				//if (AirAttack == true && (Time.time > lastHitTime + repeatDamagePeriod)) {
				//				return;
				//		}
				if (dead)
						return;

				// Play a random audioclip from the deathClips array.
				int i = Random.Range (0, deathClips.Length);
				AudioSource.PlayClipAtPoint (deathClips [i], transform.position);
						
				Vector3 hurtVector = transform.position - tPlayer.position + Vector3.up * 5f;
				rigidbody2D.AddForce (hurtVector * hurtForce);
				// Reduce the number of hit points by one.
				HP--;
				lastHitTime = Time.time; 
				UpdateHealthBar ();
				if (HP <= 0 && !dead)
						Death ();


				//}
		}
	
		public void Death ()
		{
				// Create a vector that is just above the enemy.

				// Find all of the sprite renderers on this object and it's children.
				SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer> ();

				// Disable all of them sprite renderers.
				foreach (SpriteRenderer s in otherRenderers) {
						s.enabled = false;
				}
				MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour> ();
				foreach (MonoBehaviour script in scripts) {
						if (script.name != this.name)
								script.enabled = false;
				}
				

				// Increase the score by 100 points
				score.score += 100;

				// Set dead to true.
				dead = true;

		if (!UseObjectPooler) {
						// Allow the enemy to rotate and spin it by adding a torque.
			// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
			ren.enabled = true;
			ren.sprite = deadEnemy;
						rigidbody2D.fixedAngle = false;
						rigidbody2D.AddTorque (Random.Range (deathSpinMin, deathSpinMax));
				}
				// Find all of the colliders on the gameobject and set them all to be triggers.
				Collider2D[] cols = GetComponents<Collider2D> ();
				foreach (Collider2D c in cols) {
						//c.isTrigger = true;
			c.enabled = false;
		}

				// Play a random audioclip from the deathClips array.
				int i = Random.Range (0, deathClips.Length);
				AudioSource.PlayClipAtPoint (deathClips [i], transform.position);
				Vector3 scorePos;
				scorePos = transform.position;
				scorePos.y += 1.5f;
				Instantiate (hundredPointsUI, scorePos, Quaternion.identity);
				StartCoroutine (DestroyObj ());
				// Instantiate the 100 points prefab at this point.

		}

		IEnumerator DestroyObj ()
		{

				yield return new WaitForSeconds (3);
		if (UseObjectPooler) {
			RepairObject();
			gameObject.SetActive(false); 
				} else {
			Destroy (gameObject);  
				}
				
		
		}
	public void RepairObject ()
	{
		// Create a vector that is just above the enemy.
		
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer> ();
		
		// Disable all of them sprite renderers.
		foreach (SpriteRenderer s in otherRenderers) {
			s.enabled = true;
		}
		MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour script in scripts) {
			if (script.name != this.name)
				script.enabled = true;
		}
	
		
		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = true;

		
		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D> ();
		foreach (Collider2D c in cols) {
			//c.isTrigger = true;
			c.enabled = true;
		}
		HP = OriginalHP;
		dead = false;
		UpdateHealthBar (); 
	
		
	}
		public void UpdateHealthBar ()
		{
				// Set the health bar's colour to proportion of the way between green and red based on the Enemy's health.
				HealthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - HP * 0.01f);
		
				// Set the scale of the health bar to be proportional to the Enemy's health.
				HealthBar.transform.localScale = new Vector3 (healthScale.x * HP * 0.01f, 1, 1);
		}

}
