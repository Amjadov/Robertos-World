using UnityEngine;
using System.Collections;

public class Dragon_ShootFire : MonoBehaviour
{
		public Rigidbody2D FireBall;
		public float speed = 10f;				// The speed the fireball will fire at.
		public LayerMask WhatToShoot;
		private Transform frontcheck;
		private bool PlayerFound = false;
		private float lastHitTime;
		private Animator anim;
		private Vector3 shootingDirection;
		public float DelayBetweenShootings = 2f;
		private Transform target;
		private Transform bulletSpawner;
		public float shootingDistance = 10f;
		public bool VerticalShoot = false;
		public bool RandomShootingDuration = false;
		private float t = 0f;
		private float WantedRotation = 0;
		private float WantedXSpeed = 0;
		private float WantedYSpeed = 0;
		public bool UseObjectPooler = false;

		void Start ()
		{
				frontcheck = transform.Find ("frontCheck");
				anim = GetComponent<Animator> ();
				target = GameObject.FindGameObjectWithTag ("Player").transform; 
				bulletSpawner = transform.Find ("Mouth");
		}

		void FixedUpdate ()
		{

				PlayerFound = Physics2D.Linecast (transform.position, frontcheck.position, WhatToShoot); 
				if (PlayerFound) {
						if (RandomShootingDuration) {
								int i = Random.Range (0, 3);
								Invoke ("Fire", i);
						} else {

								Fire ();
						}
				}


	
		}

		void Fire ()
		{
				// ... set the animator Shoot trigger parameter and play the audioclip.
				//anim.SetTrigger("Shoot");
				//audio.Play();
		
				// If the player is facing right...
				shootingDirection = target.transform.position - transform.position;
				if (Time.time > lastHitTime + DelayBetweenShootings) {
						//Debug.Log (transform.name + " ....." + "facingRight = " + facingRight () + ".....VerticalShoot = " + VerticalShoot);
						anim.SetTrigger ("Fire");
						if (facingRight ()) {
								if (!VerticalShoot) {
										WantedRotation = 0;
										WantedXSpeed = speed;
										WantedYSpeed = 0f;
								} else {
										WantedRotation = 90;
										WantedXSpeed = 0f;
										WantedYSpeed = speed;
								}

						} else {
								if (!VerticalShoot) {
										WantedRotation = 0;
										WantedXSpeed = -speed;
										WantedYSpeed = 0f;

								} else {
										WantedRotation = 90;
										WantedXSpeed = 0f;
										WantedYSpeed = -speed;
								}
						}

						if (UseObjectPooler == true) {
								GameObject obj = ObjectPoolerScript.current.GetPooledObject ();
								obj.transform.position = bulletSpawner.position;
								obj.GetComponent<Fireball> ().UseObjectPooler = true; 
								obj.SetActive (true);
								obj.rigidbody2D.velocity = new Vector2 (WantedXSpeed, WantedYSpeed);
								obj.transform.Rotate (0, 0, WantedRotation); 
								if (!VerticalShoot && !facingRight ())
										FlipFireBall (obj.transform); 
				
						} else {
								Rigidbody2D bulletInstance = Instantiate (FireBall, bulletSpawner.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
								//Debug.Log (WantedXSpeed + "....." + WantedYSpeed);  
								bulletInstance.velocity = new Vector2 (WantedXSpeed, WantedYSpeed);
								if (!VerticalShoot && !facingRight ())
										FlipFireBall (bulletInstance.transform); 

						}
						lastHitTime = Time.time;
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

		void FlipFireBall (Transform fireball)
		{	
		
				// Multiply the player's x local scale by -1.
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				fireball.localScale = theScale;
		}
}
