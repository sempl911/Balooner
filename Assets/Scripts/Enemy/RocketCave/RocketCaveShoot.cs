using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCaveShoot : MonoBehaviour
{
    [SerializeField] private GameObject _rocketProjectile;
    [SerializeField] private Transform _playerModel;

    [SerializeField] private float _shootDistance = 1f;

    Animator _caveAnim;

    private bool _isShoot = false;

    private void Start()
    {
        _caveAnim = gameObject.GetComponent<Animator>();
        InvokeRepeating("CaveShoot", 5f, 1f);
    }

    private void Update()
    {
        if (Vector3.Distance(_playerModel.transform.position, transform.position) < _shootDistance)
        {
            _isShoot = true;
        }
        else
        {
            _isShoot = false;
        }

        if (Vector3.Distance(_playerModel.transform.position, transform.position) < _shootDistance + 1f)
        {
            _caveAnim.SetTrigger("Atack");
        }
        else
        {
            _caveAnim.SetTrigger("EndAtack");
        }
    }

    void CaveShoot()
    {
        if (_isShoot)
        {
            var ShootTmp = Instantiate(_rocketProjectile, transform.position, Quaternion.identity);
        }
    }
}
