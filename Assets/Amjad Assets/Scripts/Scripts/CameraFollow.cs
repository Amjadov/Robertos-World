using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
		public Transform target;
		public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
		public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
		public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
		public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
		public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
		public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
		public float TargetAheadX = 0f;
		public float TargetAheadY = 0f;
		private PlayerControl  targetCtrlScrpt;
		private PlayerControl_InfiniteRunner  targetCtrlScrpt2;
		public bool IsInfinitRunner = false;
		private Transform player;		// Reference to the player's transform.
		private Transform MaxB;
		private Transform MinB;
		private bool FacingRight = true;

		void Awake ()
		{
				// Setting up the reference.
				player = GameObject.FindGameObjectWithTag ("Player").transform;
				if (IsInfinitRunner) {
						targetCtrlScrpt2 = player.GetComponent<PlayerControl_InfiniteRunner> ();
				} else {
						targetCtrlScrpt = player.GetComponent<PlayerControl> ();
				}

			

				MaxB = GameObject.Find ("SceneMaxBoundary").transform;
				MinB = GameObject.Find ("SceneMinBoundary").transform;
		}

		void Start ()
		{
				if (target != player)
						Invoke ("changeTargetToPlayer", 7);
		}

		bool CheckXMargin ()
		{

				// Returns true if the distance between the camera and the target in the x axis is greater than the x margin.
				if (FacingRight) {
						return Mathf.Abs (transform.position.x - (target.position.x + TargetAheadX)) > xMargin;
				} else {
						return Mathf.Abs (transform.position.x - (target.position.x - TargetAheadX)) > xMargin;
				}




		}

		bool CheckYMargin ()
		{
				// Returns true if the distance between the camera and the target in the y axis is greater than the y margin.
				return Mathf.Abs (transform.position.y - target.position.y) > yMargin;
		}

		void Update ()
		{
				if (IsInfinitRunner) {
						FacingRight = targetCtrlScrpt2.facingRight;
				} else {
						FacingRight = targetCtrlScrpt.facingRight;
				}
				Tracktarget ();
				
		}
		
		void Tracktarget ()
		{
				// By default the target x and y coordinates of the camera are it's current x and y coordinates.
				float targetX = transform.position.x;
				float targetY = transform.position.y;
				// If the target has moved beyond the x margin...
				if (CheckXMargin ()) {
						// ... the target x coordinate should be a Lerp between the camera's current x position and the target's current x position.
						if (FacingRight) {
								targetX = Mathf.Lerp (transform.position.x, (target.position.x + (TargetAheadX)), xSmooth * Time.deltaTime);
						} else {
								targetX = Mathf.Lerp (transform.position.x, (target.position.x - TargetAheadX), xSmooth * Time.deltaTime);
						}
				}
				// If the target has moved beyond the y margin...
				if (CheckYMargin ()) {
						// ... the target y coordinate should be a Lerp between the camera's current y position and the target's current y position.
						targetY = Mathf.Lerp (transform.position.y, target.position.y, ySmooth * Time.deltaTime);
				}
				// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
				targetX = Mathf.Clamp (targetX, MinB.position.x, MaxB.position.x);
				targetY = Mathf.Clamp (targetY, MinB.position.y, MaxB.position.y);

				// Set the camera's position to the target position with the same z component.
				transform.position = new Vector3 (targetX, targetY, transform.position.z);

		}





		void changeTargetToPlayer ()
		{
	
				target = player;

		}

}
