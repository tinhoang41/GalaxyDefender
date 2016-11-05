using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorChanger : MonoBehaviour {


	public List<Color> colorsToChange;
	public float timeFrame;

	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		initialColor = spriteRenderer.color;
		StartCoroutine ("SwitchColor");
	}

	IEnumerator Flashing()
	{
		var currentTime = 0.0f;
		var oldAlpha = spriteRenderer.color.a >= 0.5f ? 1.0f : 0.0f;
		var targetAlpha = spriteRenderer.color.a >= 0.5f ? 0.0f : 1.0f;

		while (true) 
		{
			if (currentTime >= timeFrame)
			{
				currentTime = 0;
				oldAlpha    = spriteRenderer.color.a;
				targetAlpha = oldAlpha >= 0.5 ? 0.0f : 1.0f;
			}
			else
			{
				var currentAlpha = Mathf.Lerp(oldAlpha, targetAlpha, currentTime / timeFrame);
				currentTime += Time.deltaTime;
				var newColor = spriteRenderer.color;
				newColor.a = currentAlpha;
				spriteRenderer.color = newColor;
			}
			yield return null;
		}
	}

	IEnumerator SwitchColor()
	{
		var currentTime = 0.0f;
		var currentColorIndex = 0;
		var oldColor = initialColor;
		var targetColor = colorsToChange[currentColorIndex];

		while (true) 
		{
			if (currentTime >= timeFrame)
			{
				currentTime = 0;
				oldColor = targetColor;
				targetColor = colorsToChange[++currentColorIndex % colorsToChange.Count];
			}
			else
			{
				var newColor = Color.Lerp(oldColor, targetColor, currentTime / timeFrame);
				currentTime += Time.deltaTime;
				spriteRenderer.color = newColor;
			}
			yield return null;
		}
	}

	public void Reset()
	{
		StopAllCoroutines ();
		spriteRenderer.color = initialColor;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
