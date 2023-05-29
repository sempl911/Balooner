using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DirShooterController : MonoBehaviour
{
    [SerializeField] private Transform _modelDir;
    [SerializeField] private GameObject _playerPos;

    [SerializeField] private float _detectDistance;
    // Bomb points

    [SerializeField] private GameObject _BombPoint1;
    [SerializeField] private GameObject _BombPoint2;

    //Conditions

    private bool _isDetect = false;
    private bool _isGunAtack = false;
    private bool _isBombAtack = false;

    Rigidbody2D _dirRb;

    private Vector2 _currentPos;

    private bool _isFacingRight;

    private bool _shootLeft;

    public bool isShootLeft // DirGunController
    {
        get => _shootLeft;
    }
    public bool isGunAtack
    {
        get => _isGunAtack;
    }
    public bool isBombAtack
    {
        get => _isBombAtack;
    }
    public Vector2 currentGunPos
    {
        get => _currentPos;
    }
    private int _atackIndex = 1;
    private float _timeToChangeAtackMode;
    private float _intervalChandeMode = 3f;

    private void Start()
    {
        _dirRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       if (_isDetect)
       {
            _timeToChangeAtackMode -= Time.deltaTime;
            if (_timeToChangeAtackMode < 0)
            {
                _atackIndex++;
                _timeToChangeAtackMode = _intervalChandeMode;
            }
        }

        if (_playerPos.transform.position.x < transform.position.x && _isFacingRight)
        {
            Flip();
            _shootLeft = true;
        }
        if (_playerPos.transform.position.x > transform.position.x && !_isFacingRight)
        {
            Flip();
            _shootLeft = false;
        }
        AtackSwitch();
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(_modelDir.transform.position,_playerPos.transform.position) < _detectDistance)
        {
            _isDetect = true;
        }
        else if (Vector2.Distance(_modelDir.transform.position, _playerPos.transform.position) > _detectDistance)
        {
            _isDetect = false;
        }
     
        if (_isDetect)
        {
            DetectPlayer();
        }
        if (_isGunAtack)
        {
            _currentPos = _modelDir.transform.position;
        }
        if (_isBombAtack)
        {
            BombAtack();
        }
    }

    // Logic block

    void DetectPlayer()
    {
            Vector2 newPos = new Vector2(transform.position.x, _playerPos.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, newPos, .9f * Time.deltaTime);
    }
    void BombAtack()
    {
        // Point position
        Vector2 bombPointPos1 = new Vector2(_playerPos.transform.position.x - 5f, _playerPos.transform.position.y + 4f);
        _BombPoint1.transform.position = bombPointPos1;

        Vector2 bombPointPos2 = new Vector2(_playerPos.transform.position.x + 5f, _playerPos.transform.position.y + 4f);
        _BombPoint2.transform.position = bombPointPos2;
        //Dir move
    }
    void AtackSwitch()
    {
        if (_isDetect)
        {
            switch (_atackIndex)
            {
                case 1:  // Bomb atack
                    _isGunAtack = false;
                    _isBombAtack = true;
                    break;
                case 2:  // Gun atack
                    _isGunAtack = true;
                    _isBombAtack = false;
                    break;
                default:
                    _atackIndex = 1;
                    break;
            }
        } 
    }

    // Flip Block

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = _modelDir.localScale;
        playerScale.x *= -1;
        _modelDir.localScale = playerScale;
    }   
}
