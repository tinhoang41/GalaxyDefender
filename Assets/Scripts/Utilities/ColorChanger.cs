using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour {

	public float lerpTime;

	protected List<SpriteRenderer> renderers;
	protected Color oldColor;
	protected Color newColor;

	protected virtual bool pRunCoroutineAtStart 
	{
		get { return false; }
	}
	// Use this for initialization
	void Start ()
	{
		GetRenderers ();
		Initialize ();
		if(pRunCoroutineAtStart)
			RunCoroutine ();
	}

	protected virtual void GetRenderers()
	{
		renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer> ());
	}

	protected virtual void ApplyColor(Color newColor)
	{
		foreach (var renderer in renderers)
			renderer.color = newColor;
	}

	protected IEnumerator SwitchColor(float duration)
	{
		var currentLerpTime   = 0.0f;
		var totalTime         = 0.0f;

		while (SwitchingStatus(totalTime, duration)) 
		{
			if (currentLerpTime >= lerpTime)
			{
				currentLerpTime = 0;
				UpdateColors ();
			}
			else
				ApplyColor (Color.Lerp(oldColor, newColor, currentLerpTime / lerpTime));
			
			currentLerpTime += Time.deltaTime;
			totalTime       += Time.deltaTime;
			yield return null;
		}
		EndOfSwitching ();
	}

	protected virtual bool SwitchingStatus(float totalTime, float duration)
	{
		// if input duration is negative 1, inifinite loop of switching color
		// else if totalTime of changing reaches duration
		return duration == -1.0f || totalTime <= duration;
	}

	protected virtual void Initialize()     { }
	protected virtual void UpdateColors()   { }
	protected virtual void RunCoroutine()   { }
	protected virtual void EndOfSwitching() { }
}
