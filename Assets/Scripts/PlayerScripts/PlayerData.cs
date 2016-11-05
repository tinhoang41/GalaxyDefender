using UnityEngine;
using System.Collections;

public enum ImmortalType :ushort
{
	RECOVERING = 0,
	ITEM = 1,
}
public class PlayerData : MonoBehaviour {

	public float immortalTimeForRecovering;
	public float immortalTimeFromItem;
	public int defaultLives;

	private int currentLives;
	private float immortalTime;
	// become immortal when got hit or get items
	private bool isImmortal;

	public void ApplyDamage(int damage)
	{
		if (isImmortal)
			return;
		
		currentLives -= damage;
		Mathf.Max (currentLives, 0);
		if (currentLives > 0) 
		{
			isImmortal = true;
			immortalTime = immortalTimeForRecovering;
			StartCoroutine ("RunImmortality", ImmortalType.RECOVERING);
		}
	}

	IEnumerator RunImmortality(ImmortalType immortalType)
	{
		Debug.Assert (!isImmortal);
		var currentTime = 0.0f;
		while (currentTime >= immortalTime)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}
		isImmortal = false;
	}


	// Use this for initialization
	void Start () {
		currentLives = defaultLives;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
