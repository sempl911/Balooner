using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopterRocketProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
     private Transform _playerPos;

    Rigidbody2D _projectileRb;
    PlayerHealth _playerDamage;

    private void Start()
    {
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _projectileRb = gameObject.GetComponent<Rigidbody2D>();

        Vector2 destenation = _playerPos.transform.position - transform.position;
        _projectileRb.AddForce(destenation * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void Update()
    {
        Destroy(gameObject, 3f);
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



// поворот к игроку Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_playerPos.transform.position.y - transform.position.y, _playerPos.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90));