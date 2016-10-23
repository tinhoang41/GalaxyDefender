using UnityEngine;
using System.Collections;

public class SteeringBasic : MonoBehaviour {

    public float arrivalRadius;
    public float steeringSpeed;

    private float slowingDownSpeed;
    // Use this for initialization
    void Start () {
        slowingDownSpeed = 0.0f;
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
        retVal.z            = 0;

        return retVal /= steeringSpeed;
    }

    public float GetSpeed(Vector3 destination, float currentSpeed)
    {
        var retVal        = currentSpeed;
        var desiredVelocy = destination - transform.position;
        desiredVelocy.z   = 0;
        var distance      = desiredVelocy.magnitude;

        if (distance < arrivalRadius)
            retVal = slowingDownSpeed * Mathf.Clamp(distance / arrivalRadius, 0, 1);
        else
            slowingDownSpeed = currentSpeed;

        return retVal;
    }
}
