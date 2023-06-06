using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankColliderDetect : MonoBehaviour
{
    private bool _isPlayerDetect;
    private bool _isPlayerStay = false;
    public bool isPlayerDetect
    {
        get => _isPlayerDetect;
    }
    public bool isPlayerStay
    {
        get => _isPlayerStay;
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            _isPlayerStay = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerStay = true;
        }
    }
}
