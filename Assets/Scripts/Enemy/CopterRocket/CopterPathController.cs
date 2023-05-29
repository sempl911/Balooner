using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopterPathController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private GameObject _copter;

    CopterRocketController _atackMode;

    //Path options
    [SerializeField] private GameObject _movePoint1;
    [SerializeField] private GameObject _movePoint2;
    [SerializeField] private GameObject _movePoint3;

    private bool _isBulletPath = false;
    private bool _bulletModeActive = false;
    private bool _rocketModeActive = false;
    private bool _isRocketPath = false;

    private void Start()
    {
        _atackMode = _copter.GetComponent<CopterRocketController>();
        transform.position = _copter.transform.position;
    }
    private void Update()
    {

        if (_atackMode.isDetect)
        {
            if (_atackMode.isBulletAtack)
            {
                if (!_bulletModeActive)
                {
                    _isBulletPath = true;
                    CopterBulletPath();
                }
            }
            else if (!_atackMode.isBulletAtack)
            {
                _bulletModeActive = false;
            }
            if (_atackMode.isRocketAtack)
            {
                if (!_rocketModeActive)
                {
                    _isRocketPath = true;
                    RocketAtackPath();
                }
            }
            else if(!_atackMode.isRocketAtack)
            {
                _rocketModeActive = false;
            }
        }
        MoveAllPath();
    }
    void CopterBulletPath()
    {
        if (_isBulletPath)
        {
            _movePoint1.transform.position = new Vector3(_playerPos.transform.position.x +3f, _playerPos.transform.position.y + 3f);
            _movePoint2.transform.position = new Vector3(_playerPos.transform.position.x +3f, _playerPos.transform.position.y - 3f);
            _movePoint3.transform.position = new Vector3(_playerPos.transform.position.x +5f, _playerPos.transform.position.y);
            _bulletModeActive = true;
        }
        _isBulletPath = false;
    }
    void RocketAtackPath()
    {
        if (_isRocketPath)
        {
            _movePoint1.transform.position = new Vector3(_playerPos.transform.position.x - 3f, _playerPos.transform.position.y + 3f);
            _movePoint2.transform.position = new Vector3(_playerPos.transform.position.x -3f, _playerPos.transform.position.y - 3f);
            _movePoint3.transform.position = new Vector3(_playerPos.transform.position.x  -5f, _playerPos.transform.position.y);
            _rocketModeActive = true;
        }
        _isRocketPath = false;
    }
    void MoveAllPath()
    {
        if (_atackMode.isDetect)
        {
            transform.position = new Vector3(_playerPos.transform.position.x, _playerPos.transform.position.y);
        }
    }
}
