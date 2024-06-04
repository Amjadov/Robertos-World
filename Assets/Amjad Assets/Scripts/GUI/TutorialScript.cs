using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour
{
	private UnityEngine.UI.Image guiTex;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	private float touchTime;
	private int finger = 0;
	public int TutorialNumber = 0;
	void Awake()
	{
		int LoadTutorial = 0;

		LoadTutorial = PlayerPrefs.GetInt("LoadTutorial" + TutorialNumber.ToString());
		if (LoadTutorial == 1)
		{
			transform.parent.gameObject.SetActive(false);
			return;

		}

		AudioListener.pause = true;

		GameObject.Find("Pauser").GetComponent<Pauser>().paused = true;
	}
	// Use this for initialization
	void Start()
	{
		guiTex = GetComponent<UnityEngine.UI.Image>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && guiTex.rectTransform.rect.Contains(Input.mousePosition))
		{
			DoAction();
			return;

		}

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began && guiTex.rectTransform.rect.Contains(touch.position))
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
			else if (touch.phase == TouchPhase.Stationary && guiTex.rectTransform.rect.Contains(touch.position))
			{
				//								newTouch = false;
				//								longPressDetected = true;
				//								isPressed = true;
				//								guiTex.color = PressedColor;
				//User moved his finger while touching 
			}
			else if (touch.phase == TouchPhase.Moved && guiTex.rectTransform.rect.Contains(touch.position))
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
		GameObject.Find("Pauser").GetComponent<Pauser>().paused = false;
		transform.parent.gameObject.SetActive(false);
		AudioListener.pause = false;
		PlayerPrefs.SetInt("LoadTutorial" + TutorialNumber.ToString(), 1);
		PlayerPrefs.Save();

	}

}
