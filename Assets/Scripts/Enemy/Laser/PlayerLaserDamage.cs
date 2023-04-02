using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserDamage : MonoBehaviour
{
    [SerializeField] private float _damage = 2f;
    PlayerHealth healthPlayer;

    private void Start()
    {
        healthPlayer = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthPlayer.ReduceDamage(_damage);
        }
    }
}
