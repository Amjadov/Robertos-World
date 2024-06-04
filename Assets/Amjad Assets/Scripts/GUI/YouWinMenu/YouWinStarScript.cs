using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YouWinStarScript : MonoBehaviour
{
    private Image guiImg;
    public Texture ZeroStarTexture;
    public Texture OneStarTexture;
    public Texture TwoStarTexture;
    public Texture ThreeStarTexture;
    private int StarRating;
    private string LevelName;
    public int OneStarMinimumScore = 0;
    public int TwoStarMinimumScore = 20;
    public int ThreeStarMinimumScore = 50;

    void Start()
    {
        // Move to right side of screen
        guiImg = GetComponent<Image>();
        int Score = GameObject.Find("Score").GetComponent<Score>().score;
        if (Score >= ThreeStarMinimumScore)
        {
            StarRating = 3;
        }
        else if (Score >= TwoStarMinimumScore && Score < ThreeStarMinimumScore)
        {
            StarRating = 2;
        }
        else if (Score >= OneStarMinimumScore && Score < TwoStarMinimumScore)
        {
            StarRating = 1;
        }
        if (StarRating == 0)
        {
            guiImg.sprite = Sprite.Create((Texture2D)ZeroStarTexture, new Rect(0, 0, ZeroStarTexture.width, ZeroStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 1)
        {
            guiImg.sprite = Sprite.Create((Texture2D)OneStarTexture, new Rect(0, 0, OneStarTexture.width, OneStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 2)
        {
            guiImg.sprite = Sprite.Create((Texture2D)TwoStarTexture, new Rect(0, 0, TwoStarTexture.width, TwoStarTexture.height), Vector2.zero);
        }
        else if (StarRating == 3)
        {
            guiImg.sprite = Sprite.Create((Texture2D)ThreeStarTexture, new Rect(0, 0, ThreeStarTexture.width, ThreeStarTexture.height), Vector2.zero);
        }

        PlayerPrefs.SetInt(Application.loadedLevelName + "Stars", StarRating);
        PlayerPrefs.Save();
    }
}
