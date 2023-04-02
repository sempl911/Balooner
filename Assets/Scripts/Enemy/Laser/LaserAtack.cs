using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAtack : MonoBehaviour
{
    [SerializeField] private GameObject _laserProjectile;
    [SerializeField] private GameObject _shotPoint;
    [SerializeField] private GameObject _shotEndPoint;
    [SerializeField] private GameObject _laserCurrentHealth;
    [SerializeField] private AudioSource _laserShotSound;

    LaserController isPlayerDetect;
    LaserHealth laserHealth;

    private bool _isAtack;
    private float _shotInterval = .5f;
    private float _shotDelay = 1f;

    private const int bulletInRow = 5;

    public Transform shotPoint
    {
        get => _shotPoint.transform;
    }
    public Transform shotEndPoint
    {
        get => _shotEndPoint.transform;
    }

    void Start()
    {
        isPlayerDetect = GetComponent<LaserController>();
        laserHealth =_laserCurrentHealth.GetComponent<LaserHealth>();
        StartCoroutine(ShootCoroutine(_shotInterval, _shotDelay));
    }

    void Update()
    {
        _isAtack = isPlayerDetect.IsPlayerDetect;
     
    }
    void LaserShoot()
    {
        if (laserHealth.LaserCurrentHealth > 0f)
        {
            if (_isAtack)
            {
                StartCoroutine(ShootCoroutine(_shotInterval, _shotDelay));
            }
            else
            {
                StopAllCoroutines();
            }
        }       
    }

    void Atack()
    {
        if (_isAtack)
        {
            var shot = Instantiate(_laserProjectile, _shotPoint.transform.position, Quaternion.identity);
            _laserShotSound.Play();
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
