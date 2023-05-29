using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirHealth : MonoBehaviour
{
    [SerializeField] private Slider _dirHealthBar;
    [SerializeField] private float _totalHealth = 100f;
    [SerializeField] private GameObject _dirCanvas;

    private float _health;
    private float _timer = 2f;
    private bool _isAtacking = false;

    private void Start()
    {
        _health = _totalHealth;
        InitHealth();
        _dirCanvas.SetActive(false);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && _isAtacking)
        {
            _dirCanvas.SetActive(false);
            _timer = 2f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ReduceDamage(10f);
            Destroy(collision.gameObject);
            _dirCanvas.SetActive(true);
            _isAtacking = true;
        }
    }
    private void ReduceDamage (float damage)
    {
        _health -= damage;
        InitHealth();
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            _isAtacking = false;
        }
    }
    private void Die()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void InitHealth()
    {
        _dirHealthBar.value = _health / _totalHealth;
    }
}
