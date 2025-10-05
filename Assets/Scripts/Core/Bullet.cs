using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 16f;
    public float damage = 1f;
    public float lifeTime = 2f;

    Rigidbody2D _rb;
    BulletPool _pool;
    float _despawnAt;

    public void Init(BulletPool pool) { _pool = pool; }
    void Awake() { _rb = GetComponent<Rigidbody2D>(); }

    public void Fire(Vector2 dir)
    {
        gameObject.SetActive(true);
        _rb.linearVelocity = dir.normalized * speed;
        _despawnAt = Time.time + lifeTime;
    }

    void Update() { if (Time.time >= _despawnAt) Despawn(); }

    void OnTriggerEnter2D(Collider2D other)
    {
        int L = other.gameObject.layer;
        if (L == LayerMask.NameToLayer("Enemy"))
        {
            var hp = other.GetComponent<Health>();
            if (hp) hp.Damage(damage);
            Despawn();
        }
        else if (L == LayerMask.NameToLayer("Walls") || L == LayerMask.NameToLayer("Obstacles"))
        {
            Despawn();
        }
    }

    void Despawn()
    {
        _rb.linearVelocity = Vector2.zero;
        _pool.Despawn(this);
    }
}
