using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStaticController : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform _startFirePoint;
    [SerializeField] private Transform _endFirePoint;
    [SerializeField] private GameObject startVFX;// Particle start Effect
    [SerializeField] private GameObject endVFX;// Particle end Effect
    private Transform _player;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    LaserSwitcher _laserSwitcher;
    PlayerHealth _playerDamage;

    private float _damageLaserTime = .5f;
    private bool _isDamage = false;

    void Start()
    {
        _laserSwitcher = FindAnyObjectByType<LaserSwitcher>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerHealth>();
        FillLists();
        DisableLaser();
    }

    void Update()
    {
        _damageLaserTime -= Time.deltaTime;
        if (_damageLaserTime < 0)
        {
            _damageLaserTime = .5f;
            _isDamage = true;
        }
        if (_laserSwitcher.enableLaser)
        {
            EnableLaser();
        }
        if (_laserSwitcher.updateLaser)
        {
            UpdateLaser();
        }
        if (_laserSwitcher.disableLaser)
        {
            DisableLaser();
        }

       /* if (Input.GetKeyDown(KeyCode.T))
        {
            EnableLaser();
        }
        if (Input.GetKey(KeyCode.T))
        {
            UpdateLaser();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            DisableLaser();
        }*/
    }
    void  EnableLaser()
    {
        line.enabled = true;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play(); 
        }
    }
    void UpdateLaser()
    {
        line.SetPosition(0, (Vector2)_startFirePoint.position);
        startVFX.transform.position = (Vector2)_startFirePoint.position;

        line.SetPosition(1, _endFirePoint.position);
        Vector2 direction = _endFirePoint.position - _startFirePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(/*(Vector2)transform.position*/ _startFirePoint.position, direction.normalized, direction.magnitude);
        if (hit)
        {
            line.SetPosition(1, hit.point);
            DamageLaser();
        }
        endVFX.transform.position = line.GetPosition(1);//Line Renderer last position
    }
    void DisableLaser()
    {
        line.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }
    void DamageLaser()
    {
        if (_isDamage)
        {
            _playerDamage.ReduceDamage(15f);
            _isDamage = false;
        }
    }
    void FillLists()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }
}
