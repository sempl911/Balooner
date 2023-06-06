using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TankController : MonoBehaviour
{
    [SerializeField] private GameObject _magnetoTank;
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private GameObject _tankLight;
    [SerializeField] private GameObject _tankDetCollider;
    [SerializeField] private GameObject _illuminations;
    [SerializeField] private GameObject _tankProjectorLight;

    [SerializeField] private float leftLim, rightLim;

    TankColliderDetect _tankDet;
    Animator _atackMagnetoOn;
    Light2D _projectorLight;

    [SerializeField] private float _lightOffset;
    private float _magnetoOffset = -50f;
    private float _speedRotateLight = 1f;
    private bool moveingUp = true;
    private bool _playerInLight = false;
    private float _rotationLight;
    private float _blinkDuration = .3f;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    Color defaultColor;

    private void Start()
    {
        _tankDet = _tankDetCollider.GetComponent<TankColliderDetect>();
        _atackMagnetoOn = gameObject.GetComponent<Animator>();
        _illuminations.SetActive(false);
        _projectorLight = _tankProjectorLight.GetComponent<Light2D>();
        defaultColor = _projectorLight.color;
    }

    private void Update()
    {
        RotationMagnetoToPlayer();
        DetectPlayer();
    }
    private void FixedUpdate()
    {
            RotateLightDetectPlayer();
    }
    void DetectPlayer()
    {
        _playerInLight = _tankDet.isPlayerDetect;

         if (_playerInLight)
         {
            _atackMagnetoOn.SetBool("MagnetoOff", false);
            _atackMagnetoOn.SetBool("MagnetoOn", true);
            _illuminations.SetActive(true);
            TankBlink();
         }
         if (!_playerInLight)
         {
            _atackMagnetoOn.SetBool("MagnetoOn", false);
            _atackMagnetoOn.SetBool("MagnetoOff", true);
            _illuminations.SetActive(false);
            _projectorLight.color = defaultColor;
            _projectorLight.intensity = .3f;
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

    void TankBlink()
    {
        float t = Mathf.PingPong(Time.time, _blinkDuration) / _blinkDuration;
        _projectorLight.color = Color.Lerp(color0, color1, t);
        _projectorLight.intensity = .5f;
    }
}
/*
 * Vector3 relativePos = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(relativePos);*/