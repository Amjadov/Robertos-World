using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStarScript : MonoBehaviour
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
    public Texture ZeroStarTexture;
    public Texture OneStarTexture;
    public Texture TwoStarTexture;
    public Texture ThreeStarTexture;
    private int StarRating;
    private string LevelName;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        LevelName = transform.parent.Find("LevelBackFrame").GetComponent<LevelSelectBtnScript>().LevelName;
        LevelName = LevelName + "Stars";
        StarRating = PlayerPrefs.GetInt(LevelName);

        if (StarRating == 0)
        {
            image.sprite = Sprite.Create((Texture2D)ZeroStarTexture, new Rect(0f, 0f, ZeroStarTexture.width, ZeroStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 1)
        {
            image.sprite = Sprite.Create((Texture2D)OneStarTexture, new Rect(0f, 0f, OneStarTexture.width, OneStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 2)
        {
            image.sprite = Sprite.Create((Texture2D)TwoStarTexture, new Rect(0f, 0f, TwoStarTexture.width, TwoStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 3)
        {
            image.sprite = Sprite.Create((Texture2D)ThreeStarTexture, new Rect(0f, 0f, ThreeStarTexture.width, ThreeStarTexture.height), Vector2.zero);
        }

        getRatio();
        pxWidth = image.rectTransform.rect.width * guiWRatio;
        pxHeight = image.rectTransform.rect.height * guiHRatio;
        if (SpaceFromRight == 0)
            SpaceFromRight = image.rectTransform.anchoredPosition.x;

        if (SpaceFromTop == 0)
            SpaceFromTop = image.rectTransform.anchoredPosition.y;

        pxX = (SpaceFromRight * guiWRatio);
        pxY = (SpaceFromTop * guiHRatio);

        image.rectTransform.sizeDelta = new Vector2(pxWidth, pxHeight);
        image.rectTransform.anchoredPosition = new Vector2(pxX, pxY);
    }

    void getRatio()
    {
        sWidth = Screen.width;
        sHeight = Screen.height;
        guiWRatio = sWidth / DesignedScaleWidth;
        guiHRatio = sHeight / DesignedScaleHeight;
    }

}
