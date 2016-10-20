using UnityEngine;
using System.Collections;

public class BulletMover : GameObjectMover
{

    public float speed;
    // Use this for initialization


    protected override bool pRotationByVelocity
    {
        get
        {
            return true;
        }
    }

    public override void Start()
    {
        base.Start();
        _velocity = transform.rotation * new Vector3(0, speed, 0);
    }

    protected override void UpdateVelocity()
    {
        _velocity = pReachVerticalBoundary ? Vector3.Scale(_velocity, new Vector3(1, -1, 1)) : pReachHorizontalBoundary ? Vector3.Scale(_velocity, new Vector3(-1, 1, 1)) : _velocity;
        base.UpdateVelocity();
    }

}
