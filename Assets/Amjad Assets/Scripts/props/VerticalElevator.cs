using UnityEngine;
using System.Collections;

public class VerticalElevator : MonoBehaviour
{
		public float moveSpeed = 0.4f;
		public float CurrentMove = 0;
		public bool MoveUp = true;
		private float Counter = 0f;
		private float prevx = 0;
		private bool MoveGuest = false;
		public Transform ElevatorTop;
		public Transform ElevatorBottom;
		float duration = 0.9f; // duration of movement in seconds
		bool  moving = false; // flag to indicate it's moving
	
		void FixedUpdate ()
		{
				MoveGrass ();

		}

		void MoveGrass ()
		{
				//prevx = transform.position.x;
				if (MoveUp) {
						Counter += moveSpeed;

				} else {
						Counter -= moveSpeed;
				}



				
				if (MoveUp) {
						transform.Translate (new Vector3 (0, 1, 0) * moveSpeed);
						if (transform.position.y > ElevatorTop.position.y) {
								transform.position = ElevatorBottom.position;
				
						}
				} else {
						transform.Translate (new Vector3 (0, -1, 0) * moveSpeed);
						if (transform.position.y < ElevatorBottom.position.y) {
								transform.position = ElevatorTop.position;
				
						}
				}

				MoveGuest = true;


		}



}
