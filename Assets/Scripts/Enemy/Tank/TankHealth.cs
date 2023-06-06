using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private GameObject _allTank;
    [SerializeField] private GameObject _smokeVFX;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        _smokeVFX.SetActive(false);
    }
    private void Update()
    {
        SmokeVFX();
    }
    public void ReduceDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
        
    }
    void Die()
    {
        if (_health <= 0)
        {
            Destroy(_allTank);
        }
    }
    void SmokeVFX()
    {
        if (_health < totalHealth)
        {
            _smokeVFX.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ReduceDamage(10f);
            Destroy(collision.gameObject);
        }
    }
}
