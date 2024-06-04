using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
		public float moveSpeed = 2f;		// The speed the enemy moves at.
		public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
		public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
		public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
		public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
		public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
		public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
		public Animator anim;
		private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
		private bool dead = false;			// Whether or not the enemy is dead.
		private Score score;				// Reference to the Score script.
		public Transform PatrolA;
		public Transform PatrolB;
		private bool PatrolRight = true;
		private Transform target;
		public bool isChasing = false;
		private Transform trail1;
		private Transform trail2;
		public bool ChaseRunningNow = false;
	public  Vector3 TargetX;
		void Awake ()
		{
				// Setting up the references.
				frontCheck = transform.Find ("frontCheck").transform;
				anim = GetComponent<Animator> (); 
				target = GameObject.FindGameObjectWithTag ("Player").transform; 
				trail1 = transform.Find ("trail1");  
				trail2 = transform.Find ("trail2"); 
		}

		void FixedUpdate ()
		{
				if (isChasing || ChaseRunningNow) {
						if (!trail1.gameObject.activeInHierarchy) 
								trail1.gameObject.SetActive (true);
						if (!trail2.gameObject.activeInHierarchy) 
								trail2.gameObject.SetActive (true);
						Chase ();
				} else if (!isChasing && !ChaseRunningNow) {
						if (trail1.gameObject.activeInHierarchy) 
								trail1.gameObject.SetActive (false);
						if (trail2.gameObject.activeInHierarchy) 
								trail2.gameObject.SetActive (false);
						Patrol ();
				}
//		if (isChasing == false){
//			Patrol();
//		    }
		}

	public void Chase ()
		{
				anim.SetBool ("Charge", true); 
		if (transform.position.x < TargetX.x) {
						if (!facingRight ())
								Flip ();

						//rigidbody2D.velocity = new Vector2 (moveSpeed * 5, rigidbody2D.velocity.y);
			transform.Translate(new Vector3(1,0,0) * 0.5f);

		} else if (transform.position.x > TargetX.x) {
						if (facingRight ())
								Flip ();
			transform.Translate(new Vector3(-1,0,0) * 0.5f);
						//rigidbody2D.velocity = new Vector2 (moveSpeed * -5, rigidbody2D.velocity.y);
				} else {
						rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
				}

		}

		void Patrol ()
		{
				anim.SetBool ("Charge", false); 
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
			if (!facingRight ()){
				Flip ();}
						rigidbody2D.velocity = new Vector2 (moveSpeed, rigidbody2D.velocity.y);	
				} else {
			if (facingRight ()){
				Flip ();}
						rigidbody2D.velocity = new Vector2 (moveSpeed * -1, rigidbody2D.velocity.y);	
				}

		}

		bool facingRight ()
		{
				if (transform.localScale.x < 0) {
						return true;
				} else {
						return false;
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
				if (col.transform.tag == "Wall") {
						Camera.main.GetComponent<PerlinShake> ().PlayShake (); 
						audio.Play ();
				}

				if (col.transform.tag == "Wall" || col.transform.tag == "Player") {
						ChaseRunningNow = false;
				}
		}

}
