using UnityEngine;
using System.Collections;

public class SpikeMotor : MonoBehaviour
{
		private bool StartPull = true;
		private bool isPulling = true;
		private Rigidbody2D spike;
		private Transform TopPos;
		private Transform BottomPos;
		public float DelayBeforeStart = 0;

		void Start ()
		{
				spike = rigidbody2D; 
				TopPos = transform.parent.Find ("TopPos");
				BottomPos = transform.parent.Find ("BottomPos");
		InvokeRepeating ("MoveSpike",DelayBeforeStart, 0.1f);
	 	   
		}

		void MoveSpike ()
		{

				if (transform.position.y < TopPos.position.y && isPulling == true) {
						StartPull = true;	
				} else if (transform.position.y > TopPos.position.y && isPulling == true) {
						isPulling = false;
						StartPull = false;
				} else if (transform.position.y < BottomPos.position.y && isPulling == false) {
						isPulling = true;
						StartPull = true;
						;	
				}
		

				
				if (StartPull) {
						//spike.AddForce (new Vector2 (0f, PullForce));
						transform.position = new Vector3 (transform.position.x, (transform.position.y + 0.5f)); 
				} else {
						transform.position = new Vector3 (transform.position.x, (transform.position.y - 1f));  
				}

		}

}
