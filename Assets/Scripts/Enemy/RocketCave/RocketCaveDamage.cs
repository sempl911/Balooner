using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCaveDamage : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private GameObject _allRocketCave;
    [SerializeField] private GameObject _rocketDamage;
    [SerializeField] private GameObject _caveDamaged;

    private void Start()
    {
        _caveDamaged.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
           ReduceDamage(20f);
        }
    }

    void ReduceDamage(float damage)
    {
        totalHealth -= damage;
        Vector3 damagePos = new Vector3(transform.position.x - 1f, transform.position.y);
        var RocketDamageTmp = Instantiate(_rocketDamage, damagePos, Quaternion.identity);
        Destroy(RocketDamageTmp, .7f);
        if (totalHealth < 0f)
        {
            Destroy(_allRocketCave);
            _caveDamaged.SetActive(true);
        }
       
    }

}
