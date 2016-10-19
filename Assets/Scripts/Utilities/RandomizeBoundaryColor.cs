using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeBoundaryColor : RandomColorGenerator {

    private Color                   oldColor;
    private Color                   newColor;
    private Color                   currentColor;
    private float                   currentTime;
    private List<SpriteRenderer>    renderers;

    public float  LerpTime;
    
    // Use this for initialization
    void Start ()
    {
        oldColor    = Color.white;
        newColor    = GetRandomColor();
        currentTime = 0.0f;
        renderers   = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (currentTime >= LerpTime)
        {
            currentTime = 0;
            oldColor    = currentColor;
            newColor    = GetRandomColor();
        }
        else
        {
            currentColor = Color.Lerp(oldColor, newColor, currentTime / LerpTime);
            currentTime += Time.deltaTime;
            ApplyColor();
        }
    }

    void ApplyColor()
    {
        foreach (var renderer in renderers)
            renderer.color = currentColor;
    }

    IEnumerator ChangeColor()
    {
        while(true)
        {
            if (currentColor == newColor)
            {
                oldColor = currentColor;
                newColor = GetRandomColor();
            }

            currentColor = Color.Lerp(oldColor, newColor, currentTime / LerpTime);
            ApplyColor();
            yield return null;
        }
    }
}
