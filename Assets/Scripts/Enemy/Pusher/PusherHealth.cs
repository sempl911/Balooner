using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PusherHealth : MonoBehaviour
{
    [SerializeField] private Slider _pusherHealthBar;
    [SerializeField] float _totalHealth = 100f;
    [SerializeField] private GameObject _pusherModel;
    [SerializeField] private GameObject _pusherCanvas;

    private Material _blinkMat;
    private Material _defaultMat;
    private SpriteRenderer spriteRenderer;

    private float _health;
    private float _timer = 2f;
    private bool _isAtacking = false;

    void Start()
    {
        spriteRenderer = _pusherModel.GetComponent<SpriteRenderer>();
        _blinkMat = Resources.Load("PusherBlinkMaterial", typeof(Material)) as Material;
        _defaultMat = spriteRenderer.material;
        _health = _totalHealth;
        InitHealth();
        _pusherCanvas.SetActive(false);
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && _isAtacking)
        {
            _pusherCanvas.SetActive(false);
            _timer = 2f;
        }
    }
    public void ReduceDamage(float damage)
    {
        _health -= damage;
        InitHealth();
        spriteRenderer.material = _blinkMat;

        if (_health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .05f);
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
        _pusherHealthBar.value = _health / _totalHealth;
    }
    private void ResetMaterial()
    {
        spriteRenderer.material = _defaultMat;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ReduceDamage(15f);
            Destroy(collision.gameObject);
            _isAtacking = true;
            _pusherCanvas.SetActive(true);
        }
        else
        {
            _isAtacking = false;
        }
    }
}
