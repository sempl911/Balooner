using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float zoom = 20f;

    [SerializeField] private float dumping = 1.5f;
    [SerializeField] private Vector3 offset = new Vector3(2f, 1f, -13f);
    [SerializeField] private bool isLeft;
    //Cameras Limit

    [SerializeField] private float _leftLimit = -2.8f;
    [SerializeField] private float _rightLimit = 140f;
    [SerializeField] private float _topLimit = 3.4f;
    [SerializeField] private float _bottomLimit = -1.4f;

    [SerializeField] private float zoomOutLimit = -66f;
    [SerializeField] private float zoomInLimit = -40f;
    // Conected scripts
    private PlayerControler playerControler;
    // Changes in game
    private int _LastX;
    private Transform _player;
    private float _bottomTemp;
    private float _topTemp;
    private float _leftTemp;
    private float _rightTemp;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(Mathf.Abs(offset.x), Mathf.Abs(offset.y), offset.z);
        Finde_player(isLeft);
        playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();
        _bottomTemp = _bottomLimit;
        _topTemp = _topLimit;
        _leftTemp = _leftLimit;
        _rightTemp = _rightLimit;
    }
    public void Finde_player(bool _playerisLeft)
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _LastX = Mathf.RoundToInt(_player.position.x);
        if (_playerisLeft)
        {
            transform.position = new Vector3(_player.position.x - offset.x, _player.position.y - offset.y, transform.position.z);
        }

    }

    private void Update()
    {
        ChangeCameraLimits(_leftLimit, _rightLimit, _topLimit, _bottomLimit);
    }

    void FixedUpdate()
    {
        if (_player)
        {
            int currentX = Mathf.RoundToInt(_player.position.x);
            if (currentX > _LastX) isLeft = false; else if (currentX < _LastX) isLeft = true;
            _LastX = Mathf.RoundToInt(_player.position.x);

            Vector3 target;

            if (isLeft)
            {
                target = new Vector3(_player.position.x - offset.x, _player.position.y - offset.y, transform.position.z + zoom);
            }
            else
            {
                target = new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z + zoom);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
            transform.position = new Vector3
                (
                Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit),
                               Mathf.Clamp(transform.position.y, _bottomLimit, _topLimit), Mathf.Clamp(transform.position.z, zoomOutLimit, zoomInLimit)
                );         
        }
        
    }
   /* void ChangeCameraLimits()
    {
        if (!_zoomChange.isChangeLimits)
        {
            _bottomLimit = 0f;
            _topLimit = 6.2f;
            _rightLimit = 165f;
        }
        else
        {
            _bottomLimit = _bottomTemp;
            _topLimit = _topTemp;
            _rightLimit = _rightTemp;
        }
    }*/
        public (float, float, float, float) ChangeCameraLimits (float leftLim, float rightLim, float topLim, float bottomLim)
        {
            var result = (_leftLimit = leftLim, _rightLimit = rightLim, _topLimit = topLim, _bottomLimit = bottomLim);
            return result;
        }
    
}
