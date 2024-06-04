using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour
{
		public float repeatDamagePeriod = 0.5f;
		private float lastHitTime;
			
		void OnTriggerEnter2D (Collider2D col)
		{

				if (col.transform.tag == "Player") {
						if (Time.time > lastHitTime + repeatDamagePeriod) {
								col.transform.GetComponent<PlayerHealth> ().TakeDamage (transform);  

								lastHitTime = Time.time; 

						}


				}
		}
}
