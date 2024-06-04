using UnityEngine;
using System.Collections;

public class MusicMuter : MonoBehaviour {
	void Start ()
	{

		CheckMusicState ();

	}
	public void CheckMusicState()
	{
				if (GameManager.instance.MusicMute == 1) {
			transform.audio.mute = true;
			
				} else {
			transform.audio.mute = false;
			transform.audio.Play(); 
				}
		}
}
