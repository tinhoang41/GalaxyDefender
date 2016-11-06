using UnityEngine;
using System.Collections;

public class RandomColorGenerator : MonoBehaviour {

    private const int MAX_COLOR_VALUE = 255;

    public int minRed;
    public int minBlue;
    public int minGreen;

    public Color GetRandomColor()
    {
        return new Color
            (
                Random.Range((float)minRed   / MAX_COLOR_VALUE , 1.0f) , 
                Random.Range((float)minBlue  / MAX_COLOR_VALUE, 1.0f)  , 
                Random.Range((float)minGreen / MAX_COLOR_VALUE, 1.0f)  , 
                1.0f
            );
    }
}
