using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHP = 10f;
    public float currentHP;
    public UnityEvent onDeath;
    public bool destroyOnDeath = false;

    void Awake() => currentHP = maxHP;

    public void Damage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0f) Die();
    }

    void Die()
    {
        onDeath?.Invoke();
        if (destroyOnDeath) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
