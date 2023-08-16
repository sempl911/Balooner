using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class BossCaveController : MonoBehaviour
{
    [SerializeField] GameObject balancePoint;
    [SerializeField] GameObject playerModel;
    private bool _playerUp;
    private bool _playerDown;
    Animator bossAnimator;
    private void Start()
    {
        bossAnimator = gameObject.GetComponent<Animator>();
    }
    private void Update()
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
        DetectPlayer();
        BossMove();
    }
    void DetectPlayer()
    {
        if(playerModel.transform.position.y > balancePoint.transform.position.y)
        {
            _playerUp = true;
            _playerDown = false;
        }
        if (playerModel.transform.position.y < balancePoint.transform.position.y)
        {
            _playerDown = true;
            _playerUp = false;
        }
    }
    void BossMove()
    {
        if (_playerUp)
        {
            bossAnimator.SetBool("UpBool", false);
            bossAnimator.SetBool("DownBool", true);
        }
        if (_playerDown)
        {
            bossAnimator.SetBool("DownBool", false);
            //bossAnimator.SetFloat("SpeedUp", 1f);
            bossAnimator.SetBool("UpBool", true);
        }
    }
}
