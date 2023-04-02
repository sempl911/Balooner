using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherAtack : MonoBehaviour
{
    [SerializeField] private GameObject damagePlatform;
    [SerializeField] private GameObject modelAnim;
    PlayerHealth damagePlayer;
    Animator atackAnim;
    PusherController angryCondition;
    void Start()
    {
        damagePlayer = GameObject.Find("Player").GetComponent<PlayerHealth>();
        atackAnim = modelAnim.GetComponent<Animator>();
        angryCondition = transform.root.GetComponent<PusherController>();
    }
    void Update()
    {
        if (angryCondition.AngryCondition)
        {
            atackAnim.SetTrigger("Atack");
        }
        if (angryCondition.GoBackCondition)
        {
            atackAnim.SetTrigger("EndAtack");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damagePlayer.ReduceDamage(10f);
        }
    }
}
