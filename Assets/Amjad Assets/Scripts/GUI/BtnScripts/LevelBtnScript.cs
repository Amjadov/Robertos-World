using UnityEngine;
using UnityEngine.UI;

public class LevelBtnScript : MonoBehaviour
{
    private float pxX;
    private Image image;

    public bool isPressed = false;

    // Use this for initialization
    void Start()
    {
        // Move to right side of screen
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            //User touched the button
            if (touch.phase == TouchPhase.Began && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch.position))
            {
                audio.Play();
                Application.LoadLevel(image.name);
                //User lefted his finger
            }
        }
    }
}
