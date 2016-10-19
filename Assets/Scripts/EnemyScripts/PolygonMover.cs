using UnityEngine;
using System.Collections;

public class PolygonMover : GameObjectMover {

    public  Vector3     boundSize;
    public  float       waitTime;
	public 	float 		arrivalRadius;
    
    private float       xMinForCenter;
    private float       xMaxForCenter;
    private float       yMinForCenter;
    private float       yMaxForCenter;
	private Vector3     destination;
	private Bounds      movementBound;
    private bool        hasArrived;

    public override void Start ()
    {
        base.Start();

        var size = GetComponentInChildren<SpriteRenderer>().bounds.size.magnitude;
        xMinForCenter = _boundary.min.x + (boundSize.x / 2) + (float)size/2;
        xMaxForCenter = _boundary.max.x - (boundSize.x / 2) - (float)size/2;
        
        yMinForCenter = _boundary.min.y + (boundSize.y / 2) + (float)size/2;
        yMaxForCenter = _boundary.max.y - (boundSize.y / 2) - (float)size/2;

        hasArrived    = false;

        GetNewDestinationPoint();
        StartCoroutine("MovePolygon");
    }

    public override void Update(){ }

    IEnumerator MovePolygon()
    {
        while(true)
        {
            if (hasArrived)
            {
                yield return new WaitForSeconds(waitTime);
                _velocity = Vector3.zero;
                GetNewDestinationPoint();
            }
            else
            {
                Moving();
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

    void GetNewDestinationPoint()
    {
        UpdateMovementAreas();
        destination = new Vector3
            (
                Random.Range(movementBound.min.x, movementBound.max.x),
                Random.Range(movementBound.min.y, movementBound.max.y),
                transform.position.z
            );
        _velocity  = destination - transform.position;
		_velocity.Normalize ();
		_currentSpeed = initialSpeed;
        hasArrived = false;
    }

	protected override void Moving ()
	{
		base.Moving ();
		hasArrived = _currentSpeed <= 0.005f;
	}

	protected override void UpdateVelocity ()
	{
		var desired = destination - transform.position;
		var length = desired.magnitude;
		_currentSpeed = length < arrivalRadius ? _currentSpeed * length / arrivalRadius : _currentSpeed;
		_velocity.Normalize ();
	}
}
