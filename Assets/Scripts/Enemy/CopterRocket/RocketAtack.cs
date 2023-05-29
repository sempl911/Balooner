using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAtack : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] moveSpots;
    [SerializeField] private float _startWaitTime;

    [SerializeField] private GameObject _playerPos;
    // Rocket options
    [SerializeField] private GameObject _rocketProjectile;
    [SerializeField] private GameObject _rocketShotPoint;

    CopterRocketController _atackMode;
    //Moving param
    private int _ramdomSpot;
    private float _waitTime;

    //Rocket options
    private const int _bulletInRow = 7;
    private float _shotInterval = .05f;
    private float _shotDelay = 1f;
    private bool _isStartGun;

    private void Start()
    {
        _atackMode = gameObject.GetComponent<CopterRocketController>();
    }

    private void Update()
    {

        if (_atackMode.isRocketAtack)
        {
            if (!_isStartGun)
            {
                GunAtackOnOff(_atackMode.isRocketAtack);
            }
        }
        if (_atackMode.isBulletAtack)
        {
            StopAllCoroutines();
            _isStartGun = false;
        }
        if (_atackMode.isRocketAtack)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[_ramdomSpot].position, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[_ramdomSpot].position) < .1f)
            {
                if (_waitTime <= 0)
                {
                    _ramdomSpot = Random.Range(0, moveSpots.Length);
                    _waitTime = _startWaitTime;
                }
                else
                {
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }
    bool GunAtackOnOff(bool isAtack)
    {
        if (isAtack)
        {
            StartCoroutine(RocketAtackCoroutine(_shotInterval, _shotDelay));
        }
        if (!isAtack)
        {
            StopAllCoroutines();
        }
        return isAtack;
    }
    void ShotRocket()
    {
        Instantiate(_rocketProjectile, _rocketShotPoint.transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_playerPos.transform.position.y - transform.position.y, _playerPos.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90));
    }
    IEnumerator RocketAtackCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            _isStartGun = true;

            for (int i = 0; i < _bulletInRow; i++)
            {
                ShotRocket();
                yield return new WaitForSeconds(secondsShoot);
            }
            yield return new WaitForSeconds(secondsNoShoot);
            _isStartGun = false;
        }
    }
}
