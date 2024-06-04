using UnityEngine;
using System.Collections;

public class Kong_Runaway : MonoBehaviour {
	private Animator anim;
	private bool StartRun = false;
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	// Use this for initialization
	void Start () {
		anim = transform.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (StartRun == true) {
			transform.Translate (new Vector3 (-1, 0, 0) * moveSpeed); 
				}
	}
	public void Runaway(){
		if (!facingRight ())
						Flip ();

		anim.SetTrigger ("Run");
		StartRun = true;

	}
	bool facingRight ()
	{
		if (transform.localScale.x < 0) {
			return true;
		} else {
			return false;
		}
	}
	
	public void Flip ()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
