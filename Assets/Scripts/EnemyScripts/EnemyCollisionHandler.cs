using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : CollisionHandlerBase {

    protected int life;
    protected bool isImmortal;

    public int damageDealtWhenHit;
    public float ImmortalTime;

    public virtual void Start()
    {
        life       = GetComponent<EnemyData>().data.currentLevel;
        isImmortal = true;
        StartCoroutine("waitForImmortal");
    }

    private IEnumerator waitForImmortal()
    {
        yield return new WaitForSeconds(ImmortalTime);
        isImmortal = false;
    }

    protected override bool ShouldDestroySelf(Collider2D other)
    {
		life = IsValidCollision(other) ? life - damageDealtWhenHit : life;
        return life <= 0;
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
