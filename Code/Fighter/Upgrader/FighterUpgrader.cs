using System;
using UnityEngine;

public class FighterUpgrader : MonoBehaviour
{
    [SerializeField] private FighterProtection _protection = null;
    [SerializeField] private FighterSword _sword = null;
    [SerializeField] private FighterHealth _health = null;
    [SerializeField] private FighterWeapon _weapon = null;
    [SerializeField] private FighterAnimator _animator = null;

    public event Action Upgraded;

    private void Awake()
    {
        _protection.Showed += OnProtectionShowed;
        _sword.Showed += OnSwordShowed;
    }

    private void OnDestroy()
    {
        _protection.Showed -= OnProtectionShowed;
        _sword.Showed -= OnSwordShowed;
    }

    private void OnProtectionShowed()
    {
        _health.Add(_protection.Value);
        Upgraded?.Invoke();
    }

    private void OnSwordShowed()
    {
        _weapon.Add(_sword.Damage);
        _animator.SwitchAttackAnimation(AttackAnimationType.Sword);
        Upgraded?.Invoke();
    }
}
