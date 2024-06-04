using UnityEngine;
using System.Collections;

public class ScrollScript_Follow : MonoBehaviour {
	public float speed = 0;
	// Use this for initialization
	public Transform CameraObject;
	private float CameraPosX;
	public int BGoffset;
	public bool FollowCamera;
	private float NewXPos;

	void start(){
		CameraPosX = Camera.main.transform.position.x;   
		}
	
	// Update is called once per frame
	void Update () {
		NewXPos = (Camera.main.transform.position.x - CameraPosX) / BGoffset;
		if (FollowCamera) {
			transform.position = new Vector3(NewXPos, transform.position.y, transform.position.z);
				} else {
		//renderer.material.mainTextureOffset = new Vector2 (Time.time * speed, 0f); 
		//transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
}
