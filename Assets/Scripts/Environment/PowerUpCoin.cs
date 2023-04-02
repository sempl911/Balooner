using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoin : MonoBehaviour
{
    PlayerHealth changeHealth;
    [SerializeField] private AudioSource compliteCoin;
    void Start()
    {
        changeHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            changeHealth.PowerUp(15f);
            compliteCoin.Play();
            Destroy(gameObject);
        }
    }
}
