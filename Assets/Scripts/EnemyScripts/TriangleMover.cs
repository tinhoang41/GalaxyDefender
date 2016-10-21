using UnityEngine;
using System.Collections;

public class TriangleMover : SeekingMover {

    private GameObject player;

    public float distanceThreshold = 5.0f;

    protected override bool pRotationByVelocity
    {
        get { return true;  }
    }

    protected override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void UpdateTarget()
    {
        destination = player.transform.position;
    }

    protected override void FindTarget()
    {
        if((player.transform.position - transform.position).magnitude >= distanceThreshold)
        {
            hasTarget = true;
            destination = player.transform.position;
        }

        base.FindTarget();
    }

}
