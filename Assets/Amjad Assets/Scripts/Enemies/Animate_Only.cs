using UnityEngine;
using System.Collections;

public class Animate_Only : MonoBehaviour {
	private Animator anim;
	private float CurrentSpeed = 0;
	private bool Moving = false;
	// Use this for initialization
	void Start () {
		anim = transform.GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		CurrentSpeed = rigidbody2D.velocity.x;
		anim.SetFloat ("Speed",Mathf.Abs(CurrentSpeed)); 
		if (CurrentSpeed == 0f && Moving) {
			Moving = false;
				} else if(CurrentSpeed != 0f && !Moving){
			Moving = true;
			anim.SetTrigger("Run"); 
		}

	}

	void OnDisable ()
	{
		Moving = false;
	}

}
