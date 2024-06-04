using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public int score = 0; // The player's score.


	private PlayerControl playerControl;    // Reference to the player control script.
	private int previousScore = 0;              // The score in the previous frame.
	private Text guiText;

	void Awake()
	{
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		// Move to right side of screen
		guiText = GetComponent<Text>();
	}


	void Update()
	{
		// Set the score text.
		guiText.text = score.ToString("000000"); //"Score: " + score;

		// If the score has changed...
		if (previousScore != score)
		{
			// ... play a taunt.
			//playerControl.StartCoroutine(playerControl.Taunt());
		}

		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
