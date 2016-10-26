using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleMover : SeekingMover {

    protected bool isTravelingVertical;
    protected Vector3[] anchorPoints;
    protected float xMin, xMax, yMin, yMax;
    protected bool  reachBoundary;
    protected int   currentIndex;
    public    float    waitTime;
    protected override bool pRunCoroutine
    {
        get
        {
            return true;
        }
    }

    protected override void SetUpCorouTineList()
    {
        base.SetUpCorouTineList();
        _coroutineList.Add("MoveTriangle");
    }

    protected override void Initialize()
    {
        base.Initialize();

        SetUpAnchorPoints();
        reachBoundary = false;
        steering      = GetComponent<SteeringBasic>();
        currentIndex  = 1;
        destination   = anchorPoints[currentIndex];
        hasTarget     = true;
        Debug.Log("Velocity: " + _velocity + " Speed : " + _currentSpeed + "Rotation: " + transform.rotation.eulerAngles);
    }

    protected void SetUpAnchorPoints()
    {
        isTravelingVertical = transform.rotation.eulerAngles.z == 0.0f || transform.rotation.eulerAngles.z == 180.0f;

        if (isTravelingVertical)
        {
            anchorPoints = new Vector3[] 
            {
                transform.position,
                new Vector3(transform.position.x, -transform.position.y, 0)
            };
            _velocity = transform.position.y > 0 ? new Vector3(0, -1, 0) : new Vector3(0, 1, 0);
        }
        else
        {
            anchorPoints = new Vector3[]
            {
                transform.position,
               new Vector3(-transform.position.x, transform.position.y, 0)
            };
            _velocity = transform.position.x > 0 ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
        }
    }

    protected override void FindTarget()
    {
        base.FindTarget();

        //if (!reachBoundary)
        //    return;

        destination = anchorPoints[++currentIndex % anchorPoints.Length];
        hasTarget   = true;
    }

    protected override void CheckForArrival()
    {
        base.CheckForArrival();

        if (hasTarget)
            return;

        if (isTravelingVertical)
            _velocity = transform.position.y > 0 ? new Vector3(0, -1, 0) : new Vector3(0, 1, 0);
        else
            _velocity = transform.position.x > 0 ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);

        _isRotating = true;
        reachBoundary = true;
    }

    IEnumerator MoveTriangle()
    {
        var runTime = GetComponentInParent<EnemyData>().data.currentLevel;
        while (true)
        {
            if (reachBoundary)
            {
                if (_isRotating)
                {
                    Rotate();
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(waitTime);
                    reachBoundary = false;
                    runTime--;
                }
            }
            else
            {

                Moving();
                //Rotate();
                yield return null;
            }
        }
    }


    protected override void ValidateVelocity()
    {
        base.ValidateVelocity();
        if (isTravelingVertical)
            _velocity.x = 0;
        else
            _velocity.y = 0;
    }
}
