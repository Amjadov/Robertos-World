using UnityEngine;
using System.Collections;

public class JumpColliderScript : MonoBehaviour {
	public float JumpForce = 10f;
	public bool JumpToRight = true;
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "PlayerAssist") {
			if (col.transform.GetComponent<SmoothFollow2>().facingLeft != JumpToRight)
				col.rigidbody2D.AddForce(new Vector2(0,JumpForce));    
		}
		
	}
}
