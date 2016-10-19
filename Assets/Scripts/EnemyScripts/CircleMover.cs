using UnityEngine;
using System.Collections;

public class CircleMover : GameObjectMover {

    public Vector3 initialDirection;

    private Vector3 currentDirection;

    public bool pReachVerticalBoundary
    {
        get {
            return    transform.position.y <= _boundary.min.y
                   || transform.position.y >= _boundary.max.y;
        }
    }

    public bool pReachHorizontalBoundary
    {
        get
        {
            return    transform.position.x <= _boundary.min.x
                   || transform.position.x >= _boundary.max.x;
        }
    }

    public override void Start()
    {
        base.Start();
        _velocity = initialDirection;
    }

    protected override void UpdateVelocity()
    {
        _velocity = pReachVerticalBoundary ? Vector3.Scale(_velocity, new Vector3(1,-1,1)) : pReachHorizontalBoundary ? Vector3.Scale(_velocity, new Vector3(-1, 1, 1)) : _velocity;
    }

    public void Initialize(Vector3 direction)
    {
        initialDirection = direction;
    }
}
