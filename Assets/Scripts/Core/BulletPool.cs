using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int initial = 20;
    Queue<Bullet> _pool = new Queue<Bullet>();

    void Awake()
    {
        for (int i = 0; i < initial; i++)
        {
            var b = Instantiate(bulletPrefab, transform);
            b.Init(this);
            b.gameObject.SetActive(false);
            _pool.Enqueue(b);
        }
    }

    public Bullet Spawn(Vector3 pos, Vector2 dir)
    {
        var b = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(bulletPrefab, transform);
        b.Init(this);
        b.transform.position = pos;
        b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        b.Fire(dir);
        return b;
    }

    public void Despawn(Bullet b)
    {
        b.gameObject.SetActive(false);
        _pool.Enqueue(b);
    }
}

