using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    [SerializeField] Transform _player;
    Camera _cam;
    CameraFollow _limCam;
    private void Start()
    {
        _cam = Camera.main;
        _limCam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCamera();
        }
    }

    void SetCamera()
    {
        Vector3 playerPos = _player.transform.position;
        //_cam.transform.position = playerPos;
        _limCam.ChangeCameraLimits(playerPos.x - 10f, playerPos.x + 10f, playerPos.y + 10f, playerPos.y -10f);
    }
}
