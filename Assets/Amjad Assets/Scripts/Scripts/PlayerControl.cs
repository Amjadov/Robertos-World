using UnityEngine;
using System.Collections;
public class PlayerControl : MonoBehaviour
{
		[HideInInspector]
		public bool
				facingRight = true;			// For determining which way the player is currently facing.
		[HideInInspector]
		public bool
				jump = false;				// Condition for whether the player should jump.
		public bool duck = false;				// Condition for whether the player should duck.
		public Transform JumpButton;
		public Transform FireButton;
		public float moveForce = 365f;			// Amount of force added to move the player left and right.
		public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
		public float maxJumpSpeed = 5f;
		public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
		public AudioClip[] PunchClips;			// Array of clips for when the player jumps.
		public float jumpForce = 1000f;			// Amount of force added when the player jumps.
		public float SecondJumpForce = 700f;
		public AudioClip[] taunts;				// Array of clips for when the player taunts.
		public float tauntProbability = 50f;	// Chance of a taunt happening.
		public float tauntDelay = 1f;			// Delay for when the taunt should happen.
		public bool FlipAtAwake = false;
		public LayerMask WhatIsGround;
		public LayerMask WhatIsWall;
		private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
		private Transform groundCheck;			// A position marking where to check if the player is grounded.
		private Transform wallCheck1;			// A position marking where to check if the player is grounded.	
		private Transform wallCheck2;			// A position marking where to check if the player is grounded.	
		private Transform wallCheck3;			// A position marking where to check if the player is grounded.	
		public bool grounded = false;			// Whether or not the player is grounded.
		//public bool walled = false;			// Whether or not the player stuck facing a wall "good for stoping acceleration in air"
		private Animator anim;					// Reference to the player's animator component.
		public Transform Bat;
		private Animator BatAnim;
		private bool LastPressed = false;          //check the last status of the button press
		private bool doJump = false;
		private bool SecondJump = true;
		public Transform JoystickObject;
		private Instantiate_V_Joystick Joystick;
		private float TopV = 0f;
		private float BV = 0f;
		public bool UseParallaxScrolling = false;
		public Transform  BG1;
		public Transform BG2;
		public Transform BG3;
		public bool UseLadder = false;
		public bool OnLadder = false;
		private TouchBtnScript JumpbtnScrpt;
		private TouchBtnScript FirebtnScrpt;
		private float PrevCamPos = 0;


//		void OnGUI()
//		{
//			GUI.Label(new Rect(10, 200, 300, 20), "Yaxis="+Joystick.ButtonYAxis);
//			//GUI.Label(new Rect(10, 40, 300, 20), "IsPressed="+IsPressed);
//			//GUI.Label(new Rect(10, 10, 300, 20), "Mabtn.Pressed="+abtn.Pressed);
//			}

		void Awake ()
		{

				// Setting up references.
				Joystick = JoystickObject.GetComponent<Instantiate_V_Joystick> ();
				groundCheck = transform.Find ("groundCheck");
//				wallCheck1 = transform.Find ("wallCheck1");
//				wallCheck2 = transform.Find ("wallCheck2");
//				wallCheck3 = transform.Find ("wallCheck3");
				JumpbtnScrpt = JumpButton.GetComponent<TouchBtnScript> ();
				FirebtnScrpt = FireButton.GetComponent<TouchBtnScript> ();
				anim = GetComponent<Animator> ();
				BatAnim = Bat.GetComponent<Animator> (); 
				if (FlipAtAwake) {
						Flip ();
						facingRight = true;
				}
		     
		}
	void CheckAirAttack(){
				if (grounded) {
						anim.SetBool ("AirAttack", false);
			
				}
		}
	void CheckJump(){
		if (JumpButton != null && !JumpbtnScrpt.longPressDetected) {
			if (JumpbtnScrpt.isPressed && grounded) {
				
				jump = true;
				SecondJump = true;
				
			} else if (!grounded && SecondJump && JumpbtnScrpt.isPressed) {
				
				jump = true;
				SecondJump = false;
				
				
			}
		}

		}
	void CheckDuck()
	{

		if ((Joystick.ButtonYAxis < -0.7 && grounded) || (Input.GetAxis ("Vertical") < 0 && grounded) || (Input.GetButtonDown ("Duck") && grounded)) {
			if (duck== false)			
				duck = true;
		} else {duck = false;}

		// If the player should duck...
		if (duck) {
			anim.SetBool ("Duck", true);
			
		} else {
			anim.SetBool ("Duck", false);
		}
	}
		
		void Update ()
		{
		if (Time.timeScale == 0)
						return;


				// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
//		Debug.DrawLine(transform.position, wallCheck.position, Color.red);
				grounded = Physics2D.Linecast (transform.position, groundCheck.position, WhatIsGround);  
//				walled = Physics2D.Linecast (transform.position, wallCheck1.position, WhatIsWall);
//				if (!walled)
//						walled = Physics2D.Linecast (transform.position, wallCheck2.position, WhatIsWall);
//
//				if (!walled)
//						walled = Physics2D.Linecast (transform.position, wallCheck3.position, WhatIsWall);


				
				anim.SetBool ("Grounded", grounded);  
				anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);  

		CheckAirAttack (); 
				
		 
				if (rigidbody2D.velocity.y > TopV)
						TopV = rigidbody2D.velocity.y;

				if (rigidbody2D.velocity.y < BV)
						BV = rigidbody2D.velocity.y;


				if (UseLadder && OnLadder) {
						rigidbody2D.gravityScale = 0f; 
						anim.SetBool ("OnLadder", true); 
						if (JumpButton != null && JumpbtnScrpt.longPressDetected) {
							
					
								transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
								
						}
						if (Joystick.ButtonYAxis > 0.7) {
								transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
						} else if (Joystick.ButtonYAxis < -0.7 && !grounded) {
								transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);
						}


				} else {
						rigidbody2D.gravityScale = 1f; 
						anim.SetBool ("OnLadder", false); 
			CheckJump ();
				}


				//Below is Jump Button Code (like Ctrl)
				// If the jump button is pressed and the player is grounded then the player should jump.
		float v = Input.GetAxis ("Vertical");
				if (UseLadder && OnLadder) {
						anim.SetBool ("OnLadder", true); 
						rigidbody2D.gravityScale = 0f; 
						if (v > 0) {
								transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
						} else if (v < 0 && !grounded) {
								transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);


						} else if (v == 0) {
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);  
						}
				} else {
						anim.SetBool ("OnLadder", false); 
						rigidbody2D.gravityScale = 1f; 
						if (Input.GetButtonDown ("Jump") && grounded) {
								jump = true;
								SecondJump = true;
						}
						

						if (Input.GetButtonDown ("Jump") && !grounded && SecondJump == true) {
								SecondJump = false;
								jump = true;
						}
				}

		CheckDuck ();


//				if (FireButton != null && !FirebtnScrpt.longPressDetected) {
//						if (FirebtnScrpt.isPressed) {
//								if (grounded) 
//										anim.SetTrigger ("Punch");
//								else
//										anim.SetBool ("AirAttack", true);
//
//								anim.SetTrigger ("StartAirAttack");
//
//
//								return;
//				
//
//						}
//				}
		
//				if (Input.GetButtonDown ("Punch")) {
//						if (grounded) { 
//								anim.SetTrigger ("Punch");
//						} else {
//								anim.SetBool ("AirAttack", true);
//								anim.SetTrigger ("StartAirAttack");
//
//						}
//				}


				// Cache the horizontal input.
				float h = Input.GetAxis ("Horizontal");
				if (h == 0) {
						h = Joystick.ButtonXAxis;
				}
				if (Joystick.ButtonXAxis > 0.7 | Joystick.ButtonXAxis < -0.7) {
						h = Joystick.ButtonXAxis;
				}

				// the if condition will prevent the player from getting stuck on walls and objects like spider man
				//if (grounded | rigidbody2D.velocity.y > 0f | rigidbody2D.velocity.y != 0f) {
//				if (!walled) {
//						// The Speed animator parameter is set to the absolute value of the horizontal input.
//						anim.SetFloat ("Speed", Mathf.Abs (h));
//			
//						// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
//			if (h * rigidbody2D.velocity.x < maxSpeed && Joystick.ButtonYAxis > -0.7f)
//				// ... add a force to the player.
//								rigidbody2D.AddForce (Vector2.right * h * moveForce);
//						// If the player's horizontal velocity is greater than the maxSpeed...
//
//				} else {
//						anim.SetFloat ("Speed", 0f);
//				}


			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat ("Speed", Mathf.Abs (h));
			
			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if (h * rigidbody2D.velocity.x < maxSpeed && Joystick.ButtonYAxis > -0.7f)
				// ... add a force to the player.
				rigidbody2D.AddForce (Vector2.right * h * moveForce);
			// If the player's horizontal velocity is greater than the maxSpeed...
			


				if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
					// ... set the player's velocity to the maxSpeed in the x axis.
						rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);


				if (h == 0f) {
						rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
				}
				if (UseParallaxScrolling == true && PrevCamPos != Camera.main.transform.position.x) {
						if (BG1 != null)
								BG1.GetComponent<ScrollScript_Auto> ().Scroll (rigidbody2D.velocity.x);
						if (BG2 != null)
								BG2.GetComponent<ScrollScript_Auto> ().Scroll (rigidbody2D.velocity.x);
						if (BG3 != null)
								BG3.GetComponent<ScrollScript_Auto> ().Scroll (rigidbody2D.velocity.x);
				}

			
				if (Mathf.Abs (rigidbody2D.velocity.y) > maxJumpSpeed && rigidbody2D.velocity.y > 0f)
			// ... set the player's velocity to the maxJumpSpeed in the y axis.
						rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, Mathf.Sign (rigidbody2D.velocity.y) * maxJumpSpeed);
				// If the input is moving the player right and the player is facing left...
		
				if (h > 0 && !facingRight)
						Flip ();
				else if (h < 0 && facingRight)
						Flip ();
		
				if (jump) {
						int i = Random.Range (0, jumpClips.Length);
						AudioSource.PlayClipAtPoint (jumpClips [i], transform.position);
						anim.SetTrigger ("Jump"); 
						if (SecondJump == false) {
								rigidbody2D.AddForce (new Vector2 (0f, SecondJumpForce));
						} else {
								rigidbody2D.AddForce (new Vector2 (0f, jumpForce));
						}
						jump = false;
				}
				
		
		
		
		
				PrevCamPos = Camera.main.transform.position.x;

		}

		//This will fire by an event in the animation "Hit"
		public void PlayAudioPunch ()
		{

				//int i = Random.Range (0, PunchClips.Length);
				AudioSource.PlayClipAtPoint (PunchClips [1], transform.position);
		}

		void Flip ()
		{
				// Switch the way the player is labelled as facing.
				facingRight = !facingRight;

				// Multiply the player's x local scale by -1.
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
		}

		public IEnumerator Taunt ()
		{
				// Check the random chance of taunting.
				float tauntChance = Random.Range (0f, 100f);
				if (tauntChance > tauntProbability) {
						// Wait for tauntDelay number of seconds.
						yield return new WaitForSeconds (tauntDelay);

						// If there is no clip currently playing.
						if (!audio.isPlaying) {
								// Choose a random, but different taunt.
								tauntIndex = TauntRandom ();

								// Play the new taunt.
								audio.clip = taunts [tauntIndex];
								audio.Play ();
						}
				}
		}

		int TauntRandom ()
		{
				// Choose a random index of the taunts array.
				int i = Random.Range (0, taunts.Length);

				// If it's the same as the previous taunt...
				if (i == tauntIndex)
			// ... try another random taunt.
						return TauntRandom ();
				else
			// Otherwise return this index.
						return i;
		}
}
