using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] private GameObject _laserCanvas;
    [SerializeField] private GameObject _laser;
    [SerializeField] private float totalHealth = 100f;

    private float _laserHealth = 4f;
    public float LaserCurrentHealth
    {
        get => _laserHealth;
    }

    private void Start()
    {
        _laserHealth = totalHealth;
        _laserCanvas.SetActive(false);
    }

    public void ReduceDamage(float damage)
    {
        _laserHealth -= damage;
        InitHealth();
        if (_laserHealth <= 0)
        {
            Die();
        }
       
    }

    private void Die()
    {
        if (_laserHealth <= 0)
        {
            Destroy(_laser);
        }
    }
    private void InitHealth()
    {
        healthBar.value = _laserHealth / totalHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            _laserCanvas.SetActive(true);

            ReduceDamage(25f);
            Destroy(collision.gameObject);
        }
    }           
}
