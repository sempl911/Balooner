using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class BossCaveController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float _detectDistance = 10f;
    [SerializeField] int _bossPhase = 0;

    Animator bossCaveAnimator;

    float _timeToEndPhase = 4f;
    float _timeToChangePhase = 4f;
    bool _isGunAtack;
    public bool isGunAtack
    {
        get => _isGunAtack;
    }
    private void Start()
    {
        bossCaveAnimator = gameObject.GetComponent<Animator>();
        bossCaveAnimator.SetBool("BossWakeUp", true);
    }
    private void Update()
    {
        _timeToEndPhase -= Time.fixedDeltaTime;
        _timeToChangePhase -= Time.fixedDeltaTime;

        // TIME TO END PHASE BOSS
        if (_timeToEndPhase <= 0)
        {
            BossGunPhaseUpEnd();
            BossEndRocketAtack();
            BossManeurEnd();
        }
        // TIME TO CHANGE PHASE
        if (_timeToChangePhase <=0)
        {
            _timeToChangePhase = 4;
            _bossPhase++;
            if (_bossPhase > 3)
            {
                _bossPhase = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _timeToEndPhase = 3f;
            BossGunPhaseUp();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _timeToEndPhase = 2f;
            BossRocketAtack();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            _timeToEndPhase = 2f;
            BossManeurStart();
        }
        BossWakeUp();
        ChangeBossPhase();
    }
   void BossWakeUp()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < _detectDistance)
        {
            bossCaveAnimator.SetBool("BossWakeUp", true);
        }
    }
    void BossGunPhaseUp()
    {
        bossCaveAnimator.SetBool("BossUp", true);
        bossCaveAnimator.SetBool("GunAtack", true);
        _isGunAtack = true;
    }
    void BossGunPhaseUpEnd()
    {
        bossCaveAnimator.SetBool("GunAtack", false);
        bossCaveAnimator.SetBool("BossUp", false);
        _isGunAtack = false;
    }
    void BossRocketAtack()
    {
        bossCaveAnimator.SetBool("BossDown", true);
        bossCaveAnimator.SetBool("RocketAtack", true);
    }
    void BossEndRocketAtack()
    {
        bossCaveAnimator.SetBool("BossDown", false);
        bossCaveAnimator.SetBool("RocketAtack", false);
    }
    void BossManeurStart()
    {
        bossCaveAnimator.SetBool("BossManeur1", true);
    }
    void BossManeurEnd()
    {
        bossCaveAnimator.SetBool("BossManeur1", false);
    }
    void ChangeBossPhase()
    {
        switch (_bossPhase)
        {
            case 1:
                _timeToEndPhase = 3f;
                BossGunPhaseUp();
                BossEndRocketAtack();
                BossManeurEnd();
                break;
            case 2:
                _timeToEndPhase = 2f;
                BossRocketAtack();
                BossGunPhaseUpEnd();
                BossManeurEnd();
                break;
            case 3:
                _timeToEndPhase = 2f;
                BossManeurStart();
                BossGunPhaseUpEnd();
                BossEndRocketAtack();
                break;
            default:
                BossGunPhaseUpEnd();
                BossEndRocketAtack();
                BossManeurEnd();
                bossCaveAnimator.SetBool("BossWakeUp", true);
                break;
        }
    }
}
