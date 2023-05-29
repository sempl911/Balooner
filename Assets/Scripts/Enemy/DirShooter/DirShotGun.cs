using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirShotGun : MonoBehaviour
{
    [SerializeField] private GameObject _gunProjectile;
    [SerializeField] private GameObject _gunShotPoint1stTrunk;
    [SerializeField] private GameObject _gunShotPoint2stTrunk;
    [SerializeField] private GameObject _shotEffect;

    //Gun options
    private const int _bulletInRow = 5;
    private float _shotInterval = .1f;
    private float _shotDelay = 1f;

    DirShooterController _dirAtackMode;

    private bool _isStartGun;

    void Start()
    {
        _dirAtackMode = gameObject.GetComponent<DirShooterController>();
    }

    void Update()
    {
        if (_dirAtackMode.isGunAtack)
        {
            if (!_isStartGun)
            {
                GunAtackOnOff(_dirAtackMode.isGunAtack);
            }
        }
        if (_dirAtackMode.isBombAtack)
        {
            StopAllCoroutines();
            _isStartGun = false;
        }

    }
    bool GunAtackOnOff( bool isAtack)
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
    private void ShootBullet1stTrunk()
    {
        Instantiate(_gunProjectile, _gunShotPoint1stTrunk.transform.position, Quaternion.Euler(0, 0, 90f));  
    }
    private void ShootBullet2stTrunk()
    {
        Instantiate(_gunProjectile, _gunShotPoint2stTrunk.transform.position, Quaternion.Euler(0, 0, 90f));
    }
    private void ShotEffect()
    {
        var shotEffect = Instantiate(_shotEffect, _gunShotPoint1stTrunk.transform.position, Quaternion.identity);
        Destroy(shotEffect, .5f);
    }
    // Gune Coroutine

    IEnumerator GunAtackCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            _isStartGun = true;

            for (int i = 0; i < _bulletInRow; i++)
            {
                ShootBullet1stTrunk();
                ShootBullet2stTrunk();
                ShotEffect();

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