using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftController : MonoBehaviour
{
    Rigidbody2D _objRb;
    float _vertical;
    float _horizontal;
    float speed = 20f;
    private const float speedMultiplier = 20f;
    private void Start()
    {
        _objRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        ControlPlayer();
        Jump();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _objRb.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
        }
    }
    void ControlPlayer()
    {
        _objRb.velocity = new Vector2(_horizontal * speed * speedMultiplier * Time.fixedDeltaTime, _vertical * speed * speedMultiplier * Time.fixedDeltaTime);
    }
}
