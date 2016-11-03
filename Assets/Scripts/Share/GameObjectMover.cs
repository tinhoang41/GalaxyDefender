using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectMover : MonoBehaviour {

    #region Fields
    public      float       maxAcceleration;
    public      float       minAcceleration;

    public      float       minSpeed;
    public      float       maxSpeed;

    public      float       rotatingSpeed;

    protected   bool        _isRotating;
    protected   Quaternion  _directionToRotate;
    protected   float       _rotationAngle;
    protected   float       _currentSpeed;
    protected   Bounds      _boundary;
    protected   Vector3     _velocity;
    protected   float       _currentAcceleration;
    protected   float       _initialSpeed;

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
        _boundary            = SetUpBounds();
        _velocity            = transform.rotation * Vector3.up;
        _directionToRotate   = transform.rotation;
        _rotationAngle       = 0.0f;
        _isRotating          = false;

        if (gameObject.tag == "Enemy")
        {
            SetUpVariablesBasedOnData();
        }
        else
        {
            _currentSpeed = _initialSpeed = minSpeed;
            _currentAcceleration = 0.0f;
        }
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
        _currentAcceleration = Mathf.Clamp(_currentAcceleration, 0, maxAcceleration);
        _currentSpeed   = Mathf.Clamp(_currentSpeed + _currentAcceleration * Time.deltaTime, 0, maxSpeed);
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

    protected virtual void UpdatePosition()
    {
        ValidateVelocity();
        transform.position += _velocity * _currentSpeed * Time.deltaTime;
    }

    protected virtual void UpdateRotation()
    {
        if (_isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _directionToRotate, rotatingSpeed * Time.time);
            _isRotating = (transform.rotation.eulerAngles - _directionToRotate.eulerAngles).magnitude >= 0.05f;
        }
    }

    #endregion

    #region Utilities
    protected virtual void SetUpVariablesBasedOnData()
    {
        var enemyData        = GetComponent<EnemyData>();
        var maxLevel         = enemyData.pData.maxLevel;
        var minLevel         = enemyData.pData.minLevel;
        var currentLevel     = enemyData.pData.currentLevel;
        var fraction         = currentLevel / (float)(maxLevel - minLevel);

        _initialSpeed        = GetFractionalValue(minSpeed, maxSpeed, fraction);
        _currentAcceleration = GetFractionalValue(minAcceleration, maxAcceleration, fraction);
        _currentSpeed        = _initialSpeed;

    }

    float GetFractionalValue(float min, float max, float fraction)
    {
        return min + (fraction * (max - min));
    }

    protected virtual void LimitPosition()
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

    protected virtual void ValidateVelocity()
    {
        _velocity.z = 0;
        _velocity.Normalize();
    }
    #endregion

    #endregion
}
