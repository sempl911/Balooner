using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Обязательный запрос компонента

public class ShadowEffectBossBody : MonoBehaviour
{
    public Vector3 Offset = new Vector3(-.1f, -.1f);
    public Material Material;
    GameObject _shadow;
    void Start()
    {
        _shadow = new GameObject("shadow");
        _shadow.transform.parent = transform;

        _shadow.transform.localPosition = Offset;
        _shadow.transform.localRotation = Quaternion.identity;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = _shadow.AddComponent<SpriteRenderer>();
        sr.sprite = renderer.sprite;
        sr.material = Material;

        // Указываем рендер всегда сзади объекта
        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = renderer.sortingOrder - 1;

    }

     void LateUpdate()
    {
        _shadow.transform.localPosition = Offset;
         
    }
}
