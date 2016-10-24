using UnityEngine;
using System.Collections;

public class BulletMover : GameObjectMover
{

    // Use this for initialization


    protected override bool pRotationByVelocity
    {
        get { return true; }
    }

    protected override void Initialize()
    {
        base.Initialize();
        _velocity = transform.rotation * new Vector3(0, _initialSpeed, 0);
    }

    protected override void UpdateVelocity()
    {
        _velocity = pReachVerticalBoundary ? Vector3.Scale(_velocity, new Vector3(1, -1, 1)) : pReachHorizontalBoundary ? Vector3.Scale(_velocity, new Vector3(-1, 1, 1)) : _velocity;
        base.UpdateVelocity();
    }

}
