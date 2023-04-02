using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _trunk;
    [SerializeField] private float _offsetTrunk;
    [SerializeField] private float _minLim;
    [SerializeField] private float _maxLim;

    private bool _isPlayerDetect;

    public bool IsPlayerDetect
    {
        get => _isPlayerDetect;
    }

    private void FixedUpdate()
    {
        RotateTrunk();
    }
    void RotateTrunk()
    {
        if (_isPlayerDetect)
        {
            Vector3 diff = _player.transform.position - _trunk.transform.position;
            float rotateZ = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            rotateZ = Mathf.Clamp(rotateZ, _minLim, _maxLim);
            Quaternion rotate = Quaternion.Euler(0, 0, -rotateZ + _offsetTrunk);
            _trunk.transform.rotation = rotate;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerDetect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerDetect = false;
        }
    }
}
