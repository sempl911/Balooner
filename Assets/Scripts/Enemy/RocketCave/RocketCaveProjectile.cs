using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCaveProjectile : MonoBehaviour
{
   [SerializeField] private GameObject _modelRocket;
   [SerializeField] private GameObject _flame1;
   [SerializeField] private GameObject _flame2;

   [SerializeField] private float _speed = 3f;
   [SerializeField] private Transform _player;
   [SerializeField] private float _distanceToAtack = 15f;
   [SerializeField] private GameObject _rocletExplotion;

    PlayerHealth _playerDamage;

    private float _direction = -1; //Always left
    private bool _isFacingRight = false;
    Rigidbody2D _rocketRB;
    SpriteRenderer _modelColor;

    Vector3 direction;

    private float _timer = 7f;
    private bool _isAtack = false;
    // Start is called before the first frame update
    void Start()
    {
        _rocketRB = GetComponent<Rigidbody2D>();
        _modelColor = _modelRocket.GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0f)
        {
            BlowUp();
        }
        RocketConditions();
        ProjectileLogic();
        ProjectileRotation();
        FlameFlip();
        Destroy(gameObject, 10f);
    }


    void RocketConditions()
    {
        //Atack
        if (Vector2.Distance(transform.position, _player.transform.position) < _distanceToAtack)
        {
            _isAtack = true;
        }
           
        if (_isAtack)
        {
            ProjectileAtack();
        }
    }


    void BlowUp()
    {
            Destroy(gameObject, .1f);

            var shootExplotion = Instantiate(_rocletExplotion,_modelRocket.transform.position, Quaternion.identity);
            Destroy(shootExplotion, .4f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerDamage.ReduceDamage(25f);
            var shootExplotion = Instantiate(_rocletExplotion, _modelRocket.transform.position, Quaternion.identity);
            Destroy(shootExplotion, .4f);
            Destroy(gameObject, .1f);
        }
    }
    void ProjectileLogic()
    {
        if (!_isAtack)
        {
            _rocketRB.velocity = new Vector3(_direction * _speed * Time.fixedDeltaTime, _rocketRB.velocity.y);
            _timer = 7f;
        }
    }

    void ProjectileAtack()
    {
        // Atack

        if (_isAtack)
        {
            _speed = 53f;
            direction = _player.transform.position - _modelRocket.transform.position;
            _rocketRB.velocity = new Vector3(direction.x * _speed * Time.fixedDeltaTime, direction.y * _speed * Time.fixedDeltaTime);
            // ROTATION
            // _modelRocket.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_player.transform.position.y - _modelRocket.transform.position.y, _player.transform.position.x - _modelRocket.transform.position.x) * Mathf.Rad2Deg - 1);
            ChangeModelColor();
        }
    }

    void ProjectileRotation()
    {
        if (transform.position.x > _player.transform.position.x && _isFacingRight)
        {
            Flip();
            _isFacingRight = false;
        }
        if (transform.position.x < _player.transform.position.x && !_isFacingRight)
        {
            Flip();
            _isFacingRight = true;
        }
        
    }
    void FlameFlip()
    {
        //Rotation flame
        if (!_isFacingRight)
        {
            _flame1.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
            _flame2.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
        }
        if (_isFacingRight)
        {
            _flame1.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
            _flame2.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
        }
    }
    void ChangeModelColor()
    {
        var modelColor = _modelColor.color;
        float changeRcolor = .4f;
        modelColor.r += changeRcolor * Time.fixedDeltaTime;
        modelColor.g -= changeRcolor * Time.fixedDeltaTime;
        modelColor.b -= changeRcolor * Time.fixedDeltaTime;
        _modelColor.color = modelColor;
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = _modelRocket.transform.localScale;
        playerScale.x *= -1;
        _modelRocket.transform.localScale = playerScale;
    }
}
