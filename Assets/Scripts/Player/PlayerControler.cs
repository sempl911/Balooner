using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTransformModel;

    Rigidbody2D _rB;
    Camera cam;
    PlayerHealth health;

    private float _horizontal;
    private float _vertical;
    private float _offsideOffset = 1f;
    private float _playerLimX;
    private float _playerLimY;
    private float _rotationLimit = 7f;
    private bool _isFacingRight;
    private bool _isLeftX;
    private bool _isRigthX;
    private bool _isTopY;
    private bool _isBottomY;

    const float speedMultiplier = 50f;

    public bool isFasingRigth
    {
        get => _isFacingRight;
    }
    public float InputVertical
    {
        get => _vertical;
    }
    public float InputHorizontal
    {
        get => _horizontal;
    }
    private void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        PlayerLimits();
        PlayerBoost();
        MoveRotations();
    }
    private void FixedUpdate()
    {
        PlayerKeyBoardControl();
       // PlayerAltControl();
        if (_horizontal > 0 && _isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0 && !_isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerTransformModel.localScale;
        playerScale.x *= -1;
        playerTransformModel.localScale = playerScale;
    }
    void MoveRotations()
    {
        if (_vertical < 0f)
        {
            _rB.transform.rotation = Quaternion.Euler(0, 0, _rotationLimit);
            if (!_isFacingRight)
            {
                _rB.transform.rotation = Quaternion.Euler(0, 0, -_rotationLimit);
            }

        }
        if (_vertical > 0f)
        {
            _rB.transform.rotation = Quaternion.Euler(0, 0, -_rotationLimit);
            if (!_isFacingRight)
            {
                _rB.transform.rotation = Quaternion.Euler(0, 0, _rotationLimit);
            }
        }
        if (_vertical == 0f)
        {
            _rB.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
   public void PlayerLimits()
    {
        Vector3 playerOnScreen = cam.WorldToViewportPoint(transform.position);
        if (playerOnScreen.x < 0f)                                          // LEFT LIM
        {
            _isLeftX = true;
            if (_horizontal > 0f)
            {
                _isLeftX = false;
            }
        }
        else
        {
            _isLeftX = false;
            if (!_isRigthX)
            {
                _playerLimX = transform.position.x;
            }
        }
         if (playerOnScreen.x > 1f)                                         //RiGHT LIM
         {
             _isRigthX = true;
            if (_horizontal < 0f)
            {
                _isRigthX = false;
            }
         }
         else
         {
             _isRigthX = false;
            if (!_isLeftX)
            {
                _playerLimX = transform.position.x;
            }
        }
        if (_isLeftX && !_isRigthX)
        {
            transform.position = new Vector3(_playerLimX + -_offsideOffset, transform.position.y);
        }
        if (_isRigthX && !_isLeftX)
        {
            transform.position = new Vector3(_playerLimX + _offsideOffset, transform.position.y);
        }


        if (playerOnScreen.y > 1f)                                                  //TOP LIM
        {
            _isTopY = true;
            if (_vertical < 0f)
            {
                _isTopY = false;
            }
        }
        else
        {
            _isTopY = false;
            if (!_isBottomY)
            {
                _playerLimY = transform.position.y;
            }
        }
        if (playerOnScreen.y < 0f)                                                  //BOTTOM LIM
        {
            _isBottomY = true;
            if (_vertical > 0f)
            {
                _isBottomY = false;
            }
        }
        else
        {
            _isBottomY = false;
            if (!_isTopY)
            {
                _playerLimY = transform.position.y;
            }

        }
        if (_isTopY && !_isBottomY)
        {
            transform.position = new Vector3(transform.position.x, _playerLimY + _offsideOffset);
        }
        if(_isBottomY && !_isTopY)
        {
            transform.position = new Vector3(transform.position.x, _playerLimY + -_offsideOffset);
        }
        
    }
    void PlayerKeyBoardControl()
    {
        _rB.velocity = new Vector2(_horizontal * speed * speedMultiplier * Time.fixedDeltaTime, _vertical * speed * speedMultiplier * Time.fixedDeltaTime);
    }

    void PlayerAltControl()
    {
        float m_Speed = 1f;
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rB.velocity = (Vector2)transform.up * speed * m_Speed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rB.velocity = (Vector2)(-transform.up) * speed * m_Speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _rB.velocity = Vector2.left * speed * m_Speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _rB.velocity = Vector2.right * speed * m_Speed;
        }
    }
    void PlayerBoost()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }
    }
}
