using UnityEngine;
using System.Collections;

public class SpeechScript : MonoBehaviour
{
		private Transform target;
		public float Duration = 3f;
		public float Xpos = 1f;
		public float Ypos = 3f;
		public float Zpos = 0f;
		public bool SpeechFollowup = false;  //this is when another speech needs to follow this speech
		public Transform SpeechToFollow;
		public bool TargetNotPlayer = false;
		public Transform OtherTarget;
		// Use this for initialization
		void Start ()
		{
				if (TargetNotPlayer && OtherTarget) {
						target = OtherTarget;
				} else {
						target = GameObject.FindGameObjectWithTag ("Player").transform; 
				}
	
				if (SpeechFollowup && SpeechToFollow) {
						Invoke ("ShowFollowupSpeech", Duration);
				}

				DestroyObject (gameObject, Duration);
		}
	
		// Update is called once per frame
		void Update ()
		{
				transform.position = new Vector3 (target.position.x + Xpos, target.position.y + Ypos, target.position.z + Zpos);
		}

		void ShowFollowupSpeech ()
		{

				SpeechToFollow.gameObject.SetActive (true);

		}
}
