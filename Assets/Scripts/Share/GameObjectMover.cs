using UnityEngine;
using System.Collections;


public class GameObjectMover : MonoBehaviour {

    public      float       acceleration;
    public      float       initialSpeed;
    public      float       maxSpeed;
    public      float       rotatingSpeed;

    protected   bool        _isRotating;
    protected   Quaternion  _directionToRotate;
    protected   float       _rotationAngle;
    protected   float       _currentSpeed;
    protected   Bounds      _boundary;
    protected   Vector3     _velocity;
    // Use this for initialization

    public virtual void Start()
    {
		_currentSpeed      = initialSpeed;
        _boundary          = SetUpBounds();
        _velocity          = Vector3.zero;
        _directionToRotate = transform.rotation;
        _rotationAngle     = 0.0f;
        _isRotating        = false;
    }

    public virtual void Update()
    {
        Moving();
        Steering();
    }

    #region Translation related updates
    protected virtual void UpdateVelocity()
	{
		_currentSpeed = Mathf.Clamp(_currentSpeed + acceleration * Time.deltaTime, 0, maxSpeed);
		_velocity.Normalize();
	}

    protected virtual void Moving()
    {
        UpdateVelocity();
        transform.position += _velocity * _currentSpeed * Time.deltaTime;
        LimitPosition();
    }
    #endregion

    #region Rotation related updates
    protected virtual void Steering()
    {
        UpdateRotationVariables();
        if (_isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _directionToRotate, rotatingSpeed * Time.time);
            _isRotating        = (transform.rotation.eulerAngles - _directionToRotate.eulerAngles).magnitude >= 0.005f;
        }
    }

    protected virtual void UpdateRotationVariables()
    {
        _rotationAngle     = 0;
        _directionToRotate = transform.rotation;
        _isRotating        = false;
    }
    #endregion

    #region Utilities
    void LimitPosition()
    {
        Vector3 currentPosition = transform.position;
        var xPos = Mathf.Clamp(currentPosition.x, _boundary.min.x, _boundary.max.x);
        var yPos = Mathf.Clamp(currentPosition.y, _boundary.min.y, _boundary.max.y);

        transform.position = new Vector3(xPos, yPos, currentPosition.z);
    }

    Bounds SetUpBounds()
    {
        var retVal = new Bounds(Vector3.zero, Vector3.zero);
        try {
             retVal = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>().bounds;
        }
        catch
        {
            Debug.Log("Can't find boundary");
        }

        return retVal;
    }
    #endregion
}
