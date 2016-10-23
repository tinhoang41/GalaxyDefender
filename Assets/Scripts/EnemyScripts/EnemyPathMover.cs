using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathMover : SeekingMover {

    public Vector3[] destinationsList;
    public int       repeating;
    public float     waitTime;
    protected int    currentIndex;

    protected override bool pRunCoroutine
    {
        get { return true; }
    }

    protected override void SetUpCorouTineList()
    {
        base.SetUpCorouTineList();
        _coroutineList.Add("FollowPath");
    }

    protected override void SetDestinations()
    {
        base.SetDestinations();
        currentIndex     = 0;
    }

    protected override void FindTarget()
    {
        base.FindTarget();

        if (currentIndex > destinationsList.Length)
            return;

        Debug.Log("Find " + currentIndex);
        hasTarget   = true;
        destination = destinationsList[currentIndex];

    }

    protected override void CheckForArrival()
    {
        base.CheckForArrival();
        
        currentIndex = !hasTarget ? ++currentIndex : currentIndex;
    }

    IEnumerator FollowPath()
    {
        while(repeating > 0)
        {
            currentIndex = 0;
            while (currentIndex < destinationsList.Length)
            {
                Moving();
                Rotate();
                yield return null;
            }
            repeating--;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
