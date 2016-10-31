using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	public Color initialColor;
	public Color finalColor;

	private EnemyData data;
	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		data = GetComponent<EnemyData> ();
		renderer = GetComponentInChildren<SpriteRenderer> ();
		float factor = data.data.currentLevel / data.data.maxLevel;
		var newColor = Color.Lerp (finalColor, initialColor, 1);
	}	
}
