using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

    public Color maxLevelColor;
    public Color minLevelColor;

    private EnemyData enemyData;
    private SpriteRenderer textureRenderer;

    // Use this for initialization
    void Start()
    {
        enemyData = GetComponent<EnemyData>();
        textureRenderer = GetComponentInChildren<SpriteRenderer>();
        float factor = (float)enemyData.pCurrentLife / enemyData.pMaxlife;
        var newColor = Color.Lerp(minLevelColor, maxLevelColor, factor);
        textureRenderer.color = newColor;
    }

    public void ChangeColor()
    {
        float factor          = (float)enemyData.pCurrentLife / enemyData.pMaxlife;
        var newColor          = Color.Lerp(minLevelColor, maxLevelColor, factor);
        textureRenderer.color = newColor;
    }
}
