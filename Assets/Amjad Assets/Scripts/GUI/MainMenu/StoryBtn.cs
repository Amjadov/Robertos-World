using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryBtn : MonoBehaviour
{
	private Text guiTex;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;
	public string AttachedToScene; //if this scene is still locked then the story should be locked too
	public string SceneNameToLoad;
	private bool LevelDisabled = true;
	void Start()
	{

		// Move to right side of screen
		guiTex = GameObject.Find(transform.name + "txt").GetComponent<Text>();

		int levelstate = 0;
		//levelstate = PlayerPrefs.GetInt(LevelName); 
		levelstate = GameManager.instance.GetLevelState(AttachedToScene);
		Debug.Log(AttachedToScene + levelstate.ToString());
		if (levelstate == 0)
			LevelDisabled = true;
		else
			LevelDisabled = false;

		if (AttachedToScene == "S01_E01" || GameManager.instance.OpenAllLevelsMode == true)
			LevelDisabled = false;

		if (LevelDisabled)
		{
			guiTex.text = guiTex.text + " (Locked)";
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, Input.mousePosition))
		{
			DoAction();
			return;
		}

		foreach (Touch touch in Input.touches)
		{



			//User touched the button
			if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{
				DoAction();
				//User lefted his finger
			}
			else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
			{
				//								newTouch = false;
				//								longPressDetected = false;
				//								isPressed = false;
				//								guiTex.color = UnpressedColor;
				//User kept his finger for a longer timer
			}
			else if (touch.phase == TouchPhase.Stationary && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{
				//								newTouch = false;
				//								longPressDetected = true;
				//								isPressed = true;
				//								guiTex.color = PressedColor;
				//User moved his finger while touching 
			}
			else if (touch.phase == TouchPhase.Moved && RectTransformUtility.RectangleContainsScreenPoint(guiTex.rectTransform, touch.position))
			{
				//								newTouch = false;
				//								longPressDetected = true;
				//								isPressed = true;
				//								guiTex.color = PressedColor;

			}
		}




	}
	void DoAction()
	{
		if (LevelDisabled)
			return;

		GameManager.instance.CurrentLevel = SceneNameToLoad;
		GameManager.instance.BackToMain = true;
		Application.LoadLevel("Loading");



	}

}
