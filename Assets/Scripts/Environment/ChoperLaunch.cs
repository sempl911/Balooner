using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoperLaunch : MonoBehaviour
{
    [SerializeField] private GameObject choper;
    RaycastHit2D raycastHit2D;
    Camera cam;
    private bool _isPlayerPassed;
    private float _timer = 30f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        _isPlayerPassed = true;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0f)
        {
            _isPlayerPassed = true;
            _timer = 30f;
        }


        Vector3 camPosition = cam.ViewportToWorldPoint(transform.position);

        raycastHit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 100f);
        if (raycastHit2D.collider != null)
        {
            if (raycastHit2D.collider.gameObject.GetComponent<PlayerControler>())
            {
                    LaunchChoper();
            }
        }
    }
    void LaunchChoper()
    {
        if (_isPlayerPassed)
        {
            float randomPosY = Random.Range(1, 9);
            Instantiate(choper, new Vector3(transform.position.x - 15f, randomPosY), Quaternion.identity);
            _isPlayerPassed = false;
        }
    }
}
