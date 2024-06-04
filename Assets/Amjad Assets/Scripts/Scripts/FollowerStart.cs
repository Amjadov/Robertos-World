using UnityEngine;
using System.Collections;

public class FollowerStart : MonoBehaviour {
	public Transform FollowerObject;
	public Transform FollowButton;
	void Awake()
	{

		}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			FollowerObject.GetComponent<SmoothFollow2>().enabled = true;   
			FollowButton.gameObject.SetActive(true);   
						Destroy (gameObject);  
				}

	}
}
