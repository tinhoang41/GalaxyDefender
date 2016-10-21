using UnityEngine;
using System.Collections;

public class SteeringBasic : MonoBehaviour {

    public float arrivalRadius = 0.5f;
    public float steeringSpeed = 20.0f;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public Vector3 GetSteering(Vector3 currentVelocity, Vector3 destination)
    {
        var retVal          = Vector3.zero;
        var desiredVelocity = destination - transform.position;
        desiredVelocity.z   = 0;
        desiredVelocity.Normalize();
        retVal              = desiredVelocity - currentVelocity;
        return retVal /= steeringSpeed;
    }

    public float GetSpeed(Vector3 destination, float currentSpeed)
    {
        var desiredVelocy = destination - transform.position;
        desiredVelocy.z   = 0;
        var distance      = desiredVelocy.magnitude;

        return distance >= arrivalRadius ? currentSpeed : currentSpeed * Mathf.Clamp(distance / arrivalRadius, 0, 1);
    }
}
