using UnityEngine;
using System.Collections;

public enum ImmortalType :ushort
{
	RECOVERING = 0,
	ITEM = 1,
	SPAWNING = 2,
}

public class ActorData : MonoBehaviour {
	
	protected int currentLives;
	protected float immortalTime;
	// become immortal when got hit or get items
	protected bool isImmortal;

	public virtual void ApplyDamage(int damage)
	{
		if (isImmortal)
			return;

		currentLives -= damage;
		currentLives = Mathf.Max (currentLives, 0);
	}

	IEnumerator RunImmortality(ImmortalType immortalType)
	{
		SwitchColor (immortalType);
		yield return new WaitForSeconds (immortalTime);
		isImmortal = false;
	}

	protected virtual void SwitchColor(ImmortalType immortalType){ }
	protected virtual void Initialize() { }
	protected virtual void RunCoroutines(){ }

	void Start () {
		Initialize ();
		RunCoroutines ();
	}
}
