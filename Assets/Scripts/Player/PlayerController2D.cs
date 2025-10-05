using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public bool faceMovement = true;
    public bool aimWithMouse = true;

    [Header("Disparo")]
    public BulletPool bulletPool;
    public float fireRate = 6f;
    public Transform firePoint;

    Rigidbody2D _rb;
    Vector2 _move;
    Vector2 _lastAimDir = Vector2.right;
    float _nextShot;

    void Awake() { _rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Vector2 aimDir = _lastAimDir;
        if (aimWithMouse)
        {
            Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimDir = ((Vector2)(m - transform.position)).normalized;
        }
        else if (_move.sqrMagnitude > 0.01f) aimDir = _move;

        if (aimDir.sqrMagnitude > 0.01f)
        {
            _lastAimDir = aimDir;
            if (faceMovement || aimWithMouse)
                _rb.rotation = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && Time.time >= _nextShot)
        {
            _nextShot = Time.time + 1f / fireRate;
            if (bulletPool && firePoint) bulletPool.Spawn(firePoint.position, _lastAimDir);
        }
    }

    void FixedUpdate() { _rb.linearVelocity = _move * moveSpeed; }
}
