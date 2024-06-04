using UnityEngine;
using System.Collections;

public class ScrollingGrassMotor : MonoBehaviour {
	public float MoveDistance = 10f;
	public float MoveForce = 0.4f;
	public float CurrentMove = 0;
	public bool StartFromRight = false; // this is to make the grass move to left at start instead of Right
	private bool MoveRight = true;
	private float Counter = 0f;
	private float prevx = 0;
	private bool MoveGuest = false;


	float duration = 0.9f; // duration of movement in seconds
	bool  moving = false; // flag to indicate it's moving


	void Start () { 
		if (StartFromRight) {

			MoveRight = false;
				}
		//InvokeRepeating("MoveGrass", 0, 0.1f);
	 	   
		}
//	void OnGUI()
//	{
//		GUI.Label(new Rect(10,10,500,50),"transform.position.y: "+transform.position.y);
//		GUI.Label(new Rect(10,40,500,50),"9 StartPull: "+StartPull);
//		GUI.Label(new Rect(10,70,540,50),"MaxBottom: "+isPulling);
//	}
	void FixedUpdate()
	{
		MoveGrass ();

		}
	void MoveGrass()
	{
		prevx = transform.position.x;
		if (MoveRight) {
			Counter += MoveForce;

				}else {
			Counter -= MoveForce;
			}



				
		if (MoveRight) {
						//spike.AddForce (new Vector2 (0f, PullForce));
			transform.position = new Vector3 ((transform.position.x + MoveForce),transform.position.y); 
				} else {
			transform.position = new Vector3 ((transform.position.x - MoveForce),transform.position.y); 
				}
		MoveGuest = true;
		if (Counter >= MoveDistance && MoveRight) {
			MoveRight = false;
			Counter = 0;
		} else if (Counter <= MoveDistance * -1f && !MoveRight) {
			MoveRight = true;
			Counter = 0;
			
			
		}
	}


//	void OnTriggerStay2D(Collider2D col)
//	{
////		Debug.Log (col.gameObject.tag + "___Stayed"); 
////		if (MoveRight && MoveGuest == true) {
////
////						col.transform.position = new Vector3 ((col.transform.position.x + MoveForce), col.transform.position.y); 
////			MoveGuest = false;
////		} else if (!MoveRight && MoveGuest == true){
////						col.transform.position = new Vector3 ((col.transform.position.x - MoveForce), col.transform.position.y); 
////			MoveGuest = false;
////				}
//
//		}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (MoveRight && MoveGuest == true) {
			
			col.transform.position = new Vector3 ((col.transform.position.x + MoveForce), col.transform.position.y); 
			MoveGuest = false;
		} else if (!MoveRight && MoveGuest == true){
			col.transform.position = new Vector3 ((col.transform.position.x - MoveForce), col.transform.position.y); 
			MoveGuest = false;
		}
		}
//	void  MovetheGrass (float dx){
//		if (moving) return; // if already moving return 
//		moving = true; // else set flag to signal it's moving now
//		Vector3 curPos= transform.position;
//		Vector3 newPos= new Vector3(curPos.x + dx, curPos.y,curPos.z); // calculate new position
//		for (float t = 0; t < 1;){
//			t += Time.deltaTime / duration; // advance t towards 1 a little step
//			transform.position = Vector3.Lerp(curPos, newPos, t); // move
//			//yield return 0; // suspend execution till next frame
//		}
//		moving = false; // movement finished
//		MoveRight = !MoveRight;
//	}

}
