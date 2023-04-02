using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private GameObject playerModel;

    private Material blinkMat;
    private Material defaultMat;
    private SpriteRenderer spriteRend;

    private float _health;

    public float Health
    {
        get => _health;
    }

    private void Start()
    {
        spriteRend = playerModel.GetComponent<SpriteRenderer>();
        blinkMat = Resources.Load("PlayerBlinkMaterial", typeof (Material)) as Material;
        defaultMat = spriteRend.material;

        _health = totalHealth;
        InitHealth();
    }
    public void ReduceDamage(float damage)
    {
        _health -= damage;
        InitHealth();
        spriteRend.material = blinkMat;

        if (_health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .2f);
        }
    }
    public void PowerUp(float powerUp)
    {
        
        if (_health <= totalHealth)
        {
            _health += powerUp;
            InitHealth();
        }
    }
    private void Die()
    {
        if (_health <= 0)
        {
            gameOverCanvas.SetActive(true);
            gameOverSound.Play();
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }
    private void InitHealth()
    {
        healthBar.value = _health / totalHealth;
    }
    private void ResetMaterial()
    {
        spriteRend.material = defaultMat;
    }
}
