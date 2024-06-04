using UnityEngine;
using System.Collections;

public class RunIntoBull : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector3 (-5, 0, 0); 
	}
}
