using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorChanger : ColorChanger {


	public List<Color> colorsToChange;
	private Color initialColor;

	protected ImmortalType currentImmortalType;
	protected int currentColorIndex;

	protected override bool pRunCoroutineAtStart { get { return true; } }

	// Use this for initialization
	protected override void Initialize ()
	{
		base.Initialize ();
		initialColor = renderers.Count > 0 ? renderers [0].color : Color.white;
		currentImmortalType = ImmortalType.ITEM;
		currentColorIndex = 0;
	}

//	IEnumerator Flashing(float duration)
//	{
//		var currentTime = 0.0f;
//		var oldAlpha = spriteRenderer.color.a >= 0.5f ? 1.0f : 0.0f;
//		var targetAlpha = spriteRenderer.color.a >= 0.5f ? 0.0f : 1.0f;
//		var totalTime = 0.0f;
//		while (totalTime < duration) 
//		{
//			if (currentTime >= timeFrame)
//			{
//				currentTime = 0;
//				oldAlpha    = spriteRenderer.color.a;
//				targetAlpha = oldAlpha >= 0.5 ? 0.0f : 1.0f;
//			}
//			else
//			{
//				var currentAlpha = Mathf.Lerp(oldAlpha, targetAlpha, currentTime / timeFrame);
//				currentTime += Time.deltaTime;
//				var newColor = spriteRenderer.color;
//				newColor.a = currentAlpha;
//				spriteRenderer.color = newColor;
//			}
//			totalTime += Time.deltaTime;
//			yield return null;
//		}
//	}
//
//	IEnumerator Switch(float duration)
//	{
//		var currentTime = 0.0f;
//		var currentColorIndex = 0;
//		var oldColor = initialColor;
//		var targetColor = colorsToChange[currentColorIndex];
//		var totalTime = 0.0f;
//
//		while (totalTime < duration) 
//		{
//			if (currentTime >= timeFrame)
//			{
//				currentTime = 0;
//				oldColor = targetColor;
//				targetColor = colorsToChange[++currentColorIndex % colorsToChange.Count];
//			}
//			else
//			{
//				var newColor = Color.Lerp(oldColor, targetColor, currentTime / timeFrame);
//				currentTime += Time.deltaTime;
//				spriteRenderer.color = newColor;
//			}
//			totalTime += Time.deltaTime;
//			yield return null;
//		}
//	}
		
	protected override void EndOfSwitching ()
	{
		ApplyColor (initialColor);
		//currentImmortalType  = ImmortalType;
		currentColorIndex    = 0;
	}

	protected override void UpdateColors ()
	{
		oldColor = newColor;
		newColor = colorsToChange [++currentColorIndex % colorsToChange.Count];
	}

	public void ChangePlayerColor(ImmortalType immortalType, float duration)
	{
		//currentImmortalType = immortalType;
		//StartCoroutine ("SwitchColor", duration);
//		switch (immortalType) 
//		{
//			case ImmortalType.RECOVERING:
//				StartCoroutine ("Flashing", duration);
//				break;
//			case ImmortalType.ITEM:
//				StartCoroutine ("SwitchColor", duration);
//				break;
//		}

	}

	protected override void RunCoroutine ()
	{
		StartCoroutine ("SwitchColor", -1.0f);
	}
}
