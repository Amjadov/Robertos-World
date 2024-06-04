using UnityEngine;
using System.Collections;

public class RollingRock : MonoBehaviour
{
		//[HideInInspector]
		public bool facingRight = true;
		public float speed = 5;
		public float maxSpeed = 20;
		public float RotationSpeed = 150f;
		private bool walled = false;
		private bool rightwalled = false;
		private bool leftwalled = false;
		private Transform rightCheck;			// A position marking where to check if the player is grounded.	
		private Transform rightChecks;			// A position marking where to check if the player is grounded.	
		private Transform leftCheck;			// A position marking where to check if the player is grounded.	
		public LayerMask WhatIsWall;
		private Transform leftChecks;
		private Transform body;
		private RaycastHit2D leftHit;
		private RaycastHit2D rightHit;
		private bool GoDownLadder = false;
		private Transform LadderTop;
		private Transform LadderBottom;
		public GameObject explosion;		// Prefab of explosion effect
		public bool IgnoreEnemy = true;

		private 
		void Awake ()
		{
				rightCheck = transform.Find ("rightCheck");
				leftCheck = transform.Find ("leftCheck");
				//rightChecks = transform.Find ("rightChecks");
				//leftChecks = transform.Find ("leftChecks");
				body = transform.Find ("body");
				if (IgnoreEnemy)
						Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("RollingObject"), LayerMask.NameToLayer ("Enemies"), true);
		}

		void FixedUpdate ()
		{
				if (transform.position.y <= 5f) {
						Destroy (gameObject);
						return;
				}
				if (GoDownLadder) {
//			Debug.Log ("T="+Mathf.Round(transform.position.y * 100) / 100);
//			Debug.Log ("L="+Mathf.Round(LadderBottom.position.y * 100) / 100);

						//if (Vector3.Distance (transform.position, LadderBottom.position) >= 0f) {
						if ((Mathf.Round (transform.position.y * 100) / 100 - Mathf.Round (LadderBottom.position.y * 100) / 100) > 1) {
								return;
						} else {
								Flip ();
								GoDownLadder = false;
						}
				}
				rightwalled = Physics2D.Linecast (transform.position, rightCheck.position, WhatIsWall); 
				leftwalled = Physics2D.Linecast (transform.position, leftCheck.position, WhatIsWall);

				if (rightwalled | leftwalled)
						walled = true;
				else
						walled = false;
				if (walled) {
						Flip ();
				}
				if (facingRight) {
						//rigidbody2D.velocity = new Vector2 (speed, 0); 
						rigidbody2D.AddForce (new Vector2 (speed, 0));
						body.Rotate (Vector3.forward * Time.deltaTime * -150, Space.World); 
				} else {

						//rigidbody2D.velocity = new Vector2 (speed * -1, 0);
						rigidbody2D.AddForce (new Vector2 (speed * -1, 0));
						body.Rotate (Vector3.forward * Time.deltaTime * RotationSpeed, Space.World);   
				}

				if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
					// ... set the player's velocity to the maxSpeed in the x axis.
						rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.transform.tag == "Wall") {
						Flip ();
				} else if (col.gameObject.name == "LadderTop") {
						int LadderDownChance = Random.Range (0, 2);
//						Debug.Log ("Chance=" + LadderDownChance);
						if (LadderDownChance == 1) {
								LadderBottom = col.gameObject.transform.parent.Find ("LadderBottom");
								LadderTop = col.gameObject.transform;
								transform.position = Vector3.Lerp (LadderTop.position, LadderBottom.position, 0.15f);
//								Debug.Log (LadderTop.position.y);
//								Debug.Log (LadderBottom.position.y);
								GoDownLadder = true;
						} else {

								GoDownLadder = false;
						}
				} else if (col.gameObject.tag == "Player") {

						// ... find the Enemy script and call the Hurt function.
						col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (transform);
						col.gameObject.GetComponent<PlayerHealth> ().KillPlayer ();
				} else if (col.gameObject.tag == "Enemy") {
			
						// ... find the Enemy script and call the Hurt function.
						col.gameObject.GetComponent<EnemyHealth> ().Death (); 
				} else if (col.gameObject.tag == "PlayerAssist") {
						if (col.gameObject.GetComponent<SmoothFollow2> ().ChargeMode || col.gameObject.GetComponent<SmoothFollow2> ().DemandedAttackMode) {
								OnExplode ();
								Destroy (gameObject);
						}
				}

		}

		void Flip ()
		{

				facingRight = !facingRight;
		}

		void OnExplode ()
		{
				// Create a quaternion with a random rotation in the z-axis.
				Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f, 360f));
		
				// Instantiate the explosion where the rocket is with the random rotation.
				Instantiate (explosion, transform.position, randomRotation);
		}
}
