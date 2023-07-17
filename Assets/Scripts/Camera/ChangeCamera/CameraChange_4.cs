using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange_4 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float camLeftLimit = 90f;
    [SerializeField] private float camRigthLimit = 320f;
    [SerializeField] private float camTopLimit = 22.5f;
    [SerializeField] private float camBottomLimit;

    private float _playerCurrentPosition;
    private float _changeZoom = 20f;
    private bool _isPlayerNear;

    CameraFollow cameraFollow;
    Vector3 playerDirection;
    RaycastHit2D raycastHit2D;

    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        ChekWherePlayer();
    }
    void FindPlayer()
    {
        // Find curret player position
        playerDirection = player.transform.position;
        _playerCurrentPosition = playerDirection.x - transform.position.x;

        raycastHit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 200f);
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
            cameraFollow.zoom = _changeZoom;
            cameraFollow.ChangeCameraLimits(camLeftLimit, camRigthLimit, camTopLimit, camBottomLimit);
        }
        if (_playerCurrentPosition > 0f)
        {
            cameraFollow.zoom = -_changeZoom;
            cameraFollow.ChangeCameraLimits(90f, 350f, 14f, 2.4f);
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
