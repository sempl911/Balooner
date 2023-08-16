using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlerNoPhysic : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private float _horizontal;
    private float _vertical;

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        PlayerMove();
    }
    void PlayerMove()
    {
        transform.position = new Vector3(transform.position.x + _horizontal * _speed, transform.position.y + _vertical * _speed);
    }
}
