using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCopterProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 15f;
    private Transform _playerPos;

    Rigidbody2D _projectileRb;
    PlayerHealth _playerDamage;
    Vector2 direction;

    private void Start()
    {
        _projectileRb = gameObject.GetComponent<Rigidbody2D>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        MoveProjectile();
    }
    private void Update()
    {
        Destroy(gameObject, 5f);
    }
    void MoveProjectile()
    {
        if (_playerPos.position.x < transform.position.x)
        {
            direction = Vector2.left;
        }
        if (_playerPos.position.x > transform.position.x)
        {
            direction = Vector2.right;
        }
        _projectileRb.AddForce(direction * _projectileSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerDamage.ReduceDamage(10f);
            Destroy(gameObject);
        }
    }
}
