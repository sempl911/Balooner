using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject gunProjectile;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private GameObject _lightShot;

    PlayerControler _isFacingRight;

    float _timeToOffLightShot = .1f;

    private void Start()
    {
        _isFacingRight = GameObject.Find("Player").GetComponent<PlayerControler>();
        _lightShot.SetActive(false);
    }

    private void Update()
    {
        _timeToOffLightShot -= Time.deltaTime;
        if (_timeToOffLightShot<=0)
        {
            _timeToOffLightShot = .1f;
            _lightShot.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerShoot();
        }

        // Rotate projectile

           // gunProjectile.transform.rotation = Quaternion.Euler(0f, 0f, 40f);
    }
    void PlayerShoot()
    {
        if (_isFacingRight.isFasingRigth)
        {
            var shoot = Instantiate(gunProjectile, shootPoint.transform.position, Quaternion.Euler(0f, 0f, 0f));
            _lightShot.SetActive(true);
        }
        else
        {
            var shoot = Instantiate(gunProjectile, shootPoint.transform.position, Quaternion.Euler(0f, 0f, 180f));
            _lightShot.SetActive(true);
        }
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
