using UnityEngine;
using System.Collections;

public class ScrollScript_Auto : MonoBehaviour
{
		public bool AutoScroll = false;
		public float AutoScrollSpeed = 0;
		private float counter = 1;
	public bool AutoScroll_Style2 = false;
	public bool Style2_ScrollLeft = true;
		void Update ()
		{
				if (AutoScroll)
						renderer.material.mainTextureOffset = new Vector2 (Time.time * AutoScrollSpeed, 0f); 
		if (AutoScroll_Style2)
			CounterScroll();
		}

		public void Scroll (float h)
		{
				renderer.material.mainTextureOffset = new Vector2 (counter * AutoScrollSpeed, 0f);
				if (h > 0) {
						counter += 0.01f;
				} else if (h < 0) {
						counter -= 0.01f;
				}
		}
	public void CounterScroll ()
	{
		renderer.material.mainTextureOffset = new Vector2 (counter * AutoScrollSpeed, 0f);

		if (Style2_ScrollLeft) {
			counter += 0.01f;
		} else if (!Style2_ScrollLeft) {
			counter -= 0.01f;
		}
	}
}
