using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomChange : MonoBehaviour
{
    public bool isChangeLimits = false;

    [SerializeField] private GameObject player;
    private float _playerCurrentPosition;
    private float _changeZoom = 20f;
    private bool _isPlayerNear;

    CameraFollow cameraFollow;
    Vector3 playerDirection;
    RaycastHit2D hitPlayer;

    private void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
    private void FixedUpdate()
    {
        FindPlayer();
        ChekWherePlayer();
    }
    void FindPlayer()
    {
        // Find curret player position
        playerDirection = player.transform.position;
        _playerCurrentPosition = playerDirection.x - transform.position.x;

        hitPlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 100f);

        if (hitPlayer.collider != null)
        {
            if (_isPlayerNear)
            {
                ChangeCamera();
            }
        }
    }
    void ChangeCamera() // Изменение камеры на объекте
    {
        if (_playerCurrentPosition < 0)
        {
            cameraFollow.zoom = _changeZoom;
            cameraFollow.ChangeCameraLimits(-2.8f, 140f, 3.4f, -1.4f);
        }
        if (_playerCurrentPosition > 0)
        {
            cameraFollow.zoom = - _changeZoom;
            cameraFollow.ChangeCameraLimits(-2.8f, 165f, 6.2f, 0f);
        }
    }
    void ChekWherePlayer() // Отключение изменений камеры
    {
        if (_playerCurrentPosition < -5f || _playerCurrentPosition > 5f)
        {
            _isPlayerNear = false;
        }
        else
        {
            _isPlayerNear = true;
        }
    }
}
