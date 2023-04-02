using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelProjectile : MonoBehaviour
{
    [SerializeField] private GameObject modelProjectile;
    [SerializeField] private GameObject projectileExplotion;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _liveTime = 3f;

    private Transform _player;
    private Rigidbody2D _projectileRb;
    private PlayerHealth _damagePlayer;
    private SpriteRenderer _modelRenderer;

    Vector3 direction;
    private bool _isDie;

    const float _speedMultiplier = 50f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _projectileRb = GetComponent<Rigidbody2D>();
        _damagePlayer = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _modelRenderer = modelProjectile.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Timer
        _liveTime -= Time.deltaTime;
        if (_liveTime < 0f)
        {
            _isDie = true;
            _liveTime = 3f;
        }
    }
    void FixedUpdate()
    {
        ProjectileLogic();
        ModelColorChange();
        DestroyAndExplotion();
    }
    void ProjectileLogic()
    {
        // Find and atack
        direction = _player.transform.position - modelProjectile.transform.position;
        _projectileRb.velocity = new Vector3(direction.x * _speed * _speedMultiplier * Time.fixedDeltaTime, direction.y * _speed * _speedMultiplier * Time.fixedDeltaTime);
        // ROTATION
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_player.transform.position.y - transform.position.y, _player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 1);
    }
    void ModelColorChange()
    {
        var modelColor = _modelRenderer.color;
        float changeRcolor = .3f;
        modelColor.r += changeRcolor * Time.fixedDeltaTime;
        modelColor.g -= changeRcolor * Time.fixedDeltaTime;
        modelColor.b -= changeRcolor * Time.fixedDeltaTime;
        _modelRenderer.color = modelColor;
    }
    void DestroyAndExplotion()
    {
        //Destroy and explotion

        if (_isDie)
        {
            Destroy(gameObject);
            Instantiate(projectileExplotion, transform.position, Quaternion.identity);
            _isDie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _damagePlayer.ReduceDamage(10f);
            Destroy(gameObject);
            Instantiate(projectileExplotion, transform.position, Quaternion.identity);
        }
    }
}
