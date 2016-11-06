using UnityEngine;
using System.Collections;

public abstract class CollisionHandlerBase : MonoBehaviour {

	protected ActorData objectData;

	public virtual void Start()
	{
		GetObjectData();	
	}

	protected abstract void GetObjectData ();

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (ShouldDestroySelf(other))
        {
            DestroyObject(gameObject);
            ActionAfterDestroySelf(other);
        }

        if (ShouldDestroyOther(other))
        {
            DestroyObject(other.gameObject);
            ActionAfterDestroyOther(other);
        }
    }

    protected virtual bool ShouldDestroySelf(Collider2D other)
    {
        return false;
    }

    protected virtual bool ShouldDestroyOther(Collider2D other)
    {
        return false;
    }

    protected virtual void ActionAfterDestroySelf(Collider2D other){ }

    protected virtual void ActionAfterDestroyOther(Collider2D other) { }
}
