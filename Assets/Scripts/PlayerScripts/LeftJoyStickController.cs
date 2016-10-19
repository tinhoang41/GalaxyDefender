using UnityEngine;
using CnControls;
using System.Collections;

public class LeftJoyStickController : ShooterBase
{

    #region Publics
    public string horizontalAxisName;
    public string verticalAxisName;
    public float  joystickThreshold;
    #endregion

    #region Methods
    void Update()
    {
        // Left Stick Control
        CheckShooting();
    }

    #region Left stick controls
    void CheckShooting()
    {
        var directionVector = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);
        if (directionVector.magnitude < joystickThreshold || !pCanShoot) return;

        ShootBullet(directionVector);
    }
    #endregion

    #endregion
}