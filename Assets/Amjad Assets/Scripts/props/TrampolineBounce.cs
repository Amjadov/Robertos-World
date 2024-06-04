using UnityEngine;
using System.Collections;

public class TrampolineBounce : MonoBehaviour {
	public float BounceForce = 1000;
	private Transform player;
	private Animator animl;
	public float BounceSoundRange = 20f;
	void Awake()
	{
		animl = transform.parent.GetComponent<Animator> (); 
		player = GameObject.FindGameObjectWithTag ("Player").transform;   
		//SpeechObject.gameObject.SetActive (false); 
		}

//	void OnCollisionEnter2D (Collision2D col)
//	{
//		if (col.gameObject.tag == "Player") {
//			animl.SetTrigger("Bounce"); 
//			col.transform.rigidbody2D.AddForce(new Vector2(col.transform.rigidbody2D.velocity.x,BounceForce));  
//				}
//
//	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.name == "Monkey") {
			if (Vector3.Distance(player.position, transform.position)< BounceSoundRange) {
				AudioSource.PlayClipAtPoint(audio.clip, transform.position);
			}
			animl.SetTrigger("Bounce"); 
			 
			//audio.Play ();				
			col.transform.rigidbody2D.AddForce(new Vector2(col.transform.rigidbody2D.velocity.x,BounceForce));  
		}

		
	}
}
