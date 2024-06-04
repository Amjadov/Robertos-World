using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

	private bool IsPressed = false;          //check the last status of the button press
	private bool StartProcess = false;
	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	public Transform FireButton;
	private bool LastPressed = false;          //check the last status of the button press
	private bool doFire = false;
	private Transform BulletSpawn;
	private TouchBtnScript FireBtnScript;

	void Awake()
	{
		// Setting up the references.
		anim = transform.GetComponent<Animator>();
		playerCtrl = transform.GetComponent<PlayerControl>();
		BulletSpawn = transform.Find ("Gun");  
		FireBtnScript = FireButton.GetComponent<TouchBtnScript> ();
	}

//	void OnGUI()
//	{
//		//GUI.Label(new Rect(10, 10, 300, 20), "Mabtn.Pressed="+abtn.Pressed);
//		//GUI.Label(new Rect(10, 40, 300, 20), "IsPressed="+IsPressed);
//		//GUI.Label(new Rect(10, 10, 300, 20), "Mabtn.Pressed="+abtn.Pressed);
//		}
	public void Fire()
	{
		// ... set the animator Shoot trigger parameter and play the audioclip.

		BulletSpawn.audio.Play();
		
		// If the player is facing right...
		if(playerCtrl.facingRight)
		{
			// ... instantiate the rocket facing right and set it's velocity to the right. 
			Rigidbody2D bulletInstance = Instantiate(rocket, BulletSpawn.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
		}
		else
		{
			// Otherwise instantiate the rocket facing left and set it's velocity to the left.
			Rigidbody2D bulletInstance = Instantiate(rocket, BulletSpawn.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-speed, 0);
		}
		//anim.SetBool("Shoot", false);

		}

	void Update ()
	{
		if (FireButton != null && FireBtnScript.isPressed && LastPressed == false) {
			LastPressed = true;
			//anim.SetBool("Shoot", true);
			anim.SetTrigger("Punch");
			Fire();

		} else if(FireButton != null && FireBtnScript.isPressed && LastPressed == true)
		{
			//StartProcess = false;
		} else {
			LastPressed = false;
		}

		if (Input.GetButtonDown("Punch")) {anim.SetTrigger("Punch");Fire();}
	
	}
}
