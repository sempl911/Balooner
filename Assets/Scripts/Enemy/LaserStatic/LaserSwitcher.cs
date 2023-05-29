using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitcher : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _laserL1;
    [SerializeField] private GameObject _laserL2;
    [SerializeField] private GameObject _laserL3;
    [SerializeField] private GameObject _laserL4;
    [SerializeField] private GameObject _laserStaticAllObjects;
    LaserStaticController laserTakePosition;

    private bool _enableLaser;
    private bool _updateLaser;
    private bool _disableLaser;

    private float _timerChangeLaserLine = 1f;
    private int laserIndex = 0;

    public bool enableLaser
        {
        get => _enableLaser; 
    }
    public bool updateLaser
        {
        get => _updateLaser; set { bool upLaser = _updateLaser; }
        }
    public bool disableLaser
        {
        get => _disableLaser; 
    }
    void Start()
    {
        _enableLaser = false;
        _laserStaticAllObjects.SetActive(false);
    }

    void Update()
    {
        LaserSwitch();
        LaserLogic();
        _timerChangeLaserLine -= Time.deltaTime;
        if (_timerChangeLaserLine < 0)
        {
            laserIndex++;
            _timerChangeLaserLine = 1f;
        }
        if (laserIndex > 5)
        {
            laserIndex = 0;
        }
    }
     void LaserSwitch()
    {
        if (_player.position.x < transform.position.x)
        {
            _disableLaser = true;
            _enableLaser = false;
            _updateLaser = false;
        }
        if (_player.position.x > transform.position.x)
        {
            _laserStaticAllObjects.SetActive(true);
            _enableLaser = true;
            _disableLaser = false;
        }
        if (_player.position.x - transform.position.x > 1f)
        {
            _enableLaser = false;
            _updateLaser = true;
        }
    }
    void LaserLogic()
    {
        switch (laserIndex)
        {
            case 1:
                _laserL1.SetActive(false);
                _laserL2.SetActive(true);
                _laserL3.SetActive(true);
                _laserL4.SetActive(true);
                break;
            case 2:
                _laserL1.SetActive(true);
                _laserL2.SetActive(false);
                _laserL3.SetActive(true);
                _laserL4.SetActive(true);
                break;
            case 3:
                _laserL1.SetActive(true);
                _laserL2.SetActive(true);
                _laserL3.SetActive(false);
                _laserL4.SetActive(true);
                break;
            case 4:
                _laserL1.SetActive(true);
                _laserL2.SetActive(true);
                _laserL3.SetActive(true);
                _laserL4.SetActive(false);
                break;
            default:
                _enableLaser = true;
                _laserL1.SetActive(true);
                _laserL2.SetActive(true);
                _laserL3.SetActive(true);
                _laserL4.SetActive(true);
                break;
        }
    }
}
