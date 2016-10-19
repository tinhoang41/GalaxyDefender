using UnityEngine;
using System.Collections;

public class ShooterBase : MonoBehaviour {

    private float _nextFire;
    private float _size;

    public GameObject shot;
    public float fireRate;

    protected bool pCanShoot
    {
        get { return Time.time > _nextFire; }
    }

    // Use this for initialization
    public virtual void Start()
    {
        SpriteRenderer spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        var sizeVector     = spr == null ? Vector3.zero : spr.bounds.size;
        _size              = Mathf.Max(Mathf.Max(sizeVector.x, sizeVector.y), sizeVector.z);
        _nextFire          = 0.0f;
    }

    protected virtual void ShootBullet(Vector3 direction)
    {
        direction.Normalize();
        var position      = transform.position;
        position         += direction;
        var rotationAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.y);
        var rotation      = Quaternion.Euler(0.0f, 0.0f, -rotationAngle);

        Instantiate(shot, position, rotation);
        _nextFire = Time.time + fireRate;

    }
}
