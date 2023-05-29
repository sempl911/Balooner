using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombCaveDamage : MonoBehaviour
{
    [SerializeField] private GameObject _allBombProjectile;
    [SerializeField] private GameObject _explotionParticle;

    PlayerHealth _playerDamage;
    private bool _isCollited = false;

    public bool IsCollited
    {
        get => _isCollited;
    }

    private void Start()
    {
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void BombExplotion()
    {
       var bombExp = Instantiate(_explotionParticle, transform.position, Quaternion.identity);
        Destroy(bombExp, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollited = true;
            _playerDamage.ReduceDamage(20f);
            BombExplotion();
        }
    }
}
