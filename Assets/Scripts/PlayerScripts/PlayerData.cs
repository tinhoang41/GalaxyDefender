using UnityEngine;
using System.Collections;


public class PlayerData : ActorData {

	public float immortalTimeForRecovering;
	public float immortalTimeFromItem;
	public int   defaultLives;

	protected PlayerColorChanger colorChanger;

	public override void ApplyDamage(int damage)
	{
		if (isImmortal)
			return;
		
		base.ApplyDamage (damage);

		if (currentLives > 0) 
		{
			isImmortal = true;
			immortalTime = immortalTimeForRecovering;
			StartCoroutine ("RunImmortality", ImmortalType.RECOVERING);
		}
	}

	protected override void SwitchColor (ImmortalType immortalType)
	{
		colorChanger.ChangePlayerColor (immortalType, immortalTime);
	}

	protected override void Initialize ()
	{
		base.Initialize ();
		currentLives = defaultLives;
		colorChanger = GetComponent<PlayerColorChanger> ();
	}

}
