using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class TankDamageMagneto : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _tankDetectCollider;

    TankColliderDetect _playerIsDet;

    private bool _isPlayerDetect;
    private bool _isTankDamage = false;
    private float _magnetoStrench = 2f;
    private float _timeToSmall = .2f;
    private float _smallInterval = .1f;
    private float _timeToAction = .5f;
    private float _distance;
    private float _playerMinSmall = .2f;

    public bool IsDamageTank
    {
        get => _isTankDamage;
    }

    void Start()
    {
        _playerIsDet = _tankDetectCollider.GetComponent<TankColliderDetect>();
        Time.timeScale = 1;
    }
    void Update()
    {
        _isPlayerDetect = _playerIsDet.isPlayerDetect;  
        PlayerMinSmall();
    }
    void FixedUpdate()
    {
        PlayerAtack();
    }
    void PlayerAtack()
    {
        if (_isPlayerDetect)
        {
             _timeToAction -= Time.deltaTime;
             if (_timeToAction <=0)
             {
                 PlayerMagniting();
                 PlayerSmaller();
             }
            _distance = Vector2.Distance(_player.transform.position, transform.position);  
        }
        if (!_isPlayerDetect)
        {
            _timeToAction = .5f;
        }
    }
    void PlayerMagniting()
    {
        _player.transform.position = Vector3.Lerp(_player.transform.position, transform.position, Time.deltaTime * _magnetoStrench);
    }
    void PlayerSmaller()
    {
        if (_distance <= 2.6f)
        {
            if (_player.transform.localScale.x > _playerMinSmall)
            {
                _timeToSmall -= Time.deltaTime;
                if (_timeToSmall <= 0)
                {
                    _player.transform.localScale -= new Vector3(.1f, .1f, 0);
                    _timeToSmall = _smallInterval;
                }
            }
        }
        if (_distance > 2.6f)
        {
            if (_player.transform.localScale.x < 1f)
            {
                _timeToSmall -= Time.deltaTime;
                if (_timeToSmall <= 0)
                {
                    _player.transform.localScale += new Vector3(.1f, .1f, 0);
                    _timeToSmall = _smallInterval;
                }
            }
        }
    }
    void PlayerMinSmall()
    {
        if (_player.transform.localScale.x <= _playerMinSmall)
        {
            _isTankDamage = true;
           /* _player.SetActive(false);
            var ExpTmp = Instantiate(_tankMagExp, _player.transform.position, Quaternion.identity);
            Destroy(ExpTmp, 1f);
            _isTankDamage = true;
            _playerHealth.ReduceDamage(30f);*/
        }
        else
        {
            _isTankDamage = false;
        }
    }
    
}
