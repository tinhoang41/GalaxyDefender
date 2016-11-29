using UnityEngine;
using System.Collections;

public class DiamondMover : SeekingMover {


    public float distanceThreshold = 5.0f;

    protected override bool pRotationByVelocity
    {
        get { return true;  }
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void UpdateTarget()
    {
        destination = GlobalControl.Instance.pPlayer.transform.position;
    }

    protected override void FindTarget()
    {
        if((GlobalControl.Instance.pPlayer.transform.position - transform.position).magnitude >= distanceThreshold)
        {
            hasTarget     = true;
            _currentSpeed = _initialSpeed;
            destination   = GlobalControl.Instance.pPlayer.transform.position;
        }

        base.FindTarget();
    }

}
