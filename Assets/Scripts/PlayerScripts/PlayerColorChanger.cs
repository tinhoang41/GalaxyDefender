using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColorChanger : ColorChanger {


    public List<Color> colorsToChange;
    private Color initialColor;

    protected ImmortalType currentImmortalType ;
    protected int currentColorIndex;

    protected override bool pRunCoroutineAtStart { get { return true; } }

    // Use this for initialization
    protected override void Initialize ()
    {
        base.Initialize ();
        initialColor        = renderers.Count > 0 ? renderers [0].color : Color.white;
        newColor            = oldColor = initialColor;
        currentImmortalType = ImmortalType.INVALID;
        currentColorIndex   = 0;
    }

    protected override void EndOfSwitching ()
    {
        ApplyColor (initialColor);
        currentImmortalType  = ImmortalType.INVALID;
        currentColorIndex    = 0;
    }

    protected override void UpdateColors ()
    {
        switch (currentImmortalType)
        {
            case ImmortalType.RECOVERING:
                {
                    oldColor     = new Color(initialColor.r, initialColor.g, initialColor.b, newColor.a);
                    newColor     = new Color(initialColor.r, initialColor.g, initialColor.b, oldColor.a >= 0.5f ? 0.0f : 1.0f);
                    break;
                }
            case ImmortalType.ITEM:
                {
                    oldColor = newColor;
                    newColor = colorsToChange[++currentColorIndex % colorsToChange.Count];
                    break;
                }
        }
       
    }


    public void ChangePlayerColor(ImmortalType immortalType, float duration)
    {
        currentImmortalType = immortalType;
        StartCoroutine("SwitchColor", duration);
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
    }
}
