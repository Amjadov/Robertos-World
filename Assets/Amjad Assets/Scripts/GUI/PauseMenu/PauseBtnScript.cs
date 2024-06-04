using UnityEngine;
using UnityEngine.UI;

public class PauseBtnScript : MonoBehaviour
{
    private Image image;
    public Color pressedColor;
    public Color unpressedColor;
    public bool isPressed = false;
    private bool newTouch = false;
    public bool longPressDetected = false;
    private float touchTime;
    private int finger = 0;
    public GameObject pauseMenu;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, Input.mousePosition))
        {
            DoAction();
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
            {
                DoAction();
            }
        }
    }

    void DoAction()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.ShowBannerAds(true);
        GameObject.Find("Pauser").GetComponent<Pauser>().paused = true;
        AudioListener.pause = true;
    }
}
