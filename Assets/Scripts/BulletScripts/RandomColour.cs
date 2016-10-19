using UnityEngine;
using System.Collections;

public class RandomColour : RandomColorGenerator {

    // Use this for initialization
    void Start () {
        SpriteRenderer spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        spr.color = GetRandomColor();
    }
}
