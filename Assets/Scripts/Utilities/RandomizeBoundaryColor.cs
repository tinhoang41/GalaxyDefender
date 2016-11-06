using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeBoundaryColor : ColorChanger {

	protected RandomColorGenerator colorGenerator;
	protected override bool pRunCoroutineAtStart 
	{
		get { return true; }
	}

	protected override void Initialize ()
	{
		base.Initialize ();
		colorGenerator = GetComponent<RandomColorGenerator> ();
		UpdateColors ();
	}
	protected override void RunCoroutine ()
	{
		base.RunCoroutine ();
		StartCoroutine ("SwitchColor", -1.0f);
	}

	protected override void UpdateColors ()
	{
		oldColor = newColor;
		newColor = colorGenerator.GetRandomColor ();
	}
}
