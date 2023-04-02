using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunProjectile : MonoBehaviour
{
    Rigidbody2D _projectileRb;
    PlayerControler player;

    private float _liveTime = 2f;
    private bool _sideAtack;

    const float _speedMultiplier = 30f;

    private void Start()
    {
        _projectileRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerControler>();
        _sideAtack = player.isFasingRigth;
        var atackVector = Vector2.left;
        if (_sideAtack)
        {
            atackVector = Vector2.left;
        }
        if (!_sideAtack)
        {
            atackVector = Vector2.right;
        }

        _projectileRb.AddForce(atackVector * 250f * _speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        Destroy(gameObject, _liveTime);
    }

}
