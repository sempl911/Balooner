using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _stoppingDistance = 12f;
    [SerializeField] private float _speed = 2f;
    private bool _isFacingRight = true;
    private bool _isPlayerVisibly;
    private bool _isPlayerInStoppingDistance = false;
    Rigidbody2D _rB;
    PlayerHealth _playerHealth;
    public bool isPlayerVisibly
    {
        get => _isPlayerVisibly;
    }
    public bool isFacingRight
    {
        get => _isFacingRight;
    }
    private void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void FixedUpdate()
    {
        if (_playerHealth.Health > 0)
        {
            CheckWherePlayer();
            FlipToPlayer();
        }  
    }


    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = _modelTransform.localScale;
        playerScale.x *= -1;
        _modelTransform.localScale = playerScale;
    }
    void FlipToPlayer()
    {
        if (transform.position.x < _player.position.x && _isFacingRight)
        {
            Flip();
        }
        if (transform.position.x > _player.position.x && !_isFacingRight)
        {
            Flip();
        }
    }
    void CheckWherePlayer()
    {
        if (Vector2.Distance(_player.position, _modelTransform.position) < _viewDistance)
        {
            _isPlayerVisibly = true;
        }
        if (Vector2.Distance(_player.position, _modelTransform.position) > _viewDistance)
        {
            _isPlayerVisibly = false;
        }

        if (_isPlayerVisibly)
        {
            if (!_isPlayerInStoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_modelTransform.position.x, _player.position.y), _speed *Time.fixedDeltaTime);
            }
        }
        if (!_isPlayerVisibly)
        {
           // Debug.Log("I lost player");
        }
        if (Vector2.Distance(_player.position, _modelTransform.position) < _stoppingDistance && _isPlayerVisibly)
        {
            _isPlayerInStoppingDistance = true;
        }
        else
        {
            _isPlayerInStoppingDistance = false;
        }
    }
   
}
