using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperProjectile : MonoBehaviour
{
    private Transform player;

    Rigidbody2D _projectileRB;
    PlayerHealth healthPlayer;
    private float _liveTime = 3f;

    const float _speedMultiplier = 30f;

    private void Start()
    {
        _projectileRB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        healthPlayer =GameObject.Find("Player").GetComponent<PlayerHealth>();
        AtackSide();
    }
    private void FixedUpdate()
    {
        Destroy(gameObject, _liveTime);
    }

    void AtackSide()
    {
        if (player.transform.position.x < transform.position.x)
        {
            _projectileRB.AddForce(Vector2.left * 250f * _speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if (player.transform.position.x > transform.position.x)
        {
          _projectileRB.AddForce(Vector2.right * 250f * _speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthPlayer.ReduceDamage(10f);
            Destroy(gameObject);
        }
        
    }
}
