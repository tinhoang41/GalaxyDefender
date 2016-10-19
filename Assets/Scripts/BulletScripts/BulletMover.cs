using UnityEngine;
using System.Collections;

public class BulletMover : GameObjectMover
{

    public float speed;
    // Use this for initialization
    public bool pReachVerticalBoundary
    {
        get
        {
            return transform.position.y <= _boundary.min.y
                   || transform.position.y >= _boundary.max.y;
        }
    }

    public bool pReachHorizontalBoundary
    {
        get
        {
            return transform.position.x <= _boundary.min.x
                   || transform.position.x >= _boundary.max.x;
        }
    }

    public override void Start()
    {
        base.Start();
        _velocity = transform.rotation * new Vector3(0, speed, 0);
    }

    protected override void UpdateRotationVariables()
    {
        if (!pReachVerticalBoundary && !pReachHorizontalBoundary)
            return;
        var normal = 0.0f;

        if (pReachVerticalBoundary)
            normal = transform.position.y > 0 ? 0 : 180.0f;
        else if (pReachHorizontalBoundary)
            normal = transform.position.x > 0 ? 270.0f : 90.0f;
        
        var currentVelocity = GetComponent<Rigidbody2D>().velocity.normalized;
        var currentAngle = transform.rotation.eulerAngles;
        var newAngle = 2 * normal - 180.0f - currentAngle.z;
        var newRotation = Quaternion.Euler(0, 0, newAngle);

        transform.rotation = newRotation;
    }

    protected override void UpdateVelocity()
    {
        _velocity = transform.rotation * Vector3.up;//pReachVerticalBoundary ? Vector3.Scale(_velocity, new Vector3(1, -1, 1)) : pReachHorizontalBoundary ? Vector3.Scale(_velocity, new Vector3(-1, 1, 1)) : _velocity;
    }

}
