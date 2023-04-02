using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperAtack : MonoBehaviour
{
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform player;
    [SerializeField] private AudioSource shotSound;

    PlayerHealth _playerHealth;
    ChopperController _chopperController;

    private bool _isAtack;
    private bool _isAtackSide;
    private float _shootInterval = .1f;
    private float _shootDelay = 1f;

    private const int bulletInRow = 5;

    private void Start()
    {
        _chopperController = GetComponent<ChopperController>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        StartCoroutine(ShootCoroutine(_shootInterval, _shootDelay));
    }
    private void Update()
    {
        _isAtack = _chopperController.isPlayerVisibly;
    }
    void ChopperShoot()
    {
        if (_isAtack)
        {
            StartCoroutine(ShootCoroutine(_shootInterval, _shootDelay));
        }
        else
        {
            StopAllCoroutines();
        }
    }
    void Atack()
    {
        if (_playerHealth.Health > 0)
        {
            if (_isAtack)
            {
                var shot = Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);
                shotSound.Play();
            }
        }
        
    }
    IEnumerator ShootCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            for (int i = 0; i < bulletInRow; i++)
            {
                Atack();
                yield return new WaitForSeconds(secondsShoot);
            }
            yield return new WaitForSeconds(secondsNoShoot);
        }
    }
}
