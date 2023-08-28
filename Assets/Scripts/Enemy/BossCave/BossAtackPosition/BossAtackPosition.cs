using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtackPosition : MonoBehaviour
{
    [SerializeField] Transform _playerPos1;
    [SerializeField] Transform _playerPos2;
    [SerializeField] Transform _playerPos3;

    [SerializeField] Transform _player;

    Animator bossAnimator;
    PlayerControler _playerInput;

    Vector2 playerMoveDirection;
    bool _isPos1;
    bool _isPos2;
    bool _isPos3;

    private void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
        _playerInput = GameObject.Find("Player").GetComponent<PlayerControler>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        AnimGreatUp();
        AnimGreatDown();
        AnimMidleOne();
        AnimMiddleTwo();
        PlayerDirection();
       // AnimateBoss();
    }
    void AnimGreatUp()
    {
        if (_player.position.y > _playerPos1.position.y)
        {
            Debug.Log("Great Up");
        }
    }
    void AnimGreatDown()
    {
        if (_player.position.y < _playerPos3.position.y)
        {
            Debug.Log("Great Down");
        }
    }
    void AnimMidleOne()
    {
        if (_player.position.y < _playerPos1.position.y && _player.position.y > _playerPos2.position.y)
        {
            Debug.Log("Middle One");
        }
    }
    void AnimMiddleTwo()
    {
        if (_player.position.y > _playerPos3.position.y && _player.position.y < _playerPos2.position.y)
        {
            Debug.Log("Middle Two");
        }
    }
    void PlayerDirection()
    {
        playerMoveDirection = new Vector2(_playerInput.InputVertical, _playerInput.InputHorizontal).normalized;

    }
    void AnimateBoss()
    {
        bossAnimator.SetFloat("MoveX", playerMoveDirection.x);
        bossAnimator.SetFloat("MoveY", playerMoveDirection.y);
    }
}
