using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : CollisionHandlerBase {

    protected EnemyData enemyData;
    protected EnemyColorChanger colorChanger;
    public int damageDealtWhenHit;

    protected override void GetObjectData ()
    {
        enemyData = GetComponent<EnemyData> ();
    }

    public override void Start()
    {
        base.Start ();
        enemyData    = GetComponent<EnemyData>();
        colorChanger = GetComponent<EnemyColorChanger>();
    }


    protected override bool ShouldDestroySelf(Collider2D other)
    {
        if (IsValidCollision(other))
        {
            enemyData.ApplyDamage(damageDealtWhenHit);
            if(colorChanger != null)
             colorChanger.ChangeColor();

        }
        return enemyData.pCurrentLives <= 0;
    }

    protected override void ActionAfterDestroySelf(Collider2D other)
    {
        base.ActionAfterDestroySelf(other);
        GlobalControl.Instance.UpdateScore(enemyData.EnemyScore);
    }
    protected override bool ShouldDestroyOther(Collider2D other)
    {
        return other.tag == "Bullet";
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
            return;

        base.OnTriggerEnter2D(other);
    }

    protected virtual bool IsValidCollision(Collider2D other)
    {
        return !enemyData.pIsImmortal && (other.tag == "Bullet" || other.tag == "Player");
    }
}
