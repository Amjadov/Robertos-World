using UnityEngine;
using System.Collections;


[ExecuteInEditMode] 

public class GUITouchScroll : MonoBehaviour {
	
    public GUISkin optionsSkin;
    public GUIStyle rowSelectedStyle;
	public string[] ButtonLabels;
    // Internal variables for managing touches and drags
	private int selected = -1;
	private float scrollVelocity = 0f;
	private float timeTouchPhaseEnded = 0f;
	
    public Vector2 scrollPosition;

	public float inertiaDuration = 0.75f;
	// size of the window and scrollable list
    public int numRows;
    public Vector2 rowSize;
    public Vector2 windowMargin;
    public Vector2 listMargin;
	
    private Rect windowRect;   // calculated bounds of the window that holds the scrolling list
	private Vector2 listSize;  // calculated dimensions of the scrolling list placed inside the window
	private bool Drawn = false;
    void Update()
    {
		if (Input.touchCount != 1)
		{
			selected = -1;

			if ( scrollVelocity != 0.0f )
			{
				// slow down over time
				float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
				if (scrollPosition.y <= 0 || scrollPosition.y >= (numRows*rowSize.y - listSize.y))
				{
					// bounce back if top or bottom reached
					scrollVelocity = -scrollVelocity;
				}
				
				float frameVelocity = Mathf.Lerp(scrollVelocity, 0, t);
				scrollPosition.y += frameVelocity * Time.deltaTime;

				// after N seconds, we've stopped
				if (t >= 1.0f) scrollVelocity = 0.0f;
			}
			return;
		}
		
		Touch touch = Input.touches[0];
		bool fInsideList = IsTouchInsideList(touch.position);

		if (touch.phase == TouchPhase.Began && fInsideList)
		{
			selected = TouchToRowIndex(touch.position);
			scrollVelocity = 0.0f;
		}
		else if (touch.phase == TouchPhase.Canceled || !fInsideList)
		{
			selected = -1;
		}
		else if (touch.phase == TouchPhase.Moved && fInsideList)
		{
			// dragging
			selected = -1;
			scrollPosition.y += touch.deltaPosition.y;
		}
		else if (touch.phase == TouchPhase.Ended)
		{
            // Was it a tap, or a drag-release?
            if ( selected > -1 && fInsideList )
            {
	            Debug.Log("Player selected row " + selected);
            }
			else
			{
				// impart momentum, using last delta as the starting velocity
				// ignore delta < 10; precision issues can cause ultra-high velocity
				if (Mathf.Abs(touch.deltaPosition.y) >= 10) 
					scrollVelocity = (int)(touch.deltaPosition.y / touch.deltaTime);
				
				timeTouchPhaseEnded = Time.time;
			}
		}
		
	}

    void OnGUI ()
    {

						GUI.skin = optionsSkin;
        
						windowRect = new Rect (windowMargin.x, windowMargin.y, 
        				 Screen.width - (2 * windowMargin.x), Screen.height - (2 * windowMargin.y));
						listSize = new Vector2 (windowRect.width - 2 * listMargin.x, windowRect.height - 2 * listMargin.y);
		
						GUI.Window (0, windowRect, (GUI.WindowFunction)DoWindow, "Choose Story");

						Drawn = true;
				
    }
	
	void DoWindow (int windowID) 
	{
		int levelstate = 0;
		Rect rScrollFrame = new Rect(listMargin.x, listMargin.y, listSize.x, listSize.y);
		//Rect rList        = new Rect(0, 0, rowSize.x, numRows*rowSize.y);
		Rect rList        = new Rect(0, 0, windowRect.width - 5*listMargin.x, numRows*rowSize.y);
		
        scrollPosition = GUI.BeginScrollView (rScrollFrame, scrollPosition, rList, false, false);
            
		//Rect rBtn = new Rect(0, 0, rowSize.x, rowSize.y);
		Rect rBtn = new Rect(0, 0, windowRect.width - 5*listMargin.x, rowSize.y);

        for (int iRow = 0; iRow < numRows; iRow++)
        {
           	// draw call optimization: don't actually draw the row if it is not visible
            if ( rBtn.yMax >= scrollPosition.y && 
                 rBtn.yMin <= (scrollPosition.y + rScrollFrame.height) )
           	{
				string rowLabel;
            	bool fClicked = false;
				if (ButtonLabels.Length > 0f && ButtonLabels[iRow].Length > 0f ){
					levelstate = 0;
					switch(iRow)
					{
					case 0:
						levelstate = GameManager.instance.GetLevelState ("S01_E01");
						break;
					case 1:
						levelstate = GameManager.instance.GetLevelState ("S01_E04");
						break;
					case 2:
						levelstate = GameManager.instance.GetLevelState ("S01_E09");
						break;
					case 3:
						levelstate = GameManager.instance.GetLevelState ("S01_E14");
						break;
					case 4:
						levelstate = GameManager.instance.GetLevelState ("S01_E19");
						break;
					case 5:
						levelstate = GameManager.instance.GetLevelState ("S01_E20");
						break;


					}


					if (levelstate == 1 || GameManager.instance.OpenAllLevelsMode == true || iRow == 0){
						rowLabel = ButtonLabels[iRow];
					} else {
						rowLabel = ButtonLabels[iRow] + " (Locked)";

					}
				} else {
					rowLabel = "Story " + iRow;
				}
               	
               	if ( iRow == selected )
               	{
                	fClicked = GUI.Button(rBtn, rowLabel, rowSelectedStyle);
               	}
               	else
                {
               		fClicked = GUI.Button(rBtn, rowLabel);
                }
                
                // Allow mouse selection, if not running on iPhone.
                if ( fClicked && Application.platform != RuntimePlatform.IPhonePlayer )
                {

                   //Debug.Log("Player mouse-clicked on row " + iRow);
					switch(iRow)
					{
					case 0:
						GameManager.instance.CurrentLevel = "Story";
						Application.LoadLevel("Loading");  
						break;
					case 1:
						levelstate = 0;
						levelstate = GameManager.instance.GetLevelState ("S01_E04");  
						if (levelstate != 0){
						GameManager.instance.CurrentLevel = "Story2";
						Application.LoadLevel("Loading");
						}
						break;
					case 2:
						GameManager.instance.CurrentLevel = "Story3";
						Application.LoadLevel("Loading");  
						break;
					case 3:
						GameManager.instance.CurrentLevel = "Story4";
						Application.LoadLevel("Loading");  
						break;
					case 4:
						GameManager.instance.CurrentLevel = "Story5";
						Application.LoadLevel("Loading");  
						break;
					case 5:
						GameManager.instance.CurrentLevel = "Story6";
						Application.LoadLevel("Loading");  
						break;
					default:
						GameManager.instance.CurrentLevel = "Story";
						Application.LoadLevel("Loading");  
						break;

					}
                }
           	}
           	            
            rBtn.y += rowSize.y;
        }
        GUI.EndScrollView();
	}

    private int TouchToRowIndex(Vector2 touchPos)
    {
		float y = Screen.height - touchPos.y;  // invert y coordinate
		y += scrollPosition.y;  // adjust for scroll position
		y -= windowMargin.y;    // adjust for window y offset
		y -= listMargin.y;      // adjust for scrolling list offset within the window
		int irow = (int)(y / rowSize.y);
		
		irow = Mathf.Min(irow, numRows);  // they might have touched beyond last row
		return irow;
    }
	
	bool IsTouchInsideList(Vector2 touchPos)
	{
		Vector2 screenPos    = new Vector2(touchPos.x, Screen.height - touchPos.y);  // invert y coordinate
		Rect rAdjustedBounds = new Rect(listMargin.x + windowRect.x, listMargin.y + windowRect.y, listSize.x, listSize.y);

		return rAdjustedBounds.Contains(screenPos);
	}

}
 