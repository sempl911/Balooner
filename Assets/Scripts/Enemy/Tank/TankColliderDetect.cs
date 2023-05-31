using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankColliderDetect : MonoBehaviour
{
    private bool _isPlayerDetect;
    public bool isPlayerDetect
    {
        get => _isPlayerDetect;
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
        }
    }
}
