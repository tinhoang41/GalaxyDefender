using UnityEngine;
using System.Collections;
using CnControls;

public class RightJoyStickController : GameObjectMover {

    public float  joystickThreshold;
    public string horizontalAxisName;
    public string verticalAxisName;
    public float  initialAcceleration;

    protected SteeringBasic steering;
    protected bool isControlling;

    protected override bool pRotationByVelocity
    {
        get { return true; }
    }

    protected override void Initialize()
    {
        base.Initialize();

        isControlling = false;
        steering             = GetComponent<SteeringBasic>();
        _rotationAngle       = 0.0f;
        _isRotating          = false;
        _directionToRotate   = transform.rotation;
        _currentAcceleration = initialAcceleration;
    }


    protected override void UpdateVelocity()
    {
		if (GetComponent<PlayerData> ().pIsDead)
			return;
		
        var desiredVelocity = new Vector3(CnInputManager.GetAxis(horizontalAxisName), CnInputManager.GetAxis(verticalAxisName), 0.0f);

        if (desiredVelocity.magnitude < joystickThreshold)
        {
            isControlling = false;
            _currentSpeed = 0;
        }
        else
        {


            if (!isControlling)
            {
                _currentSpeed = _initialSpeed;
                _velocity = desiredVelocity;
            }
            else
            {
                var steeringVector = steering.GetSteeringByTwoVelocity(_velocity, desiredVelocity);
                _velocity += steeringVector;
            }
            isControlling = true;
        }

        base.UpdateVelocity();
    }

}
