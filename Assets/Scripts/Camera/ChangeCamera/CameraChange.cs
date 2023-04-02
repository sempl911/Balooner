using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float camLeftLimit = 102f;
    [SerializeField] private float camRigthLimit;
    [SerializeField] private float camTopLimit;
    [SerializeField] private float camBottmoLimit;

    private float _playerCurrentPosition;
    //private float _changeZoom = 20f;
    private bool _isPlayerNear;

    CameraFollow cameraFollow;
    PlayerHealth _playerHealth;
    Vector3 playerDirection;
    RaycastHit2D raycastHit2D;

    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealth.Health > 0f)
        {
            FindPlayer();
            ChekWherePlayer();
        }
        else
        {
            return;
        }
    }
    void FindPlayer()
    {
            // Find curret player position
            playerDirection = player.transform.position;
            _playerCurrentPosition = playerDirection.y - transform.position.y;

            raycastHit2D = Physics2D.Raycast(transform.position, transform.right, 50f);
            if (raycastHit2D.collider != null)
            {
                if (raycastHit2D.collider.gameObject.GetComponent<PlayerControler>())
                {
                    if (_isPlayerNear)
                    {
                        ChangeCamera();
                    }
                }
            }
    }
    void ChangeCamera()
    {
        if (_playerCurrentPosition < 0f)
        {
            //cameraFollow.zoom = -_changeZoom;
            cameraFollow.ChangeCameraLimits(-2.8f, 165f, 20f, 0f);
        }
        if (_playerCurrentPosition > 0f)
        {
            //cameraFollow.zoom = _changeZoom;
            cameraFollow.ChangeCameraLimits(camLeftLimit, camRigthLimit, camTopLimit, camBottmoLimit);
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
