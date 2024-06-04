using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // Added to use UI.Text

public class CreditsScroll : MonoBehaviour
{
    private int i = 0;

    /* Public Variables */

    // The array of UI.Text elements to display and scroll
    public Text[] textElements;

    // The delay time before displaying the UI.Text elements
    public float displayTime = 5.0f;

    // The delay time before starting the UI.Text scroll
    public float scrollTime = 5.0f;

    // The scrolling speed
    public float scrollSpeed = 0.2f;

    void Start()
    {
        foreach (Text text in textElements)
        {
            text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 0.9f, text.transform.position.z);
            i -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // start display count down
        displayTime -= Time.deltaTime;

        if (displayTime < 0)
        {
            // if it is time to display, start the scrolling count down timer
            scrollTime -= Time.deltaTime;
        }

        // if it is time to scroll, cycle through the UIElements and
        // increase their Y position by the desired speed
        if (scrollTime < 0)
        {
            foreach (Text text in textElements)
            {
                text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + scrollSpeed, text.transform.position.z);
                if (text.transform.position.y >= 1.1f)
                    text.transform.position = new Vector3(text.transform.position.x, 0, text.transform.position.z);
            }
        }
    }
}
