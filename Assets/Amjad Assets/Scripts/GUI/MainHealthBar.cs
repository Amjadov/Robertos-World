using UnityEngine;
using UnityEngine.UI;

public class MainHealthBar : MonoBehaviour
{
    public Texture[] textures;
    public Texture NewTexture;
    private Image UsedTexture;

    // Use this for initialization
    void Start()
    {
        // Move to right side of screen
        UsedTexture = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NewTexture != UsedTexture.sprite.texture)
        {
            UsedTexture.sprite = Sprite.Create((Texture2D)NewTexture, new Rect(0, 0, NewTexture.width, NewTexture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
