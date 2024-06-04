using UnityEngine;
using System.Collections;

public class Monkey_Jump : MonoBehaviour {
	public float maxJumpSpeed = 60f; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			if (Mathf.Abs (rigidbody2D.velocity.y) > maxJumpSpeed && rigidbody2D.velocity.y > 0f)
				// ... set the player's velocity to the maxJumpSpeed in the y axis.
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, Mathf.Sign (rigidbody2D.velocity.y) * maxJumpSpeed);
			
		}
}
