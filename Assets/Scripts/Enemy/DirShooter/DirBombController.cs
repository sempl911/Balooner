using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirBombController : MonoBehaviour
{
    [SerializeField] private GameObject _bombExplotion;

    PlayerHealth _playerDamage;
    Rigidbody2D _bombRb;
    private void Start()
    {
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _bombRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RotationBomb();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyOnPlayer();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DestroyBomb();
        }
    }

    void DestroyOnPlayer()
    {
        _playerDamage.ReduceDamage(10f);
        Destroy(gameObject);
        var expTmp = Instantiate(_bombExplotion, transform.position, Quaternion.identity);
        Destroy(expTmp, .5f);
    }
    private void DestroyBomb()
    {
        Destroy(gameObject);
        var expTmp = Instantiate(_bombExplotion, transform.position, Quaternion.identity);
        Destroy(expTmp, .5f);
    }
    void RotationBomb()
    {
        float rotationIndex = 1;
        transform.rotation = Quaternion.Euler(0, 0, rotationIndex * Time.deltaTime);
    }
}
