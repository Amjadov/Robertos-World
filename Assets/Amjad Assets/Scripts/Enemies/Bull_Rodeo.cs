using UnityEngine;
using System.Collections;

public class Bull_Rodeo : MonoBehaviour
{
		public float moveSpeed = 2f;		// The speed the enemy moves at.
		public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
		public AudioClip HitClip;		// An array of audioclips that can play when the enemy dies.
		public AudioClip BullCharge;
		public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
		public Animator anim;
		public Transform PatrolA;
		public Transform PatrolB;
		private bool PatrolRight = true;
		private Transform target;
		public bool isChasing = false;
		private Transform trail1;
		private Transform trail2;
		public bool ChaseRunningNow = false;
		public  Vector3 TargetX;
		private Transform prevCol;
		public string LastPlayingAnimation;
		public bool FlipObject = false;
		private float WaitTime = 0.5f;
		private float lastHitTime;
		private bool Speed1Applied = false;
		private bool Speed2Applied = false;

		void Awake ()
		{


				anim = GetComponent<Animator> (); 
				target = GameObject.FindGameObjectWithTag ("Player").transform; 
				trail1 = transform.Find ("trail1");  
				trail2 = transform.Find ("trail2"); 
		}

		void Update ()
		{
				if (Time.timeScale < 1) {
						audio.Stop (); 
						return;
				}


				if (Time.time > lastHitTime + WaitTime && isChasing == false) {
						isChasing = true;
						anim.SetTrigger ("Charge");
						TargetX = target.position;
						lastHitTime = Time.time;
				}

	
				anim.SetFloat ("Speed", rigidbody2D.velocity.x); 
				if (isChasing || ChaseRunningNow) {
						if (!trail1.gameObject.activeInHierarchy) 
								trail1.gameObject.SetActive (true);
						if (!trail2.gameObject.activeInHierarchy) 
								trail2.gameObject.SetActive (true);

						lastHitTime = Time.time;

						Chase ();
//				} else if (!isChasing && !ChaseRunningNow) {
//						if (trail1.gameObject.activeInHierarchy) 
//								trail1.gameObject.SetActive (false);
//						if (trail2.gameObject.activeInHierarchy) 
//								trail2.gameObject.SetActive (false);
//						Patrol ();
				} else if (!ChaseRunningNow) {
						if (transform.position.x < target.position.x) {
								if (!facingRight ())
										Flip ();

						} else if (transform.position.x > TargetX.x) {
								if (facingRight ())
										Flip ();
						}
				}

		}
//	public string GetCurrentPlayingAnimationClip {
//				get {
//			
//						foreach (AnimationState anime in anim.animation) {
//				if (anim.animation.IsPlaying(anime.name)) {
//										return anime.name;
//								}
//								{
//					
//										return string.Empty;
//								}
//						}
//			return string.Empty ;
//				}
//		}
		public void Chase ()
		{
				if (LastPlayingAnimation != "Charge") {
						// if (!anim.animation.IsPlaying("Charge")){

						anim.SetTrigger ("Charge");

						LastPlayingAnimation = "Charge";
				}
				if (!ChaseRunningNow) { 
						 
						int BullSoundChance = Random.Range (0, 3);
						if (BullSoundChance == 1) {
								AudioSource.PlayClipAtPoint (BullCharge, transform.position, 0.3f);
						} 
						audio.Play ();
						ChaseRunningNow = true;
				}
				if (facingRight ()) {
						//transform.Translate (new Vector3 (1, 0, 0) * moveSpeed);
						transform.rigidbody2D.velocity = new Vector2 (moveSpeed, transform.rigidbody2D.velocity.y);  
				} else {
						transform.rigidbody2D.velocity = new Vector2 (-moveSpeed, transform.rigidbody2D.velocity.y);
						//transform.Translate (new Vector3 (-1, 0, 0) * moveSpeed);
						
				} 
		}

		void Patrol ()
		{
				audio.Stop ();
				if (PatrolRight) {
						if (transform.position.x >= PatrolB.position.x) {

								PatrolRight = false;

						}

				} else {
						if (transform.position.x <= PatrolA.position.x) {

								PatrolRight = true;
				
						}

				}
				if (PatrolRight) {
						if (!facingRight ()) {
								Flip ();
						}
						rigidbody2D.velocity = new Vector2 (moveSpeed, rigidbody2D.velocity.y);	
				} else {
						if (facingRight ()) {
								Flip ();
						}
						rigidbody2D.velocity = new Vector2 (moveSpeed * -1, rigidbody2D.velocity.y);	
				}

		}

		bool facingRight ()
		{
				if (!FlipObject) { 
						if (transform.localScale.x < 0) {
								return true;
						} else {
								return false;
						}
				} else {
						if (transform.localScale.x < 0) {
								return false;
						} else {
								return true;
						}


				}
		}

		public void Flip ()
		{
				// Multiply the x component of localScale by -1.
				Vector3 enemyScale = transform.localScale;
				enemyScale.x *= -1;
				transform.localScale = enemyScale;
		}

		void OnCollisionEnter2D (Collision2D col)
		{


				if (isChasing && prevCol != col.transform) {
//			if (prevCol)
						//Debug.Log(prevCol.name + "......." +  col.transform.name);
						if (col.transform.tag == "Wall" || col.transform.tag == "Player") {
								  
								if (col.transform.GetComponent<RodeoFence> ()) {
										col.transform.GetComponent<RodeoFence> ().TakeDamage (transform); 
										if (col.transform.GetComponent<RodeoFence> ().health < 80 && col.transform.GetComponent<RodeoFence> ().health > 20 && !Speed1Applied) {
												//moveSpeed = 0.4f;
												moveSpeed = moveSpeed * 1.15f;
												Speed1Applied = true;
										} else if (col.transform.GetComponent<RodeoFence> ().health <= 20 && !Speed2Applied) {// && col.transform.GetComponent<RodeoFence> ().health > 10) {
												//moveSpeed = 0.5f;
												moveSpeed = moveSpeed * 1.25f;
												Speed2Applied = true;
										}
								}
								if (LastPlayingAnimation != "Idle") {
										anim.SetTrigger ("Idle");
										LastPlayingAnimation = "Idle";
								}
								audio.Stop ();
								if (trail1.gameObject.activeInHierarchy) 
										trail1.gameObject.SetActive (false);
								if (trail2.gameObject.activeInHierarchy) 
										trail2.gameObject.SetActive (false);
								Camera.main.GetComponent<PerlinShake> ().PlayShake (); 

								if (col.transform.tag == "Wall")
										AudioSource.PlayClipAtPoint (HitClip, transform.position);

								isChasing = false;
								ChaseRunningNow = false;
								transform.rigidbody2D.velocity = new Vector2 (0, transform.rigidbody2D.velocity.y);
						}


				}
				if (prevCol == col.transform && isChasing == true) {
						isChasing = false;
						transform.rigidbody2D.velocity = new Vector2 (0, transform.rigidbody2D.velocity.y);
						ChaseRunningNow = false;
				}
				if (col.transform.name != "Ground Collider") {
						transform.rigidbody2D.velocity = new Vector2 (0, transform.rigidbody2D.velocity.y);
						PushPlayerBack (col.transform);
						prevCol = col.transform;

				}

		}

		public void PushPlayerBack (Transform enemy)
		{
				// Create a vector that's from the enemy to the player with an upwards boost.
				//Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f; //* enemy.rigidbody2D.velocity.x ;
				Vector3 hurtVector;
				if (transform.position.x > enemy.position.x) {
						hurtVector = transform.position - enemy.position + Vector3.right * 9f;  //* enemy.rigidbody2D.velocity.x ;
				} else {
						hurtVector = transform.position - enemy.position + Vector3.left * 9f;
				}
				hurtVector = new Vector3 (hurtVector.x * 2f, hurtVector.y, hurtVector.z);
				// Add a force to the player in the direction of the vector and multiply by the hurtForce.
				rigidbody2D.AddForce (hurtVector * 5f);
				//rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y); //+ enemy.rigidbody2D.velocity.x);  
		}

		public void LookAtPlayer ()
		{
				if (transform.position.x > target.transform.position.x && facingRight ()) {
						Flip ();
				} else if (transform.position.x < target.transform.position.x && !facingRight ()) {
						Flip ();
				}


		}

}
