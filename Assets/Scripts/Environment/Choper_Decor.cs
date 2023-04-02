using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Choper_Decor : MonoBehaviour
{
    private float _scaleX = 2f;
    private float _scaleY = 2f;

    Rigidbody2D rB;
    new SpriteRenderer renderer;
    Camera cam;

    const float speedMultiplier = 30f;


    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _scaleX -= .01f;
        _scaleY -= .01f;
        if (_scaleX <= 1f)
        {
            _scaleX = 1f;
            _scaleY = 1f;
        }

        rB.velocity = new Vector2(3, 0);
        gameObject.transform.localScale = new Vector3(_scaleX, _scaleY);

        Vector3 choperOnScreen = cam.WorldToViewportPoint(transform.position);
        if (choperOnScreen.x > 1f)
        {
            Destroy(gameObject, .3f);
        }
        OpacityChoper();
    }
    void OpacityChoper()
    {
        var color = renderer.color;
        float speedToChangeAlpha = .1f;
        color.a -= speedToChangeAlpha * Time.fixedDeltaTime;
        renderer.color = color;

    }
}
