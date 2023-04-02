using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChopperHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private GameObject chopperCanvas;
    [SerializeField] private AudioSource chopperDamage;
    [SerializeField] private GameObject modelChopper;

    private Material matDefault;
    private Material matBlink;
    private SpriteRenderer spritRend;

    private float _health;
    private float _timer = 4f;
    private bool _isAtacking = false;

    private void Start()
    {
        spritRend = modelChopper.GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("ChopperBlinkMaterial", typeof(Material)) as Material;
        matDefault = spritRend.material;

        _health = totalHealth;
        InitHealth();
        chopperCanvas.SetActive(false);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <=0 && _isAtacking)
        {
            chopperCanvas.SetActive(false);
            _timer = 4f;
        }
    }
    public void ReduceDamage(float damage)
    {
        _health -= damage;
        chopperDamage.Play();
        spritRend.material = matBlink;
        InitHealth();
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .2f);
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
        healthBar.value = _health / totalHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ReduceDamage(10f);
            Destroy(collision.gameObject);
            _isAtacking = true;
            chopperCanvas.SetActive(true);
        }
        else
        {
            _isAtacking = false;
        }
    }
    private void ResetMaterial()
    {
        spritRend.material = matDefault;
    }
}
