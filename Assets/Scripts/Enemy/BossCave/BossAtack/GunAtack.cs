using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAtack : MonoBehaviour
{
    [SerializeField] GameObject _bossGunProjectile;
    [SerializeField] GameObject _bossCave;
    [SerializeField] GameObject _shotEffect;
    [SerializeField] GameObject _bossTrunk;

   // Animator _gunAnim;

    BossCaveController _atackMode;

    private const int _bulletInRow = 4;
    private float _shotInterval = .4f;
    private float _shotDelay = 1f;
    private bool _isStartGun; // coroutine varible

    private void Start()
    {
        _atackMode = _bossCave.GetComponent<BossCaveController>();
       // _gunAnim = _bossTrunk.GetComponent<Animator>();
    }
    private void Update()
    {
        if (_atackMode.isGunAtack)
        {
            if (!_isStartGun)
            {
                GunAtackOnOff(true);
            }
        }
        if (!_atackMode.isGunAtack)
        {
            GunAtackOnOff(false);
            _isStartGun = false;
           // _gunAnim.SetTrigger("ShotTrig");
        }
    }
    void GunShot()
    {
        Instantiate(_bossGunProjectile, transform.position, Quaternion.identity);
    }
    void ShotEffect()
    {
       var shotVxTmp =  Instantiate(_shotEffect, transform.position, Quaternion.identity);
        Destroy(shotVxTmp, .3f);
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
    IEnumerator GunAtackCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            _isStartGun = true;

            for (int i = 0; i < _bulletInRow; i++)
            {
               // _gunAnim.SetTrigger("ShotTrig");

                GunShot();
                ShotEffect();

                yield return new WaitForSeconds(secondsShoot);
            }
            yield return new WaitForSeconds(secondsNoShoot);
            _isStartGun = false;
        }
    }
}
