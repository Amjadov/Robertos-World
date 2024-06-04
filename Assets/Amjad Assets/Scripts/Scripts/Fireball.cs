using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
		public GameObject explosion;		// Prefab of explosion effect.
		private Transform target;
		private Vector3 startpos;
		private Vector3 endpos;
		private float t = 0f;
		private float moveSpeed = 6f;
		private float dist;
		public bool DestroyObject = true;
		public float DestroyAfter = 2f;
		public bool UseObjectPooler = false;

		void Start ()
		{
				if (DestroyObject) { 
			Invoke ("isFinished", 2);
				}


		}

		void OnEnable ()
		{
				if (UseObjectPooler) 
						Invoke ("isFinished", 2);
		}

		void isFinished ()
		{
				if (UseObjectPooler) {
						gameObject.SetActive (false);
				} else {
						Destroy (gameObject, 0);
				}
		
		
		}

		void OnExplode ()
		{

				Quaternion randomRotation = Quaternion.Euler (0f, 0f, Random.Range (0f, 360f));


				Instantiate (explosion, transform.position, randomRotation);
		}

		void OnTriggerEnter2D (Collider2D col)
		{

				if (col.tag == "Player") {

						col.gameObject.GetComponent<PlayerHealth> ().TakeDamage (transform);

						Invoke ("isFinished", 2);

				}
	

		}
}
