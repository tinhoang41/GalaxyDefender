using UnityEngine;
using System.Collections;

public class SeekingMover : GameObjectMover {

    protected Vector3       destination;
    protected bool          hasTarget;
    protected SteeringBasic steering;

    private const float ACCEPTABLE_VALUE = 0.5f;

    protected override bool pRotationByVelocity
    {
        get { return true; }
    }

    protected override void Initialize()
    {
        base.Initialize();
        SetDestinations();
        hasTarget   = false;
        steering    = GetComponent<SteeringBasic>();
    }

    protected virtual void FindTarget()
    {
        _currentSpeed = initialSpeed;
    }

    protected virtual void UpdateTarget() { }

    protected override void UpdateVelocity()
    {
        UpdateTarget();

        if (steering != null)
        {
            var steeringVector = steering.GetSteering(_velocity, destination);
            _velocity         += steeringVector;
            _currentSpeed      = steering.GetSpeed(destination, _currentSpeed);
        }
        else
            _velocity = (destination - transform.position).normalized;

        base.UpdateVelocity();
    }

    protected virtual void CheckForArrival()
    {
        var diff = transform.position - destination;
        hasTarget = (transform.position - destination).magnitude > ACCEPTABLE_VALUE ||  _currentSpeed > ACCEPTABLE_VALUE;
        if (!hasTarget)
            Debug.Log("Reach Target");
    }

    protected override void Moving()
    {
        if (!hasTarget)
            FindTarget();
        else
        {
            base.Moving();
            CheckForArrival();
        }
    }

    protected virtual void SetDestinations()
    {
        destination = transform.position;
    }

}
