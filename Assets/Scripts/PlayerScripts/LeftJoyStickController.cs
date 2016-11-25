using UnityEngine;
using CnControls;
using System.Collections;
using System;

public class LeftJoyStickController : ShooterBase, MyJoystickController
{

    #region Publics
    public string horizontalAxisName;
    public string verticalAxisName;
    public float  joystickThreshold;

    public int numberSprayPerRound;
    public int numberOfRound;
    protected PlayerData playerData;

    #endregion
    private bool isSpraying;

    public bool pIsControlling
    {
        get;
        set;
    }
    #region Methods

    void Update()
    {
        if (GetComponent<PlayerData> ().pIsDead)
            return;
        
        // Left Stick Control
        CheckShooting();
        if (Input.GetKeyUp(KeyCode.Space) && !isSpraying) {
            StartCoroutine("Spray");
        }


    }

    #region Left stick controls
    void CheckShooting()
    {
        var directionVector = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);
        pIsControlling = directionVector.magnitude >= joystickThreshold;
        if(!pCanShoot || !pIsControlling)
            return;
        ShootBullet(directionVector);
    }
    #endregion

    IEnumerator Spray()
    {
        var degreeInc = 360.0f / numberSprayPerRound;
        var diff = degreeInc;
        var num = numberOfRound;
        isSpraying = true;
        while(num >= 0)
        {
            for (int i = 0; i < numberSprayPerRound; i++)
            {
                var degreeToRotate = (degreeInc * i ) + (numberOfRound % 2) * diff;
                var rotation = Quaternion.identity;//.Euler(0.0f, 0.0f, -degreeToRotate);
                var velocity = (Quaternion.Euler(0, 0, degreeToRotate) * Vector2.right);
                ShootBullet(velocity);
            }
            num--;
            yield return new WaitForSeconds(fireRate);
        }
        isSpraying = false;
    }

    IEnumerator Splash()
    {
        var degreeInc = 360.0f / numberSprayPerRound;
        var diff = degreeInc;
        var num = numberOfRound;
        isSpraying = true;
        while (num >= 0)
        {
            for (int i = 0; i < numberSprayPerRound; i++)
            {
                var degreeToRotate = (degreeInc * i) + (numberOfRound % 2) * diff;
                var rotation = Quaternion.identity;//.Euler(0.0f, 0.0f, -degreeToRotate);
                var velocity = (Quaternion.Euler(0, 0, degreeToRotate) * Vector2.right);
                ShootBullet(velocity);
                yield return new WaitForSeconds(0.005f);
            }
            num--;
            yield return new WaitForSeconds(fireRate);
        }
        isSpraying = false;
    }
    #endregion
}