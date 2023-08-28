using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] Transform bossPoint;
    Transform _playerPos;
    Animator bossAnimator;
    float moveX = 0;
    float moveY = 0;
    [SerializeField, Range(0,1)]float bossMoveSpeed = .1f;
    void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            moveX += .1f;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            moveX -= .1f;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            moveY += .1f;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            moveY -= .1f;
        }
        PlayerDetect();
        bossAnimator.SetFloat("MoveX", moveX);
        bossAnimator.SetFloat("MoveY", moveY);
    }

    void PlayerDetect()
    {
        if (_playerPos.position.y > bossPoint.position.y)
        {
            moveY = 0;
            moveY += bossMoveSpeed;
        }
        if (_playerPos.position.y < bossPoint.position.y)
        {
            moveY = 0;
            moveY -= bossMoveSpeed;
        }
        if (_playerPos.position.x > bossPoint.position.x)
        {
            moveX = 0;
            moveX += bossMoveSpeed;
        }
        if (_playerPos.position.x < bossPoint.position.x)
        {
            moveX = 0;
            moveX -= bossMoveSpeed;
        }
    }
}
