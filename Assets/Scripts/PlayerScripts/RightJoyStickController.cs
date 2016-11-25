using UnityEngine;
using System.Collections;
using CnControls;

public class RightJoyStickController : GameObjectMover, MyJoystickController
{

    public float  joystickThreshold;
    public string horizontalAxisName;
    public string verticalAxisName;
    public float  initialAcceleration;

    protected SteeringBasic steering;

    protected override bool pRotationByVelocity
    {
        get { return true; }
    }

    public bool pIsControlling
    {
        get;
        set;
    }

    protected override void Initialize()
    {
        base.Initialize();

        pIsControlling       = false;
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
            pIsControlling = false;
            _currentSpeed = 0;
        }
        else
        {


            if (!pIsControlling)
            {
                _currentSpeed = _initialSpeed;
                _velocity = desiredVelocity;
            }
            else
            {
                var steeringVector = steering.GetSteeringByTwoVelocity(_velocity, desiredVelocity);
                _velocity += steeringVector;
            }
            pIsControlling = true;
        }

        base.UpdateVelocity();
    }

}
