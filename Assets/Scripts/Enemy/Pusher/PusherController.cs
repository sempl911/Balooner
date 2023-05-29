using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherController : MonoBehaviour
{
    [SerializeField] private GameObject _modelPusher;
    public float speed;
    public int pointOfPatrol;
    public Transform point;
    public float stoppingDistance;

    bool moveingRight;

    private bool _chill = false;
    private bool _angry = false;
    private bool _goBack = false;
    bool _facingRight;
    Transform player;

    public bool AngryCondition
    {
        get => _angry;
    }
    public bool GoBackCondition
    {
        get => _goBack;
    }

    public bool FacingRight
    {
        get => _facingRight;
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < pointOfPatrol && _angry == false)
        {
            _chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            _angry = true;
            _chill = false;
            _goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            _goBack = true;
            _angry = false;
        }


        if (_chill == true)
        {
            Chill();
        }
        else if (_angry == true)
        {
            Angry();
        }
        else if (_goBack == true)
        {
            GoBack();
        }
        FlipEnemyToPlayerElse();
    }
    void Chill()
    {
        if (transform.position.x > point.position.x + pointOfPatrol)
        {
            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - pointOfPatrol)
        {
            moveingRight = true;
        }

        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        speed = 2;
    }
    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 4;
    }
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = 2;
    }
    void FlipEnemyToPlayerElse()
    {
        if (_angry)
        {
            if (transform.position.x < player.transform.position.x && !_facingRight)
            {
                Flip();
            }
            else if (transform.position.x > player.transform.position.x && _facingRight)
            {
                Flip();
            }
        }
        if (_chill)
        {
            if (transform.position.x < point.transform.position.x - pointOfPatrol && !_facingRight)
            {
                Flip();
            }
            else if (transform.position.x > point.transform.position.x + pointOfPatrol && _facingRight)
            {
                Flip();
            }
        }
        if (_goBack)
        {
            if (transform.position.x < point.transform.position.x - pointOfPatrol && !_facingRight)
            {
                Flip();
            }
            else if (transform.position.x > point.transform.position.x + pointOfPatrol && _facingRight)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        Vector3 currentScale = _modelPusher.transform.localScale;
        currentScale.x *= -1;
        _modelPusher.transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }
}
