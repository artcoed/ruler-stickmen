using System;
using UnityEngine;

public class FighterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    private const string RunParameter = "Run";
    private const string AttackFistParameter = "FistAttack";
    private const string AttackSwordParameter = "SwordAttack";
    private const string AnyAttackParameter = "AnyAttack";

    private string _currentAttackParameter;

    public float AnimationSeconds => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

    private void Awake()
    {
        SwitchAttackAnimation(AttackAnimationType.Fist);
    }

    public void Run()
    {
        _animator.SetBool(AnyAttackParameter, false);
        _animator.SetBool(RunParameter, true);
    }

    public void Attack()
    {
        _animator.SetBool(AnyAttackParameter, true);
        _animator.SetTrigger(_currentAttackParameter);
    }

    public void Stay()
    {
        _animator.SetBool(AnyAttackParameter, false);
        _animator.SetBool(RunParameter, false);
    }

    public void SwitchAttackAnimation(AttackAnimationType type)
    {
        switch (type)
        {
            case AttackAnimationType.Fist:
                _currentAttackParameter = AttackFistParameter;
                break;
            case AttackAnimationType.Sword:
                _currentAttackParameter = AttackSwordParameter;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
