using UnityEngine;
using System.Collections;

public class SmoothFollow2 : MonoBehaviour
{
		public Transform target;
		public float Distance = 3f;
		public float Speed = 0.3f;
		public float MaxJump = 20f;
		public float AttackSpeed = 1f;
		public Transform AttackButton;
		private PlayerControl  targetCtrlScrpt;
		private PlayerControl_InfiniteRunner  targetCtrlScrpt2;
		private Animator anim;
		public bool facingLeft = true;
		private bool moving = false;
		private Vector3 WantedPos;
		private float ActualDistance;
		public bool TrackMode = true;
		public bool ChargeMode = false;
		public bool DemandedAttackMode = false;
		private Transform frontCheck;
		public LayerMask WhatIsEnemy;
		public bool EnemyFound = false;
		private Transform player;
		private EnemyHealth Ehealth;
		private TouchBtnScript AttackbtnScrpt;
		private Vector3 OriginalPosition;
		public float DemandedAttackSeconds = 2f;
		private float AttackTime = 0;
		void Awake ()
		{
				// Setting up the reference.
				anim = GetComponent<Animator> ();
				frontCheck = transform.Find ("frontCheck");
				player = GameObject.FindGameObjectWithTag ("Player").transform; 
				AttackbtnScrpt = AttackButton.GetComponent<TouchBtnScript> ();

		}

		void Start ()
		{
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Player"), true); 

		}

//		void OnGUI ()
//		{
//				GUI.Label (new Rect (10, 200, 300, 20), "Kong=" + facingLeft); //transform.position.x + "---" + transform.position.y);
//				GUI.Label (new Rect (10, 40, 300, 20), "Player=" + player.GetComponent<PlayerControl> ().facingRight);//target.position.x + "---" + target.position.y);
////
//		}

		void Update ()
		{

				EnemyFound = Physics2D.Linecast (transform.position, frontCheck.position, WhatIsEnemy);   
				if (EnemyFound && !ChargeMode && !DemandedAttackMode) {
						TrackMode = false;
						ChargeMode = true;
						anim.SetTrigger ("Charge"); 
						//Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"),true); 
  

				} else if (!EnemyFound && !TrackMode && !DemandedAttackMode) {
						TrackMode = true;
						ChargeMode = false;
						
						//Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"),false); 

				} 

				//if (((AttackButton && AttackbtnScrpt.isPressed) || Input.GetButtonDown ("Fire2")) && !DemandedAttackMode) {
			if ((AttackButton && AttackbtnScrpt.isPressed) && !DemandedAttackMode) {
						DemandedAttackMode = true;
						Camera.main.GetComponent<CameraFollow>().target = transform;  
						TrackMode = false;
						ChargeMode = false;
						anim.SetTrigger ("Charge"); 
						OriginalPosition = transform.position;
						if (player.GetComponent<PlayerControl> ().facingRight && facingLeft) {
								Flip ();
						} else if (!player.GetComponent<PlayerControl> ().facingRight && !facingLeft) {
								Flip ();		
						}
				}
				

				//FacingRight = targetCtrlScrpt.facingRight;
				if (TrackMode)
						Tracktarget ();
				if (ChargeMode)
						Attack ();
				if (DemandedAttackMode)
						DemandedAttack ();

				if (rigidbody2D.velocity.y > MaxJump)
						rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, MaxJump);

				if (transform.position.y < 13f)
						transform.position = new Vector3 (transform.position.x, 13f, transform.position.z);

				if (target.position.x > transform.position.x) {
						ActualDistance = Mathf.Abs (Mathf.Round (target.position.x - Distance) - Mathf.Round (transform.position.x));
				} else if (target.position.x < transform.position.x) {
						ActualDistance = Mathf.Abs (Mathf.Round (transform.position.x) - Mathf.Round (target.position.x + Distance));
				}
		
		}
	
		void Tracktarget ()
		{

				if (target.position.x > transform.position.x) {
						WantedPos = new Vector3 (target.position.x - Distance, target.position.y, transform.position.z);
						ActualDistance = Mathf.Abs (Mathf.Round (target.position.x - Distance) - Mathf.Round (transform.position.x));
				} else if (target.position.x < transform.position.x) {
						WantedPos = new Vector3 (target.position.x + Distance, target.position.y, transform.position.z);
						ActualDistance = Mathf.Abs (Mathf.Round (transform.position.x) - Mathf.Round (target.position.x + Distance));
				}

				if (Mathf.Round (transform.position.x) > Mathf.Round (target.position.x + Distance) || Mathf.Round (transform.position.x) < Mathf.Round (target.position.x - Distance)) {				
					
						transform.position = Vector3.Lerp (transform.position, WantedPos, Time.deltaTime * Speed);   
						if (!moving) {
								moving = true;
								anim.SetTrigger ("Run");
						} 
						
				} else {

						if (moving) {
								//Debug.Log ("Idling...");
								moving = false;
								anim.SetTrigger ("Idle"); 
						} 

				}
				if (transform.position.x <= target.transform.position.x) {
						if (facingLeft) {
								Flip ();
						}
				} else {
						if (!facingLeft) {
								Flip ();
						}
				}
				anim.SetFloat ("ActualDistance", ActualDistance);
				
		}

		void Attack ()
		{


			
//			int BullSoundChance = Random.Range (0, 3);
//			if (BullSoundChance == 1) {
//				AudioSource.PlayClipAtPoint (BullCharge, transform.position, 0.3f);
//			} 
//			audio.Play ();
//			ChaseRunningNow = true;
	
				if (facingLeft) {
						transform.Translate (new Vector3 (-1, 0, 0) * AttackSpeed);
				} else {
			
						transform.Translate (new Vector3 (1, 0, 0) * AttackSpeed);
			
				} 
		}

		void DemandedAttack ()
		{

				

				//facingRight = !player.GetComponent<PlayerControl> ().facingRight;
		AttackTime +=Time.deltaTime;

				if (facingLeft) {
						transform.Translate (new Vector3 (-1, 0, 0) * AttackSpeed);
				} else {
			
						transform.Translate (new Vector3 (1, 0, 0) * AttackSpeed);
			
				} 
				//if (Vector3.Distance (transform.position, OriginalPosition) > 25f) {
		if (AttackTime >= DemandedAttackSeconds){ 
						DemandedAttackMode = false;
						Camera.main.GetComponent<CameraFollow>().target = player;  
						TrackMode = true;
			AttackTime= 0;
			anim.SetTrigger("Run"); 
				}
		}

		void Flip ()
		{

				// Switch the way the player is labelled as facing.
				facingLeft = !facingLeft;
			
				// Multiply the player's x local scale by -1.
				Vector3 theScale;
				theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;

		}

		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Enemy") {

						Ehealth = col.gameObject.GetComponent<EnemyHealth> ();
						if (Ehealth) {
								Ehealth.Hurt (transform, false); 
						}  
//		} else if (col.gameObject.tag != "Enemy") {
//						DemandedAttackMode = false;
//						TrackMode = true; 
			}

		}
	

	
}
