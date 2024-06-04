// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Instantiate_V_Joystick_C : MonoBehaviour {
	
	
	// Objects to drag in
	public Transform character;
	public GameObject cursorPrefab;
	public GameObject joystickPrefab;
	
	// Cursor settings
	public float cursorPlaneHeight = 0;
	public float cursorFacingCamera = 0;
	public float cursorSmallerWithDistance = 0;
	public float cursorSmallerWhenClose = 1;
	
	public float SideMovement = 0;
	// Private memeber data
	
	private Transform cursorObject;
	private Joystick_Script_C joystickLeft;
	//private Joystick_Script_C joystickRight;
	
	// Prepare a cursor point varibale. This is the mouse position on PC and controlled by the thumbstick on mobiles.
	private Vector2 cursorScreenPosition;
	
	private Plane playerMovementPlane;
	
	//private GameObject joystickRightGO;
	
	
	void  Awake (){		
		
		// Ensure we have character set
		// Default to using the transform this component is on
		if (!character)
			character = transform;
		//#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		if (joystickPrefab) {
			// Create left joystick
			GameObject joystickLeftGO = Instantiate(joystickPrefab) as GameObject;
			joystickLeftGO.name = "Joystick Left";
			joystickLeft = joystickLeftGO.GetComponent<Joystick_Script_C> ();
			
			// Create right joystick
			//We dont need a second joystick right now
//			joystickRightGO = Instantiate(joystickPrefab) as GameObject;
//			joystickRightGO.name = "Joystick Right";
//			joystickRight = joystickRightGO.GetComponent<Joystick_Script_C> ();			
		}
		//#elif !UNITY_FLASH
		//if (cursorPrefab) {
			cursorObject = (Instantiate (cursorPrefab) as GameObject).transform;
		//}
		//#endif
		
		
		
	}
	
	void  Start (){
		#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		// Move to right side of screen
//		GUITexture guiTex = joystickRightGO.GetComponent<GUITexture> ();
//		float Insetx = Screen.width - guiTex.pixelInset.x - guiTex.pixelInset.width;	
//		//guiTex.pixelInset.x = Screen.width - guiTex.pixelInset.x - guiTex.pixelInset.width;		
//		//guiTex.pixelInset.x = Insetx;
//		guiTex.pixelInset = new Rect(Insetx,guiTex.pixelInset.y ,guiTex.pixelInset.width ,guiTex.pixelInset.height ); 
//
//
////		GUITexture ThisGU = guiTex;
////		ThisGU.pixelInset.x = Insetx;
////		guiTex.pixelInset = ThisGU.pixelInset;
		#endif	
		
		
	}
	
	void  OnDisable (){
		if (joystickLeft) 
			joystickLeft.enabled = false;
		
//		if (joystickRight)
//			joystickRight.enabled = false;
	}
	
	void  OnEnable (){
		if (joystickLeft) 
			joystickLeft.enabled = true;
		
//		if (joystickRight)
//			joystickRight.enabled = true;
	}
	
	private bool  button8Down = false;
	private bool  button9Down = false;
	private int moveF;
	private int moveB;
	
	void  OnGUI (){
		GUI.Label(new Rect(10,10,100,25),"8: "+SideMovement+" ("+button8Down+")");
		GUI.Label(new Rect(10,40,100,25),"9: "+SideMovement+" ("+button9Down+")");
		//GUI.Label(new Rect(10,70,140,25),"JS Connected: "+GLOBAL.isJSConnected);
	}
	
	void  Update (){
		// HANDLE CHARACTER MOVEMENT DIRECTION

//		float h = Input.GetAxis("Horizontal");
//		if (h > 0) {
//						SideMovement = 1;
//				} else if (h < 0) {
//						SideMovement = -1;
//				} else {
//						SideMovement = 0;
//				}


//		//if(GLOBAL.isJSConnected)
//		//{
			if(Input.GetButtonDown("Joystick button 8"))
			{
				button8Down = true;
				
			}
			
			if(Input.GetButtonUp("Joystick button 8"))
			{
				button8Down = false;
				
			}
			
			if(Input.GetButtonDown("Joystick button 9"))
			{
				button9Down = true;
				
			}
			
			if(Input.GetButtonUp("Joystick button 9"))
			{
				button9Down = false;
				
			}
			if (!button8Down && button9Down) {
						SideMovement = -1;
				} else if (button8Down && !button9Down) {
						SideMovement = 1;
				} else {
						SideMovement = 0;
				}
			// Inverted logic might be more comfortable
//			if(!button8Down)
//			{
//				moveF = 1;
//				ButtonRight = 1;
//			}
//			else
//			{
//				moveF = 0;
//				ButtonRight = 0;
//			}
//			
//			if(button9Down)
//			{
//				moveB = 1;
//				ButtonLeft = 1;
//			}
//			else
//			{
//				moveB = 0;
//				ButtonLeft = 0;
//			}
		//}
		
		#if UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		
		#elif UNITY_IPHONE
		
		
		#else
		
		#endif
		
		
		
		
		
		playerMovementPlane.normal = character.up;
		playerMovementPlane.distance = -character.position.y + cursorPlaneHeight;
		
		// used to adjust the camera based on cursor or joystick position
		
		
		#if UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		
		
		
		#else
		
		//#if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3 || UNITY_IPHONE)
		#if (UNITY_XBOX360 || UNITY_PS3 || UNITY_IPHONE)
		// On consoles use the analog sticks
		float axisX;
		float axisY;
		
		if(GLOBAL.isJSConnected)
		{
			if (GLOBAL.isJSExtended)
			{
				axisX = Input.GetAxis("RightHorizontal");
				axisY = Input.GetAxis("RightVertical");
				
				//cameraAdjustmentVector = motor.facingDirection;
			}
			else
			{
				axisX = Input.GetAxis("Horizontal");
				axisY = Input.GetAxis("Vertical");
				
			}
		}
		else
		{
			
		}		
		
		#else
		
		// On PC, the cursor point is the mouse position
		
		float halfWidth = Screen.width / 2.0f;
		float halfHeight = Screen.height / 2.0f;
		float maxHalf = Mathf.Max (halfWidth, halfHeight);
		
		// Acquire the relative screen position			
		
		
		
		
		
		#endif
		
		#endif
		
		
	}
	
}