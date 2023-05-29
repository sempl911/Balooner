using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtack : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] moveSpots;
    [SerializeField] private float _startWaitTime;

    CopterRocketController _atackMode;

    //Gun projectile options
    [SerializeField] private GameObject _gunProjectile;
    [SerializeField] private GameObject _gunShotPoint;
    //Gun options
    private const int _bulletInRow = 5;
    private float _shotInterval = .1f;
    private float _shotDelay = 1f;
    //Moving param
    private int _ramdomSpot;
    private float _waitTime;
    private bool _isStartGun;

    private void Start()
    {
        _waitTime = _startWaitTime;
        _ramdomSpot = Random.Range(0, moveSpots.Length);
        _atackMode = gameObject.GetComponent<CopterRocketController>();
    }
    private void Update()
    {
        if (_atackMode.isBulletAtack)
        {
            if (!_isStartGun)
            {
                GunAtackOnOff(_atackMode.isBulletAtack);
            }
        }
        if (_atackMode.isRocketAtack)
        {
            StopAllCoroutines();
            _isStartGun = false;
        }
        if (_atackMode.BulletAtack)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[_ramdomSpot].position, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[_ramdomSpot].position)< .1f)
            {
                if (_waitTime <=0)
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
            StartCoroutine(GunAtackCoroutine(_shotInterval, _shotDelay));
        }
        if (!isAtack)
        {
            StopAllCoroutines();
        }
        return isAtack;
    }
    void ShotBullet()
    {
        Instantiate(_gunProjectile, _gunShotPoint.transform.position, Quaternion.identity);
    }
    IEnumerator GunAtackCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            _isStartGun = true;

            for (int i = 0; i < _bulletInRow; i++)
            {
                ShotBullet();
                yield return new WaitForSeconds(secondsShoot);
            }
            yield return new WaitForSeconds(secondsNoShoot);
            _isStartGun = false;
        }
    }
}


/* Чтоб остановить корутину или вызвать разово нужно добавить буль в начало и конец корутины и выполнять проверку
 * bool start = false;
 * void Update (){
 * if(!start){
 * StartCoroutine(Название нужной крутины()); 
 * }
*/

