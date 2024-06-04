using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkipBtn : MonoBehaviour
{
	public static SkipBtn current;
	private Image guiTex;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;

	void Awake()
	{
		current = this;
	}

	void Start()
	{

		// Move to right side of screen
		guiTex = GetComponent<Image>();
	}


	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && guiTex.rectTransform.rect.Contains(Input.mousePosition))
		{
			EndScene();
			return;
		}

        foreach (Touch touch in Input.touches)
        {



            //User touched the button
            if (touch.phase == TouchPhase.Began && guiTex.rectTransform.rect.Contains(touch.position))
            {
                EndScene();
                //User lefted his finger
            }
            else if (touch.phase == TouchPhase.Ended && touch.fingerId == finger || touch.phase == TouchPhase.Canceled && touch.fingerId == finger)
            {
                //                                newTouch = false;
                //                                longPressDetected = false;
                //                                isPressed = false;
                //                                guiTex.color = UnpressedColor;
                //User kept his finger for a longer timer
            }
            else if (touch.phase == TouchPhase.Stationary && guiTex.rectTransform.rect.Contains(touch.position))
            {
                //                                newTouch = false;
                //                                longPressDetected = true;
                //                                isPressed = true;
                //                                guiTex.color = PressedColor;
                //User moved his finger while touching 
            }
            else if (touch.phase == TouchPhase.Moved && guiTex.rectTransform.rect.Contains(touch.position))
            {
                //                                newTouch = false;
                //                                longPressDetected = true;
                //                                isPressed = true;
                //                                guiTex.color = PressedColor;

            }
        }




    }

    public void EndScene()
    {
        if (GameManager.instance.CurrentLevel == Application.loadedLevelName)
        {
            Debug.Log(GameManager.instance.NextLevel);
            Debug.Log(GameManager.instance.GetNextLevel());
            GameManager.instance.NextLevel = GameManager.instance.GetNextLevel();
            GameManager.instance.CurrentLevel = GameManager.instance.NextLevel;
            PlayerPrefs.SetInt("Load" + Application.loadedLevelName, 1);
            PlayerPrefs.Save();
        }

        if (GameManager.instance.BackToMain)
        {
            GameManager.instance.BackToMain = false;
            GameManager.instance.CurrentLevel = "Main";
        }
        Application.LoadLevel("Loading");

    }
}
