using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamageDetect : MonoBehaviour
{
    [SerializeField] private GameObject _bombAllObject;

    [SerializeField] private GameObject _bombDamage;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speedBomb;

    private bool _isDetect;

    Rigidbody2D _bombRb;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _bombRb = _bombDamage.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        DetectGoBomb();
    }
    void DetectGoBomb()
    {
        if (_isDetect)
        {
            Vector3 direction;
            direction = _player.transform.position - _bombDamage.transform.position;
            _bombRb.velocity = new Vector3(direction.x * _speedBomb * Time.fixedDeltaTime, direction.y * _speedBomb * Time.deltaTime);
        }
        _bombAllObject.transform.position = _bombDamage.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isDetect = true;
        }
    }
}
