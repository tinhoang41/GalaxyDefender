using UnityEngine;
using System.Collections;
using CnControls;

public class RightJoyStickController : GameObjectMover {

    public float  joystickThreshold;
    public string horizontalAxisName;
    public string verticalAxisName;

    protected override bool pRotationByVelocity
    {
        get { return true; }
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();

        _rotationAngle = 0.0f;
        _isRotating        = false;
        _directionToRotate = transform.rotation;
    }

    protected override void UpdateRotationVariables()
    {
        var movementVector = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);
        if (movementVector.magnitude < joystickThreshold) return;

        _rotationAngle     = Mathf.Rad2Deg * Mathf.Atan2(movementVector.x, movementVector.y);
        _directionToRotate = Quaternion.Euler(0.0f, 0.0f, -_rotationAngle);
        _isRotating        = true;
    }

    protected override void UpdateVelocity()
    {
        _velocity = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);

        if (_velocity.magnitude < joystickThreshold)
            _currentSpeed = 0;
        else
            _currentSpeed = initialSpeed;

        base.UpdateVelocity();
    }
}
