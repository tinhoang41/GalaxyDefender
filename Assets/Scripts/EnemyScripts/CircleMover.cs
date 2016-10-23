using UnityEngine;
using System.Collections;

public class CircleMover : GameObjectMover {

    private Vector3 initialDirection;

    private Vector3 currentDirection;


    protected override void Initialize()
    {
        base.Initialize();
        _velocity = initialDirection;
    }

    protected override void UpdateVelocity()
    {
        _velocity = pReachVerticalBoundary ? Vector3.Scale(_velocity, new Vector3(1,-1,1)) : pReachHorizontalBoundary ? Vector3.Scale(_velocity, new Vector3(-1, 1, 1)) : _velocity;
        base.UpdateVelocity ();
    }

    public void Initialize(Vector3 direction)
    {
        initialDirection = direction;
    }
}
