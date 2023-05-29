using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplitBlink : MonoBehaviour
{
    SpriteRenderer _objectRenderer;

    private void Start()
    {
        _objectRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        ChangeOpacity();
    }
    void ChangeOpacity() 
    {
        var objColor = _objectRenderer.color;
        float changeRcolor = .2f;
        objColor.a -= changeRcolor * Time.fixedDeltaTime;
        _objectRenderer.color = objColor;
    }
}
