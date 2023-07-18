using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange_7 : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private GameObject camPrevies;
    private CameraChange_6 previesCam;
    //Before
    [SerializeField] private float camBeforeLeft;
    [SerializeField] private float camBeforRigth;
    [SerializeField] private float camBeforeTop;
    [SerializeField] private float camBeforeBottom;

    //After
    [SerializeField] private float camAfterLeft;
    [SerializeField] private float camAfterRigth;
    [SerializeField] private float camAfterTop;
    [SerializeField] private float camAfterBottom;

    //Befor public 
    public float CamBeforeLeft
    {
        get => camBeforeLeft;
    }
    public float CamBeforeRigth
    {
        get => camBeforRigth;
    }
    public float CamBeforeTop
    {
        get => camBeforeTop;
    }
    public float CamBeforeBottom
    {
        get => camBeforeBottom;
    }

    //After public
    public float CamAfterLeft
    {
        get => camAfterLeft;
    }
    public float CamAfterRigth
    {
        get => camAfterRigth;
    }
    public float CamAfterTop
    {
        get => camAfterTop;
    }
    public float CamAfterBottom
    {
        get => camAfterBottom;
    }

    private float _playerCurrentPosition;
    private float _changeZoom = 20f;
    private bool _isPlayerNear;

    CameraFollow cameraFollow;
    Vector3 playerDirection;
    RaycastHit2D raycastHit2D;

    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        previesCam = camPrevies.GetComponent<CameraChange_6>();
        player = GameObject.Find("Player");
        camBeforeLeft = previesCam.CamBeforeLeft;
        camBeforRigth = previesCam.CamBeforeRigth;
        camBeforeTop = previesCam.CamBeforeTop;
        camBeforeBottom = previesCam.CamBeforeBottom;

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
            cameraFollow.ChangeCameraLimits(camBeforeLeft, camBeforRigth, camBeforeTop, camBeforeBottom);
        }
        if (_playerCurrentPosition > 0f)
        {
            cameraFollow.zoom = -_changeZoom;
            cameraFollow.ChangeCameraLimits(camAfterLeft, camAfterRigth, camAfterTop, camAfterBottom);
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
