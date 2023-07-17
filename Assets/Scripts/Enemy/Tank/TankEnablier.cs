using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnablier : MonoBehaviour
{
    public GameObject tank;
    RaycastHit2D hitPlayer;

    private void Start()
    {
        tank.SetActive(false);
    }

    private void Update()
    {
        EnableTank();
    }
    void EnableTank()
    {
        // Find curret player position
        hitPlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 100f);

        if (hitPlayer.collider != null)
        {
                tank.SetActive(true);
        }
    }
}