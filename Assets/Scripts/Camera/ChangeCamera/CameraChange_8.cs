using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange_8 : MonoBehaviour
{
    public bool isZoomBack;

    private GameObject player;
    [SerializeField] private GameObject camPrevies;
    private CameraChange_7 previesCam;
    private CameraFollow zoomChangeLimit;
    Camera cam;
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

    private float _playerCurrentPosition;
    private float _changeZoom = 20f;
    private bool _isPlayerNear;

    CameraFollow cameraFollow;
    Vector3 playerDirection;
    RaycastHit2D raycastHit2D;

    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        previesCam = camPrevies.GetComponent<CameraChange_7>();
        player = GameObject.Find("Player");
        cam = Camera.main;
        zoomChangeLimit = cam.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {

        FindPlayer();
        ChekWherePlayer();
        camBeforeLeft = previesCam.CamAfterLeft;
        camBeforRigth = previesCam.CamAfterRigth;
        camBeforeTop = previesCam.CamAfterTop;
        camBeforeBottom = previesCam.CamAfterBottom;
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
            ZoomBack(-66f);

            cameraFollow.zoom = -_changeZoom;
            cameraFollow.ChangeCameraLimits(camBeforeLeft, camBeforRigth, camBeforeTop, camBeforeBottom);
        }
        if (_playerCurrentPosition > 0f)
        {
            ZoomTempChangeToBoss(-78.5f);
            cameraFollow.zoom = -_changeZoom - 10f;
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

    float ZoomTempChangeToBoss(float tmpZoom)
    {
        zoomChangeLimit.ZoomOutLimit = tmpZoom;
        return zoomChangeLimit.ZoomOutLimit;
    }
    float ZoomBack(float tmpZoom)
    {
        if (isZoomBack)
        {
            do
            {
                zoomChangeLimit.ZoomOutLimit += 1.5f * Time.deltaTime;
            } while (tmpZoom <= zoomChangeLimit.ZoomOutLimit);
        }
       
        return zoomChangeLimit.ZoomOutLimit;
    }
}