using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurelHealth : MonoBehaviour
{
    [SerializeField] private GameObject turelAllObject;
    [SerializeField] private GameObject modelTurel;
    [SerializeField] private GameObject modelTurelTrunk;

    [SerializeField] private Slider healthBar;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private GameObject turelCanvas;
    [SerializeField] private AudioSource turelDamageSound;

    private Material matDefault;
    private Material matBlink;
    private Material matBlinkTrunk;

    private SpriteRenderer spritRend;
    private SpriteRenderer spritRendTrunk;


    private float _health;
    private float _timer = 4f;
    private bool _isAtacking = false;

    private void Start()
    {
        spritRend = modelTurel.GetComponent<SpriteRenderer>();
        spritRendTrunk = modelTurelTrunk.GetComponent<SpriteRenderer>();


        matBlink = Resources.Load("TurelMaterial", typeof(Material)) as Material;
        matBlinkTrunk = Resources.Load("TurelMaterial", typeof(Material)) as Material;

        matDefault = spritRend.material;
        matDefault = spritRendTrunk.material;

        _health = totalHealth;
        InitHealth();
        turelCanvas.SetActive(false);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && _isAtacking)
        {
            turelCanvas.SetActive(false);
            _timer = 4f;
        }
    }
    public void ReduceDamage(float damage)
    {
        _health -= damage;
        turelDamageSound.Play();
        spritRend.material = matBlink;
        spritRendTrunk.material = matBlinkTrunk;
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
            Destroy(turelAllObject);
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
            ReduceDamage(20f);
            Destroy(collision.gameObject);
            _isAtacking = true;
            turelCanvas.SetActive(true);
        }
        else
        {
            _isAtacking = false;
        }
    }
    private void ResetMaterial()
    {
        spritRend.material = matDefault;
        spritRendTrunk.material = matDefault;

    }
}
