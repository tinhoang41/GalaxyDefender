using UnityEngine;
using System.Collections;

public class TriangleMover : GameObjectMover {


    private Vector3 destination;
    private bool    hasArrived;
    private Vector3 steering;

    private GameObject player;
    public float mass;
    protected override bool pRotationByVelocity
    {
        get
        {
            return true;
        }
    }
    // Use this for initialization
    public override void Start () {
        destination = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        hasArrived = true;
        _currentSpeed = 0.0f;
        base.Start();

    }
    
    // Update is called once per frame
    public override void Update () {

        if ( player != null && (player.transform.position - transform.position).magnitude >= 10.0f)
        {
            destination = player.transform.position;
            hasArrived = false;
            _currentSpeed = initialSpeed;
        }

        base.Update();
    }

    protected override void Moving()
    {
        hasArrived = _currentSpeed <= 0.5f;
        if (!hasArrived)
            base.Moving();
    }

    protected override void UpdateVelocity()
    {
        destination = player.transform.position;
        var desired = destination - transform.position;
        var length = desired.magnitude;
		_currentSpeed = length < 1.0f ? (_currentSpeed + acceleration * Time.deltaTime) * length / 1.0f : _currentSpeed + acceleration * Time.deltaTime;
        UpdateSeekingVelocity();
        _velocity.Normalize();
    }

    protected void UpdateSeekingVelocity()
    {
        var desiredVelocity = destination - transform.position;
        var velocityTemp = _velocity * _currentSpeed;

        desiredVelocity.Normalize();
        steering = desiredVelocity - _velocity;
        steering /= mass;

        _velocity += steering;
    }
}
