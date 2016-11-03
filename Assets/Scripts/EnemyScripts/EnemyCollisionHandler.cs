using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : CollisionHandlerBase {

    protected bool isImmortal;
    protected EnemyData enemyData;
    protected ColorChanger colorChanger;
    public int damageDealtWhenHit;
    public float ImmortalTime;

    public virtual void Start()
    {
        enemyData    = GetComponent<EnemyData>();
        colorChanger = GetComponent<ColorChanger>();
        isImmortal   = true;
        StartCoroutine("waitForImmortal");
    }

    private IEnumerator waitForImmortal()
    {
        yield return new WaitForSeconds(ImmortalTime);
        isImmortal = false;
    }

    protected override bool ShouldDestroySelf(Collider2D other)
    {
        if (IsValidCollision(other))
        {
            enemyData.AddDamage(damageDealtWhenHit);
            if(colorChanger != null)
             colorChanger.ChangeColor();

        }
        return enemyData.pCurrentLife <= 0;
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
        return !isImmortal && (other.tag == "Bullet" || other.tag == "Player");
    }
}
