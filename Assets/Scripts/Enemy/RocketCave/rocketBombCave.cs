using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketBombCave : MonoBehaviour
{
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _damageZone;

    SpriteRenderer bombRenderer;
    SpriteRenderer damageRenderer;

    bombCaveDamage isCollited;

    private void Start()
    {
        bombRenderer = _bomb.GetComponent<SpriteRenderer>();
        damageRenderer = _damageZone.GetComponent<SpriteRenderer>();
        isCollited = _bomb.GetComponent<bombCaveDamage>();
    }
    private void Update()
    {
        BombColorChange();
        if (isCollited.IsCollited)
        {
            Destroy(gameObject, .1f);
        }
    }
    void BombColorChange()
    {
        var bombColor = bombRenderer.color;
        var damageColor = damageRenderer.color;
        var colorDamageBlinkTo = new Color(.94f, .057f, .057f, .01f);
        var colorDamageBlinkFrom = new Color(1f, 1f, 1f, .36f);

        var colorBombTo = new Color(1f, 1f, 1f);
        var colorBombFrom = new Color(.8f, .79f, .79f,.8f);

        bombColor = Color.Lerp(colorBombFrom, colorBombTo, Mathf.Abs(Mathf.Sin(Time.time)));
        damageColor = Color.Lerp(colorDamageBlinkTo, colorDamageBlinkFrom, Mathf.Abs(Mathf.Sin(Time.time)));

        bombRenderer.color = bombColor;
        damageRenderer.color = damageColor;
    }
}
