﻿using UnityEngine;
using CnControls;
using System.Collections;

public class LeftJoyStickController : ShooterBase
{

    #region Publics
    public string horizontalAxisName;
    public string verticalAxisName;
    public float  joystickThreshold;

    public int numberSprayPerRound;
    public int numberOfRound;
    #endregion
    private bool isSpraying;
    #region Methods
    void Update()
    {
        // Left Stick Control
        CheckShooting();

        if (Input.GetKeyDown(KeyCode.Space) && !isSpraying)
            StartCoroutine("Spray");

    }

    #region Left stick controls
    void CheckShooting()
    {
        var directionVector = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);
        if (directionVector.magnitude < joystickThreshold || !pCanShoot) return;

        ShootBullet(directionVector);
    }
    #endregion

    IEnumerator Spray()
    {
        var degreeInc = 360.0f / numberSprayPerRound;
        var diff = degreeInc;
        isSpraying = true;
        while(numberOfRound >= 0)
        {
            for (int i = 0; i < numberSprayPerRound; i++)
            {
                var degreeToRotate = (degreeInc * i ) + (numberOfRound % 2) * diff;
                var rotation = Quaternion.identity;//.Euler(0.0f, 0.0f, -degreeToRotate);
                var velocity = (Quaternion.Euler(0, 0, degreeToRotate) * Vector2.right);
                ShootBullet(velocity);
            }
            numberOfRound--;
            yield return new WaitForSeconds(fireRate);
        }
        isSpraying = false;
    }
    #endregion
}