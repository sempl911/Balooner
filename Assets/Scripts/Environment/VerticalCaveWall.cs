using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCaveWall : MonoBehaviour
{
    [SerializeField] Sprite[] damageImage = new Sprite[3];
    [SerializeField] GameObject destroyAnim;
    SpriteRenderer rockSprite;
    
    private int imageIndex;


    private void Start()
    {
        rockSprite = GetComponent<SpriteRenderer>();
        imageIndex = damageImage.Length;
        destroyAnim.SetActive(false);
    }
    private void Update()
    {
        if (imageIndex <=0)
        {
            imageIndex = damageImage.Length;
            rockSprite.sprite = null;
            Destroy(gameObject, .15f);
            destroyAnim.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            Destroy(collision. gameObject);
            imageIndex--;
            rockSprite.sprite = damageImage[imageIndex];
        }
    }
    
}
