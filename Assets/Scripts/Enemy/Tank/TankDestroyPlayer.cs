using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDestroyPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _tank;
    [SerializeField] private GameObject _detectCollider;
    [SerializeField] private GameObject _magnetoPlayerExp;
    [SerializeField] private GameObject _player;

    PlayerHealth _playerHealth;
    TankColliderDetect _isTankStay;
    TankDamageMagneto _isPlayerSmall;

    private bool _playerDestroy = false;
    private bool _isDamage;
    private bool _isMagnetoExp = false;
    private float _damageMagneto = .5f;
    private float _timeToDead = .7f;

    private void Start()
    {
        _isTankStay = _detectCollider.GetComponent<TankColliderDetect>();
        _isPlayerSmall = _tank.GetComponent<TankDamageMagneto>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        _damageMagneto -= Time.deltaTime;
        if (_damageMagneto <= 0)
        {
            _damageMagneto = .5f;
            _isDamage = true;
        }
        PlayerDamaged();
        PlayerReduceHealth();
    }
    void PlayerDamaged()
    {
        if (_isTankStay.isPlayerStay && _isPlayerSmall.IsDamageTank)
        {
            _playerDestroy = true;
        }
        else
        {
            _playerDestroy = false;
        }
    }
    void PlayerReduceHealth()
    {
        if (_playerDestroy)
        {
            if (_isDamage)
            {
                if (_playerHealth.Health >20)
                {
                    _playerHealth.ReduceDamage(20f);
                    _isDamage = false;
                }
                else if (_playerHealth.Health <= 20f)
                {
                    MagnetoPlayerExp();
                    TimeToDead();
                }
            }
        }        
    }
    void MagnetoPlayerExp()
    {
        if (_playerHealth.Health <=20f && !_isMagnetoExp)
        {
            var expTmp = Instantiate(_magnetoPlayerExp, _player.transform.position, Quaternion.identity);
            Destroy(expTmp, .7f);
            _isMagnetoExp = true;
        }  
    }
    void TimeToDead()
    {
        _timeToDead -= Time.deltaTime;
        if (_timeToDead <= 0)
        {
            _playerHealth.ReduceDamage(20f);
        }
    }

}
