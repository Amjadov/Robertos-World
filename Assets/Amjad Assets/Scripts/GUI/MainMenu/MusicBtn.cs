using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicBtn : MonoBehaviour
{
    private Image guiImage;
    public Color PressedColor;
    public Color UnpressedColor;
    public bool isPressed = false;
    private bool newTouch = false;
    public bool longPressDetected = false;
    private float touchTime;
    private int finger = 0;
    public Texture MusicOnTex;
    public Texture MusicOFFTex;
    // Use this for initialization
    void Awake()
    {
        guiImage = GetComponent<Image>();
        CheckMusicState();
    }

    void CheckMusicState()
    {
        int MusicMute = PlayerPrefs.GetInt("MusicMute");
        GameManager.instance.MusicMute = MusicMute;

        if (MusicMute == 1)
        {
            guiImage.sprite = Sprite.Create((Texture2D)MusicOFFTex, new Rect(0, 0, MusicOFFTex.width, MusicOFFTex.height), Vector2.zero);
        }
        else
        {
            guiImage.sprite = Sprite.Create((Texture2D)MusicOnTex, new Rect(0, 0, MusicOnTex.width, MusicOnTex.height), Vector2.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(guiImage.rectTransform, Input.mousePosition))
        {
            DoAction();
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(guiImage.rectTransform, touch.position))
            {
                DoAction();
            }
        }
    }
    void DoAction()
    {
        MuteTheMusic();
        CheckMusicState();
        GameObject.Find("music").GetComponent<MusicMuter>().CheckMusicState();
    }

    public void MuteTheMusic()
    {
        if (GameManager.instance.MusicMute == 0)
        {
            PlayerPrefs.SetInt("MusicMute", 1);
            GameManager.instance.MusicMute = 1;
        }
        else
        {
            PlayerPrefs.SetInt("MusicMute", 0);
            GameManager.instance.MusicMute = 0;
        }
        PlayerPrefs.Save();
    }
}
