using UnityEngine;
using System.Collections;

public class RandomColour : RandomColorGenerator {

    public bool useRandomColor = false;
    // Use this for initialization
    void Start () {
        SpriteRenderer spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        if(useRandomColor)
            spr.color = GetRandomColor();
    }
}
