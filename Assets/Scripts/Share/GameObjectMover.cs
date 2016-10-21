using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectMover : MonoBehaviour {

    #region Fields
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

    protected  List<string> _coroutineList;

    #endregion

    #region Properties
    protected virtual bool pRotationByVelocity
    {
        get { return false; }
    }

    protected bool pReachVerticalBoundary
    {
        get
        {
            return transform.position.y <= _boundary.min.y
                   || transform.position.y >= _boundary.max.y;
        }
    }

    protected bool pReachHorizontalBoundary
    {
        get
        {
            return transform.position.x <= _boundary.min.x
                   || transform.position.x >= _boundary.max.x;
        }
    }

    protected virtual bool pRunCoroutine
    {
        get { return false; }
    }
    #endregion

    #region Methods
    public virtual void Update()
    {
        if (!pRunCoroutine)
        {
            Moving();
            Rotate();
        }
    }

    #region Intializes
    protected virtual void Initialize()
    {
        _currentSpeed      = initialSpeed;
        _boundary          = SetUpBounds();
        _velocity          = transform.rotation * Vector3.up;
        _directionToRotate = transform.rotation;
        _rotationAngle     = 0.0f;
        _isRotating        = false;
    }

    public virtual void Start()
    {
        Initialize();
        RunCoroutineList();
    }
    #endregion

    #region Translation related updates
    protected virtual void UpdateVelocity()
    {
        _velocity.Normalize();
        _currentSpeed = Mathf.Clamp(_currentSpeed + acceleration * Time.deltaTime, 0, maxSpeed);
    }

    protected virtual void Moving()
    {
        UpdateVelocity();
        UpdatePosition();
        LimitPosition();
    }
    #endregion

    #region Rotation related updates
    protected virtual void Rotate()
    {
        UpdateRotationVariables();
        UpdateRotation();
    }

    protected virtual void UpdateRotationVariables()
    {
        if (pRotationByVelocity)
        {
            _rotationAngle     = GetRotationAngleFromVector(_velocity);
            _directionToRotate = Quaternion.Euler(0, 0, -_rotationAngle);
            _isRotating        = -_rotationAngle != transform.rotation.eulerAngles.z;
        }
        else
        {
            _rotationAngle     = 0;
            _directionToRotate = transform.rotation;
            _isRotating        = false;
        }
    }
    #endregion

    #region Steering Related Behaviour

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
        try
        {
             retVal = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>().bounds;
        }
        catch
        {
            Debug.Log("Can't find boundary");
        }

        return retVal;
    }

    protected float GetRotationAngleFromVector(Vector3 direction)
    {
        direction.Normalize();
        return Mathf.Rad2Deg* Mathf.Atan2(direction.x, direction.y);
    }

    protected virtual void UpdatePosition()
    {
        transform.position += _velocity * _currentSpeed * Time.deltaTime;
    }

    protected virtual void UpdateRotation()
    {
        if (_isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _directionToRotate, rotatingSpeed * Time.time);
            _isRotating = (transform.rotation.eulerAngles - _directionToRotate.eulerAngles).magnitude >= 0.005f;
        }
    }

    protected virtual void SetUpCorouTineList()
    {
        _coroutineList = new List<string>();
    }

    protected virtual void RunCoroutineList()
    {
        if (!pRunCoroutine)
            return;

        SetUpCorouTineList();
        foreach (var coroutin in _coroutineList)
            StartCoroutine(coroutin);
    }
    #endregion

    #endregion
}
