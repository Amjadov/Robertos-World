#pragma strict

// Objects to drag in
public var character : Transform;
public var cursorPrefab : GameObject;
public var joystickPrefab : GameObject;

// Cursor settings
public var cursorPlaneHeight : float = 0;
public var cursorFacingCamera : float = 0;
public var cursorSmallerWithDistance : float = 0;
public var cursorSmallerWhenClose : float = 1;

public var ButtonXAxis: float = 0;
public var ButtonYAxis : float = 0; 
// Private memeber data

private var cursorObject : Transform;
private var joystickLeft : Joystick_Script;
private var joystickRight : Joystick_Script;

// Prepare a cursor point varibale. This is the mouse position on PC and controlled by the thumbstick on mobiles.
private var cursorScreenPosition : Vector2;

private var playerMovementPlane : Plane;

private var joystickRightGO : GameObject;


function Awake () {		
	
	// Ensure we have character set
	// Default to using the transform this component is on
	if (!character)
		character = transform;
	#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		if (joystickPrefab) {
			// Create left joystick
			var joystickLeftGO : GameObject = Instantiate (joystickPrefab) as GameObject;
			joystickLeftGO.name = "Joystick Left";
			joystickLeft = joystickLeftGO.GetComponent.<Joystick_Script> ();
			
//			// Create right joystick
//			joystickRightGO = Instantiate (joystickPrefab) as GameObject;
//			joystickRightGO.name = "Joystick Right";
//			joystickRight = joystickRightGO.GetComponent.<Joystick_Script> ();			
		}
	#elif !UNITY_FLASH
		if (cursorPrefab) {
			cursorObject = (Instantiate (cursorPrefab) as GameObject).transform;
		}
	#endif
	
	
	
}

function Start () {
	#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
		// Move to right side of screen
//		var guiTex : GUITexture = joystickRightGO.GetComponent.<GUITexture> ();
//		guiTex.pixelInset.x = Screen.width - guiTex.pixelInset.x - guiTex.pixelInset.width;			
	#endif	
	
	
}

function OnDisable () {
	if (joystickLeft) 
		joystickLeft.enabled = false;
	
	if (joystickRight)
		joystickRight.enabled = false;
}

function OnEnable () {
	if (joystickLeft) 
		joystickLeft.enabled = true;
	
	if (joystickRight)
		joystickRight.enabled = true;
}

private var button8Down : boolean = false;
private var button9Down : boolean = false;
private var moveF : int;
private var moveB : int;

function OnGUI()
{
//	GUI.Label(new Rect(10,10,500,50),"8 ButtonXAxis: "+ButtonXAxis+" ("+button8Down+")");
//	GUI.Label(new Rect(10,40,500,50),"9 ButtonYAxis: "+ButtonYAxis+" ("+button9Down+")");
//	GUI.Label(new Rect(10,70,540,50),"JS Connected: "+GLOBAL.isJSConnected);
}

function Update () {
	// HANDLE CHARACTER MOVEMENT DIRECTION

	if(GLOBAL.isJSConnected)
	{
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

		// Inverted logic might be more comfortable
		if(!button8Down)
		{
			moveF = 1;
			ButtonXAxis = 1;
		}
		else
		{
			moveF = 0;
			ButtonXAxis = 0;
		}

		if(button9Down)
		{
			moveB = 1;
			ButtonYAxis = 1;
		}
		else
		{
			moveB = 0;
			ButtonYAxis = 0;
		}
	}

	#if UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
	//motor.movementDirection = joystickLeft.position.x * screenMovementRight + joystickLeft.position.y * screenMovementForward;
		ButtonXAxis = joystickLeft.position.x; 
		ButtonYAxis =  joystickLeft.position.y;
	#elif UNITY_IPHONE
		if(GLOBAL.isJSConnected)
		{
			if (GLOBAL.isJSExtended)
				motor.movementDirection = Input.GetAxis("Horizontal") * screenMovementRight + Input.GetAxis("Vertical") * screenMovementForward;
			else
				motor.movementDirection = -moveB * (motor.facingDirection*2) + moveF * (motor.facingDirection*2);
		}
		else
		{
			motor.movementDirection = joystickLeft.position.x * screenMovementRight + joystickLeft.position.y * screenMovementForward;
		}

	#else
		//motor.movementDirection = Input.GetAxis ("Horizontal") * screenMovementRight + Input.GetAxis ("Vertical") * screenMovementForward;
	#endif
		
	
	
	
	
	playerMovementPlane.normal = character.up;
	playerMovementPlane.distance = -character.position.y + cursorPlaneHeight;
	
	// used to adjust the camera based on cursor or joystick position
	

	#if UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY || UNITY_TIZEN
	
		
	
	#else
	
		//#if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3 || UNITY_IPHONE)
		#if (UNITY_XBOX360 || UNITY_PS3 || UNITY_IPHONE)
			// On consoles use the analog sticks
			var axisX : float;
			var axisY : float;

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
		
			var halfWidth : float = Screen.width / 2.0f;
			var halfHeight : float = Screen.height / 2.0f;
			var maxHalf : float = Mathf.Max (halfWidth, halfHeight);
			
			// Acquire the relative screen position			
			
						
		
		
			
		#endif
		
	#endif
		

}





