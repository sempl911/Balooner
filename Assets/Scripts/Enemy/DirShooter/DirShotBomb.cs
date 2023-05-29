using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirShotBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombProjectile;
    [SerializeField] private GameObject _bombShotPoint;

    //Bomb options
    private const int bombInRow = 9;
    private float _bombInterval = .3f;
    private float _bombDelay = .5f;

    DirShooterController _dirAtackMode;

    private bool _isStartBombing;

    private void Start()
    {
        _dirAtackMode = gameObject.GetComponent<DirShooterController>();
    }

    private void Update()
    {
        if (_dirAtackMode.isBombAtack)
        {
            if (!_isStartBombing)
            {
                GunAtackOnOff(_dirAtackMode.isBombAtack);
            }
        }
        if (_dirAtackMode.isGunAtack)
        {
            StopAllCoroutines();
            _isStartBombing = false;
        }
    }
    bool GunAtackOnOff(bool isAtack)
    {
        if (isAtack)
        {
            StartCoroutine(GunAtackCoroutine(_bombInterval, _bombDelay));
        }
        if (!isAtack)
        {
            StopAllCoroutines();
        }
        return isAtack;
    }
    private void BombBulett()
    {
        Instantiate(_bombProjectile, _bombShotPoint.transform.position, Quaternion.Euler(0, 0, 90f));
    }

    // Gune Coroutine

    IEnumerator GunAtackCoroutine(float secondsShoot, float secondsNoShoot)
    {
        while (true)
        {
            _isStartBombing = true;

            for (int i = 0; i < bombInRow; i++)
            {
                BombBulett();
                yield return new WaitForSeconds(secondsShoot);
            }
            yield return new WaitForSeconds(secondsNoShoot);
            _isStartBombing = false;
        }
    }
}
