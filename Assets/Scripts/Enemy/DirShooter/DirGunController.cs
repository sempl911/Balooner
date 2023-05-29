using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirGunController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 1.3f;

    private Transform _playerPos;

    Rigidbody2D _bulletRb;
    DirShooterController _isShootLeft;
    PlayerHealth _playerDamage;
    private Vector2 _bulletDirection;
    private void Start()
    {
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _isShootLeft = GameObject.Find("DirShooter").GetComponent<DirShooterController>();
        _bulletRb = gameObject.GetComponent<Rigidbody2D>();
        MoveBullet(); // <<<--- IN START
    }
    private void Update()
    {
        Destroy(gameObject, _lifeTime);
    }
    void MoveBullet()
    {
        if (_playerPos.position.x < transform.position.x)
        {
            _bulletDirection = Vector2.left;
        }
        if (_playerPos.position.x > transform.position.x)
        {
            _bulletDirection = Vector2.right;
        }
        _bulletRb.AddForce(_bulletDirection * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerDamage.ReduceDamage(5f);
            Destroy(gameObject);
        }
    }   
}
