using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private GameObject _magnetoTank;
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private GameObject _tankLight;
    [SerializeField] private GameObject _tankDetCollider;

    [SerializeField] private float leftLim, rightLim;

    TankColliderDetect _tankDet;
    [SerializeField] private float _lightOffset;
    private float _magnetoOffset = -50f;
    private float _speedRotateLight = 1f;
    private bool moveingUp = true;
    private bool _playerInLight = false;
    private float _rotationLight;

    private void Start()
    {
        _tankDet = _tankDetCollider.GetComponent<TankColliderDetect>();
    }

    private void Update()
    {
        RotationMagnetoToPlayer();
        DetectPlayer();
    }
    private void FixedUpdate()
    {
            RotateLightDetectPlayer();
            
        Debug.Log(_playerInLight);
    }
    void DetectPlayer()
    {
        _playerInLight = _tankDet.isPlayerDetect;

        if (_playerInLight)
        {

        }
        if (!_playerInLight)
        {

        }
    }
    void RotateLightDetectPlayer()
    {
        //WALKING
        if (!_playerInLight)
        {
            _rotationLight = _tankLight.transform.rotation.z;

        if (_rotationLight <= -.7f)
        {
            moveingUp = true;
        }
        if (_rotationLight >= .1f)
        {
            moveingUp = false;
        }
        
        if (moveingUp)
        {
             _speedRotateLight += 50 * Time.deltaTime;
        }
        if (!moveingUp)
        {
            _speedRotateLight -= 50 * Time.deltaTime;
        }
            _tankLight.transform.rotation = Quaternion.Euler(0, 0, _speedRotateLight);
        }
        //  ATACKING
        if (_playerInLight)
        {
            Vector3 diff = _playerPos.transform.position - _tankLight.transform.position;
            float rotateZ = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            rotateZ = Mathf.Clamp(rotateZ, rightLim, leftLim);
            Quaternion rotate = Quaternion.Euler(0, 0, -rotateZ + _lightOffset);
            _tankLight.transform.rotation = rotate;
        }
    }

    void RotationMagnetoToPlayer()
    {
        Vector3 moveDirection = _playerPos.transform.position - _magnetoTank.transform.position;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, rightLim, leftLim);
          _magnetoTank.transform.rotation = Quaternion.AngleAxis(angle + _magnetoOffset, Vector3.forward);
        }
    }
}
/*
 * Vector3 relativePos = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(relativePos);*/