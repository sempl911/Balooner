using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject gunProjectile;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private AudioSource gunSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerShoot();
        }
    }
    void PlayerShoot()
    {
        var shot = Instantiate(gunProjectile, shootPoint.transform.position, Quaternion.identity);
        gunSound.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChopperEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
