using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
     [SerializeField] private GameObject _startLaser;
     [SerializeField] private GameObject _endLaser;

    private float _laserSpeed = .3f;
    private LineRenderer _laserRenderer;
    private LaserAtack _laserGuides;
    private LaserHealth _laserCurrentHealth;
    Vector3 startShot, endShot;


    float _timer = .05f;
    bool continueLaser = false;

    private void Start()
    {
        _laserGuides = GameObject.Find("Laser").GetComponent<LaserAtack>();
        _laserCurrentHealth = FindAnyObjectByType<LaserHealth>();

        _laserRenderer = GetComponent<LineRenderer>();

        transform.position = _laserGuides.shotPoint.transform.position;

        startShot = _laserGuides.shotPoint.transform.position;
        endShot = _laserGuides.shotEndPoint.transform.position;

        _startLaser.transform.position = startShot;
        _endLaser.transform.position = startShot;

        // Laser parametres
        _laserRenderer.startWidth = .1f;
        _laserRenderer.endWidth = .1f;
        _laserRenderer.startColor = Color.red;
        _laserRenderer.endColor = Color.red;
        _laserRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (_laserCurrentHealth.LaserCurrentHealth > 0f)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0f)
            {
                continueLaser = true;
                _timer = .05f;
            }
            if (!continueLaser)
            {
                _endLaser.transform.position = startShot;
            }
            startShot = _laserGuides.shotPoint.transform.position;
            endShot = _laserGuides.shotEndPoint.transform.position;

            Shot();
        }       
    }

    private void LateUpdate()
    {
        _laserRenderer.SetPosition(0, _startLaser.transform.position);
        _laserRenderer.SetPosition(1, _endLaser.transform.position);
    }

    void Shot()
    {
   
        Vector3 destination = endShot - startShot;

        _startLaser.transform.position = new Vector3(_startLaser.transform.position.x + destination.x * _laserSpeed * Time.deltaTime,_startLaser.transform.position.y + destination.y * _laserSpeed * Time.deltaTime);
        if (continueLaser)
        {
            _endLaser.transform.position = new Vector3(_endLaser.transform.position.x + destination.x * _laserSpeed * Time.deltaTime, _endLaser.transform.position.y + destination.y * _laserSpeed * Time.deltaTime);
        }
        
        Destroy(gameObject, 2f);
    }
}
