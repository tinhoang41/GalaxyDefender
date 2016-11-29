using UnityEngine;
using System.Collections;

public interface MyJoystickController
{
    bool pIsControlling
    {
        get;
        set;
    }
}
public class PlayerData : ActorData
{

    public float immortalTimeForRecovering;
    public float immortalTimeFromItem;
    public int   defaultLives;
    
    public bool pIsDead
    {
        get { return isDead; }
    }
    protected bool isDead;
    protected PlayerColorChanger colorChanger;

    public override void ApplyDamage(int damage)
    {
        if (isImmortal)
            return;
        
        base.ApplyDamage (damage);
        isDead = currentLives <= 0;
        GlobalControl.Instance.pHUD.GetComponent<HUDManager>().UpdateLiveText(currentLives);
        if (!isDead) 
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
        isDead       = false;
        currentLives = defaultLives;
        colorChanger = GetComponent<PlayerColorChanger> ();
        GlobalControl.Instance.pHUD.GetComponent<HUDManager>().UpdateLiveText(currentLives);

    }

}
