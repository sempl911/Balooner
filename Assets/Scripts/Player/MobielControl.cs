using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobielControl : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Transform playerTransformModel;
    Rigidbody2D _rBmobiel;
    private float speed = 7f;
    private float _horizontal;
    private float _vertical;
    private bool _isFacingRight;

    const float speedMultiplier = 50f;

    private void Start()
    {
        _rBmobiel = GetComponent <Rigidbody2D> ();
        _joystick = FindAnyObjectByType<FloatingJoystick>().GetComponent<FloatingJoystick>();
    }
    private void Update()
    {
        _horizontal = _joystick.Horizontal;
        _vertical = _joystick.Vertical;
    }
    private void FixedUpdate()
    {
        _rBmobiel.velocity = new Vector2(_horizontal * speed * speedMultiplier * Time.fixedDeltaTime, _vertical * speed * speedMultiplier * Time.fixedDeltaTime);

        if (_horizontal > 0 && _isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0 && !_isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerTransformModel.localScale;
        playerScale.x *= -1;
        playerTransformModel.localScale = playerScale;
    }
}
