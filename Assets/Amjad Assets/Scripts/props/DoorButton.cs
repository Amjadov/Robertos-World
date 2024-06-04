using UnityEngine;
using System.Collections;

public class DoorButton : MonoBehaviour {
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private SpriteRenderer KeyRen;
	public Sprite GreenButton;		
	public Sprite GreenLock;	
	public Transform DoorKey;
	public Transform door;
	// Use this for initialization
	void Start () {
	
		ren = transform.GetComponent<SpriteRenderer>();
		KeyRen = DoorKey.GetComponent<SpriteRenderer>();
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
						ren.sprite = GreenButton;
						KeyRen.sprite = GreenLock;
			door.GetComponent<DoorScript>().IsOpen = true;
				}

		}

}
