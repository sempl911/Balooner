using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private AudioSource bombExplosion;
    public float speed = 3f;
    public float topL = 3f;
    public float bottomL = -2f;
    bool moveing = true;
    private int health = 3;
    Animator bombAnimator;
    SpriteRenderer bombBlink;
    PlayerHealth changeHealth;
    // Start is called before the first frame update
    void Start()
    {
        bombAnimator = GetComponent<Animator>();
        bombBlink = GetComponent<SpriteRenderer>();
        changeHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > topL)
        {
            moveing = false;
        }
        else if (transform.position.y < bottomL)
        {
            moveing = true;
        }
        if (moveing)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        if(health <=0)
        {
            DestroyBomb();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            changeHealth.ReduceDamage(20f);
            DestroyBomb();
        }
        if(collision.gameObject.CompareTag("PlayerProjectile"))
        {
            health--;
            Destroy(collision.gameObject);
            // gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
    private void DestroyBomb()
    {
        bombAnimator.SetTrigger("BombExp");
        bombExplosion.Play();
        Destroy(gameObject,.4f);
    }
    
}
