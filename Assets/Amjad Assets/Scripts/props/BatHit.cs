using UnityEngine;
using System.Collections;

public class BatHit : MonoBehaviour
{
		private EnemyHealth Ehealth;
		private bool btnPressed = false;
		public Transform FireButton;
		public GameObject explosion;		// Prefab of explosion effect.
		// Use this for initialization
		
		void OnTriggerEnter2D (Collider2D col)
		{


				DoAction (col);

		}

		void OnTriggerStay2D (Collider2D col)
		{
				DoAction (col);
		
		}

		void DoAction (Collider2D col)
		{
				if (FireButton != null && !FireButton.GetComponent<TouchBtnScript> ().longPressDetected && FireButton.GetComponent<TouchBtnScript> ().isPressed) {
						//if (FireButton.GetComponent<TouchBtnScript> ().isPressed) {
						btnPressed = true;
				
						//} else {
						//		btnPressed = false;
						//}
				} else if (Input.GetButtonDown ("Punch")) {
						btnPressed = true;
				} else if(transform.parent.transform.GetComponent<Animator>().GetBool("AirAttack")){
						btnPressed = true;

		} else {
						btnPressed = false;
				}


				if (col.gameObject.tag == "Enemy" && btnPressed) {

						Ehealth = col.gameObject.GetComponent<EnemyHealth> ();
			if (Ehealth){
			Ehealth.Hurt (transform.parent, transform.parent.GetComponent<Animator>().GetBool("AirAttack")); 
			}
			if (transform.parent.GetComponent<Animator>().GetBool("AirAttack"))
			{
				transform.parent.GetComponent<PlayerHealth>().PushPlayerBack(col.transform);   
			}
			
				} else if (col.gameObject.name == "crate" && btnPressed) {
						//Debug.Log ("Destroying " + col.gameObject.name);
						OnExplode (col);
						Destroy (col.gameObject); 
				}
		 
		}

		void OnExplode (Collider2D col)
		{
				// Create a quaternion with a random rotation in the z-axis.
				Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f, 360f));
		
				// Instantiate the explosion where the rocket is with the random rotation.
				Instantiate (explosion, col.transform.position, randomRotation);
		}
}
