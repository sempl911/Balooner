using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CopterRocketHealth : MonoBehaviour
{
    [SerializeField] private Slider _copterHealthBar;
    [SerializeField] private float _totalHealth = 100f;
    [SerializeField] private GameObject _copterCanvas;
    [SerializeField] private GameObject _copterModel;
    [SerializeField] private GameObject _copterBlowUp;

    private SpriteRenderer _spriteRend;
    private Material _defaultMat;
    private Material _blinkMat;

    private float _health;
    private float _timer = 2f;
    private bool _isAtacking = false;

    private void Start()
    {
        _spriteRend = _copterModel.GetComponent<SpriteRenderer>();
        _blinkMat = Resources.Load("CopterRocketBlinkMaterial", typeof(Material)) as Material;
        _defaultMat = _spriteRend.material;

        _health = _totalHealth;
        InitHealth();
        _copterCanvas.SetActive(false);
        _copterBlowUp.SetActive(false);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && _isAtacking)
        {
            _copterCanvas.SetActive(false);
            _timer = 2f;
        }
        _copterBlowUp.transform.position = _copterModel.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ReduceDamage(10f);
            Destroy(collision.gameObject);
            _copterCanvas.SetActive(true);
            _isAtacking = true;
        }
    }
    private void ReduceDamage(float damage)
    {
        _health -= damage;
        InitHealth();
        _spriteRend.material = _blinkMat;
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .2f);
            _isAtacking = false;
        }
    }
    private void Die()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
            _copterBlowUp.SetActive(true);
            Destroy(_copterBlowUp, 1f);
        }
    }
    private void InitHealth()
    {
        _copterHealthBar.value = _health / _totalHealth;
    }
    private void ResetMaterial()
    {
        _spriteRend.material = _defaultMat;
    }
   
}
