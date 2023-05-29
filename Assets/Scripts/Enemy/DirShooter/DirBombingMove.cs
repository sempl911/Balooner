using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirBombingMove : MonoBehaviour
{
    public float startWhaitTime;
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] moveSpots;

    DirShooterController _dirAtackMode;

    private int _randomStpots;
    private float _whaitTime;

    private void Start()
    {
        _whaitTime = startWhaitTime;
        _randomStpots = Random.Range(0, moveSpots.Length);
        _dirAtackMode = gameObject.GetComponent<DirShooterController>();
    }
    private void Update()
    {
        if (_dirAtackMode.isBombAtack)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[_randomStpots].position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[_randomStpots].position) < .1f)
            {
                if (_whaitTime <= 0)
                {
                    _randomStpots = Random.Range(0, moveSpots.Length);
                    _whaitTime = startWhaitTime;
                }
                else
                {
                    _whaitTime -= Time.deltaTime;
                }
            }
        }
    }

}
