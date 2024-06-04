using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectBtnScript : MonoBehaviour
{
	private float pxX;
	private float pxY;
	private float pxWidth;
	private float pxHeight;
	private Image image;
	public Color PressedColor;
	public Color UnpressedColor;
	public bool isPressed = false;
	private bool newTouch = false;
	public bool longPressDetected = false;
	public float SpaceFromRight = 0f;
	public float SpaceFromTop = 0f;
	private float touchTime;
	private float sWidth;
	private float sHeight;
	private float guiWRatio;
	private float guiHRatio;
	private static float DesignedScaleWidth = 800;
	private static float DesignedScaleHeight = 480;
	private int finger = 0;
	public Sprite EnabledSprite;
	public Sprite DisableSprite;
	public string LevelName;
	public string NextLevelName;
	private bool LevelDisabled = true;
	// Use this for initialization
	void Awake()
	{
		image = GetComponent<Image>();
		int levelstate = 0;
		levelstate = GameManager.instance.GetLevelState(LevelName);
		if (levelstate == 0)
			LevelDisabled = true;
		else
			LevelDisabled = false;

		if (LevelName == "S01_E01" || GameManager.instance.OpenAllLevelsMode == true)
			LevelDisabled = false;

		if (LevelDisabled)
		{
			image.sprite = DisableSprite;
		}
		else
		{
			image.sprite = EnabledSprite;
		}
	}

	void Start()
	{
		getRatio();
		pxWidth = image.rectTransform.rect.width * guiWRatio;
		pxHeight = image.rectTransform.rect.height * guiHRatio;
		if (SpaceFromRight == 0)
			SpaceFromRight = image.rectTransform.offsetMax.x;

		if (SpaceFromTop == 0)
			SpaceFromTop = -image.rectTransform.offsetMin.y;

		pxX = (SpaceFromRight * guiWRatio) - (sWidth / 2f);
		pxY = (sHeight / 2f) - (SpaceFromTop * guiHRatio);

		image.rectTransform.offsetMax = new Vector2(pxX + pxWidth / 2f, pxY + pxHeight / 2f);
		image.rectTransform.offsetMin = new Vector2(pxX - pxWidth / 2f, pxY - pxHeight / 2f);
	}

	void getRatio()
	{
		sWidth = Screen.width;
		sHeight = Screen.height;

		guiWRatio = sWidth / DesignedScaleWidth;
		guiHRatio = sHeight / DesignedScaleHeight;
	}

	void Update()
	{
		if (LevelDisabled)
			return;

		if (Input.GetMouseButtonDown(0) && image.rectTransform.rect.Contains(Input.mousePosition))
		{
			DoAction();
			return;
		}

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began && image.rectTransform.rect.Contains(touch.position))
			{
				DoAction();
			}
		}
	}

	void DoAction()
	{
		GameManager.instance.CurrentLevel = LevelName;
		GameManager.instance.NextLevel = NextLevelName;
		Application.LoadLevel("Loading");
	}

}
