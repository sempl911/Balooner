using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [SerializeField] GameObject _explotion;
    Rigidbody2D _projectileRb;
    float _randomLifeTime = .5f;
    private float _shotForce = 20f;
    
    void Start()
    {
        _projectileRb = gameObject.GetComponent<Rigidbody2D>();
        _randomLifeTime = Random.Range(.3f, .7f);
        _projectileRb.gravityScale = GaravityProjectile(_projectileRb.gravityScale);
        _projectileRb.AddForce(Vector2.left * _shotForce, ForceMode2D.Impulse);
    }
    // GRAVITY CHANGE!!!!
    void Update()
    {
        _randomLifeTime -= Time.fixedDeltaTime;
      
        LifeCycle(_randomLifeTime);
    }
    void LifeCycle( float lifeTime)
     {
         if (lifeTime <= 0f)
         {
             Destroy(gameObject);
             var gunExp = Instantiate(_explotion, transform.position, Quaternion.identity);
             Destroy(gunExp, .3f);
            _randomLifeTime = Random.Range(.3f, .7f);
        }
    }
    float GaravityProjectile( float gravityScale)
    {
        gravityScale = Random.Range(.7f, 2f);
        return gravityScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            var gunExp = Instantiate(_explotion, transform.position, Quaternion.identity);
            Destroy(gunExp, .3f);
        }
    }
}
