using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class BossCaveController : MonoBehaviour
{
    [SerializeField] GameObject balancePoint;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject bossBody;
    private bool _playerDetect;
    Animator bossAnimator;

    [SerializeField] private float _offset = 0;
    [SerializeField] private float _distanceDetect = 20;

    private void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        TurnToPlayer();
        DetectPlayer();
    }
    void TurnToPlayer()
    {
        if (_playerDetect)
        {
            Vector3 diff = playerModel.transform.position - bossBody.transform.position;
            float rotateZ = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            Quaternion rotate = Quaternion.Euler(0, 0, - rotateZ + _offset);
            bossBody.transform.rotation = rotate;
        }
    }
    void DetectPlayer()
    {
        if (Vector2.Distance(playerModel.transform.position, transform.position) < _distanceDetect)
        {
            _playerDetect = true;
        }
        else
        {
            _playerDetect = false;
        }
    }
    void BossMove()
    {
       /* if (_playerUp)
        {
            bossAnimator.SetBool("UpBool", false);
            bossAnimator.SetBool("DownBool", true);
        }
        if (_playerDown)
        {
            bossAnimator.SetBool("DownBool", false);
            //bossAnimator.SetFloat("SpeedUp", 1f);
            bossAnimator.SetBool("UpBool", true);
        }*/
    }
    void AnimKeyRun()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            bossAnimator.SetBool("DownBool", false);
            bossAnimator.SetFloat("SpeedUp", 1f);
            bossAnimator.SetBool("UpBool", true);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            bossAnimator.SetBool("DownBool", false);

            bossAnimator.SetFloat("SpeedUp", -1f);

            bossAnimator.SetBool("UpBool", true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            bossAnimator.SetBool("UpBool", false);

            bossAnimator.SetFloat("SpeedDown", 1f);
            bossAnimator.SetBool("DownBool", true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            bossAnimator.SetBool("UpBool", false);

            bossAnimator.SetFloat("SpeedDown", -1f);
            bossAnimator.SetBool("DownBool", true);
        }
    }
}
