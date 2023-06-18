using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftController : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _player;
    
    private float _vertical;
    private float _horizontal;

    private void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        //ControlPlayer();
        PlayerAltControl();
    }
    void PlayerAltControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _model.transform.position = new Vector2(1 * _speed, _model.transform.position.y);        }

       /* if (Input.GetKeyDown(KeyCode.S))
        {
            _playerRb.velocity = (Vector2)(-transform.up) * _speed * m_Speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _playerRb.velocity = Vector2.left * _speed * m_Speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _playerRb.velocity = Vector2.right * _speed * m_Speed;
        }*/
    }
}
