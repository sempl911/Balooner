using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange_1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float camLeftLimit;
    [SerializeField] private float camRigthLimit;
    [SerializeField] private float camTopLimit = 20f;
    [SerializeField] private float camBottmoLimit;

    private float _playerCurrentPosition;
    private bool _isPlayerNear;

    PlayerHealth _playerHealth;
    CameraFollow cameraFollow;
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
        if (_playerHealth.Health > 0)
        {
            FindPlayer();
            ChekWherePlayer();
        }
    }
    void FindPlayer()
    {
        // Find curret player position
        playerDirection = player.transform.position;
        _playerCurrentPosition = playerDirection.x - transform.position.x;

        raycastHit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 40f);
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
            cameraFollow.ChangeCameraLimits(-2.8f, 165f, 6.2f, 0f);
        }
        if (_playerCurrentPosition > 0f)
        {
            cameraFollow.ChangeCameraLimits(camLeftLimit, camRigthLimit, camTopLimit, camBottmoLimit);
        }
    }
    void ChekWherePlayer() // Отключение изменений камеры
    {
        if (_playerCurrentPosition < -1f || _playerCurrentPosition > 1f)
        {
            _isPlayerNear = false;
        }
        else
        {
            _isPlayerNear = true;
        }
    }
}
