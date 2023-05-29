using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CopterRocketController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private GameObject _model;
    
    [SerializeField] private float _detectDistance = 9f;

    private bool _isFacingRight;
    private bool _isDetect = false;
    private bool _bulletAtack = false;
    private bool _rocketAtack = false;

    private float _timeToChangeAtackMode = 2f;
    private int _atackIndex;
    private float _intervalChangeMode = 2f;
    public bool isDetect
    {
        get => _isDetect;
    }
    public bool isRocketAtack
    {
        get => _rocketAtack;
    }
    public bool isBulletAtack
    {
        get => _bulletAtack;
    }
    public bool BulletAtack
    {
        get => _bulletAtack;
    }
    public int AtackIndex
    {
        get => _atackIndex;
        set { int atackTmp = _atackIndex; }
    }
   
    void Update()
    {
         if (_isDetect)
         {
             _timeToChangeAtackMode -= Time.deltaTime;
             if (_timeToChangeAtackMode < 0)
             {
                 _atackIndex++;
                 _timeToChangeAtackMode = _intervalChangeMode;
             }
         }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            _atackIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            _atackIndex = 2;
        }
       
        FlipToPlayer();
        SwitchAtackMode();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(_model.transform.position, _playerPos.transform.position) < _detectDistance)
        {
            _isDetect = true;
        }
        else if (Vector2.Distance(_model.transform.position, _playerPos.transform.position) > _detectDistance)
        {
            _isDetect = false;
        }
    }
    void SwitchAtackMode()
    {
        if (_isDetect)
        {
            switch (_atackIndex)
            {
                case 1:  // Gun atack
                    _bulletAtack = true;
                    _rocketAtack = false;
                    break;
                case 2:  // Rocket atack
                    _bulletAtack = false;
                    _rocketAtack = true;
                    break;
                default:
                    _atackIndex = 1;
                    break;
            }
        }
    }
    
    void FlipToPlayer()
    {
        if (_playerPos.transform.position.x < transform.position.x && _isFacingRight)
        {
            Flip();
        }
        if (_playerPos.transform.position.x > transform.position.x && !_isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}


// поворот к игроку Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_playerPos.transform.position.y - transform.position.y, _playerPos.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90));