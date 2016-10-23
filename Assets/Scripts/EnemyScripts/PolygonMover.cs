using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonMover : SeekingMover {

    public  Vector3     boundSize;
    public  float       waitTime;
    
    private float       xMinForCenter;
    private float       xMaxForCenter;
    private float       yMinForCenter;
    private float       yMaxForCenter;
    private Bounds      movementBound;
    private bool        isWaiting;

    protected override bool pRunCoroutine
    {
        get { return true; }
    }

    protected override void Initialize()
    {
        base.Initialize();
        var size = GetComponentInChildren<SpriteRenderer>().bounds.size.magnitude;
        xMinForCenter = _boundary.min.x + (boundSize.x / 2) + (float)size / 2;
        xMaxForCenter = _boundary.max.x - (boundSize.x / 2) - (float)size / 2;

        yMinForCenter = _boundary.min.y + (boundSize.y / 2) + (float)size / 2;
        yMaxForCenter = _boundary.max.y - (boundSize.y / 2) - (float)size / 2;

        isWaiting = false;
    }

    protected override void SetUpCorouTineList()
    {
        _coroutineList = new List<string>
        {
            "MovePolygon"
        };
    }

    public override void Update(){ }

    IEnumerator MovePolygon()
    {
        Debug.Log("Start polygon coroutine");
        while(true)
        {
            if (isWaiting)
            {
                yield return new WaitForSeconds(waitTime);
                isWaiting = false;
            }
            else
            {
                Moving();
                Rotate();
                yield return null;
            }
        }
    }

    void UpdateMovementAreas()
    {
        Vector3 position = new Vector3
        (
            Mathf.Clamp(transform.position.x, xMinForCenter, xMaxForCenter),
            Mathf.Clamp(transform.position.y, yMinForCenter, yMaxForCenter),
            transform.position.z
        );

        movementBound = new Bounds(position, boundSize);
    }

    protected override void FindTarget()
    {
        UpdateMovementAreas();
        destination = new Vector3
            (
                Random.Range(movementBound.min.x, movementBound.max.x),
                Random.Range(movementBound.min.y, movementBound.max.y),
                transform.position.z
            );

        _currentSpeed = _initialSpeed;
        hasTarget     = true;
    }

    protected override void CheckForArrival()
    { 
        base.CheckForArrival();
        isWaiting = !hasTarget;
    }

}
