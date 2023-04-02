using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject _turelTrunk;
    [SerializeField] private float _offsetTrunk;

    TurelAtack shotPremission;

    private bool _isPlayerDetect;
    private bool _isFacingRight = true;

    private void Start()
    {
        shotPremission = GetComponent<TurelAtack>();
    }

    private void FixedUpdate()
    {
        RotateTrunk();
    }

    void RotateTrunk()
    {
        if (_isPlayerDetect)
        {
            Vector3 diff = player.transform.position - _turelTrunk.transform.position;
            float rotateZ = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            Quaternion rotate = Quaternion.Euler(0, 0, -rotateZ + _offsetTrunk);
            _turelTrunk.transform.rotation = rotate;
        }
        if (player.transform.position.x > transform.position.x && _isFacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && !_isFacingRight)
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shotPremission.isShootStatus = true;
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
            shotPremission.isShootStatus = false;
        }
    }

    void Flip()
    {
        Vector3 currentScale = _turelTrunk.transform.localScale;
        currentScale.y *= -1;
        _turelTrunk.transform.localScale = currentScale;
        _isFacingRight = !_isFacingRight;
    }
}
