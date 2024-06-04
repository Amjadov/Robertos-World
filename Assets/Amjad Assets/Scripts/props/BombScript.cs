using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {
	public float Speed = 15f;
	public bool MoveLeft = true;
	public GameObject explosion;		// Prefab of explosion effect.
	public bool UseObjectPooler = false;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Bombs"), false); 
		if (MoveLeft) {
			rigidbody2D.AddTorque(Speed * 2); 
		} else {
			rigidbody2D.AddTorque(-Speed * 2);
		}

	}
	public void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	// Update is called once per frame
	void Update () {
		//transform.rigidbody2D.velocity = new Vector2 (Speed, rigidbody2D.velocity.y);
		if (MoveLeft) {
						rigidbody2D.AddForce (Vector2.right * -Speed);
						 
				} else {
						rigidbody2D.AddForce (Vector2.right * Speed);
						
				}

		if (Mathf.Abs (rigidbody2D.velocity.x) > Speed)
			transform.rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * Speed, rigidbody2D.velocity.y); 
		 
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{

//		 if (col.gameObject.tag == "Player") {

//			col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (transform);
//			col.gameObject.GetComponent<PlayerHealth> ().KillPlayer ();
//			OnExplode ();
//			DestroyObj();
//			OnExplode ();
//			DestroyObj();
						
				//} else 
	if (col.gameObject.tag == "Finish") {
			OnExplode ();
			DestroyObj();
				}
		
	}
	public void DestroyObj()
	{
		if (UseObjectPooler) {
						gameObject.SetActive (false);  

				} else {
			Destroy (gameObject);
		}


	}
}
