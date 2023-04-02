using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelAtack : MonoBehaviour
{
    public bool isShootStatus;

    [SerializeField] private GameObject _turelProjectile;
    [SerializeField] private GameObject _shootPoint;
    TurelController _isDetect;

    private float _shootInterval = .5f;
    private float _shootDelay = .7f;

    private void Start()
    {
        _isDetect = GetComponent<TurelController>();
        InvokeRepeating("TurelShot", _shootInterval, _shootDelay);
    }
    private void FixedUpdate()
    {

    }

    void TurelShot()
    {
        if (isShootStatus)
        {
            var shot = Instantiate(_turelProjectile, _shootPoint.transform.position, Quaternion.identity);
        }
    }
 
}
