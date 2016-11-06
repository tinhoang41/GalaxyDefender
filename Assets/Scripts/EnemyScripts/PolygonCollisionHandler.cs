using UnityEngine;
using System.Collections;

public class PolygonCollisionHandler : EnemyCollisionHandler {

    private float CHILD_MULTIPLIER = 4.0f;
    private float MAX_CHILD = 20.0f;
    private float MIN_CHILD = 8.0f;
    private int   childNumber;

    private int childLevel;

    public override void Start()
    {
        base.Start();
        var currentLevel = GetComponent<EnemyData>().pData.currentLevel;
        childLevel       = Mathf.Max(currentLevel / 2, 1);
        childNumber      = (int)Mathf.Clamp(CHILD_MULTIPLIER * currentLevel, MIN_CHILD, MAX_CHILD);
    }

    protected override void ActionAfterDestroySelf(Collider2D other)
    {
        base.ActionAfterDestroySelf(other);
        SpawnChild();
    }

    protected override bool ShouldDestroySelf(Collider2D other)
    {
        return base.ShouldDestroySelf(other);
    }
    private void SpawnChild()
    {
        var degreeInc = 360.0f / childNumber;

        for (int i = 0; i < childNumber; i++)
        {
            var degreeToRotate = degreeInc * i;
            var rotation = Quaternion.Euler(0 , 0, -degreeToRotate);//.Euler(0.0f, 0.0f, -degreeToRotate);
            var velocity = (Quaternion.Euler(0, 0, degreeToRotate) * Vector2.right);
            InitChild(transform.position, velocity, rotation);
        }
    }

    private void InitChild(Vector3 position, Vector3 velocity, Quaternion rotation)
    {

        EnemyDataParameter circleData = GetComponent<EnemyData>().GetData();
        circleData.currentLevel = childLevel;
        EnemyGenerator.GenerateCircle(circleData, position, velocity, rotation);
    }
}
