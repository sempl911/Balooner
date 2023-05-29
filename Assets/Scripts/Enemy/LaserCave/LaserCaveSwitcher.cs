using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCaveSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _laser1;
    [SerializeField] private GameObject _laser2;
    [SerializeField] private GameObject _laser3;
    [SerializeField] private GameObject _laserStaticSwitcher;

    private int _laserIndex = 3;
    private float _timeToChangeLaser = .9f;

    LaserSwitcher _updateLaser;

    private void Start()
    {
        _updateLaser = _laserStaticSwitcher.GetComponent<LaserSwitcher>();
    }
    private void Update()
    {
        LaserCaveLogic();
        UpdateLaser();
        _timeToChangeLaser -= Time.deltaTime;
        if (_timeToChangeLaser < 0f)
        {
            _laserIndex++;
            _timeToChangeLaser = .9f;
        }
        if (_laserIndex > 4)
        {
            _laserIndex = 0;
        }
    }
    void UpdateLaser()
    {
        _updateLaser.updateLaser = true;
    }
    void LaserCaveLogic()
    {
        switch (_laserIndex)
        {
            case 1:
                _laser1.SetActive(false);
                _laser2.SetActive(true);
                _laser3.SetActive(true);
                break;
            case 2:
                _laser1.SetActive(true);
                _laser2.SetActive(false);
                _laser3.SetActive(true);
                break;
            case 3:
                _laser1.SetActive(true);
                _laser2.SetActive(true);
                _laser3.SetActive(false);
                break;
            default:
                _laser1.SetActive(true);
                _laser2.SetActive(true);
                _laser3.SetActive(true);
                break;
        }
    }
}
