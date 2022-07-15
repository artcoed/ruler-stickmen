using System;
using System.Collections;
using UnityEngine;

public class FighterWeapon : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FighterMovement _movement = null;
    [SerializeField] private FighterAnimator _animator = null;
    [SerializeField] private ParticleSystem _attackParticle = null;
    [SerializeField] private Transform _attackParticlePoint = null;

    [SerializeField] private int _damage = 2;
    [SerializeField] private float _attackParticleSeconds = 2f;

    private FighterHealth _target;

    private Coroutine _attacking;
    private bool _isHidding;

    private void Awake()
    {
        _fighter.Targeted += OnTargeted;
        _movement.Reached += Attack;
        _fighter.Hidding += OnHidding;
    }

    private void OnDestroy()
    {
        _fighter.Targeted -= OnTargeted;
        _movement.Reached -= Attack;
        _fighter.Hidding -= OnHidding;
    }

    public void Add(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _damage += damage;
    }

    private void OnAttack()
    {
        if (_isHidding)
            return;

        _attacking = StartCoroutine(Attacking());
    }

    private void OnTargeted(Fighter target)
    {
        _target = target.GetComponent<FighterHealth>();

        if (_target == null)
            throw new ArgumentException(nameof(target));
    }

    private void OnHidding()
    {
        if (_attacking != null)
            StopCoroutine(_attacking);

        _isHidding = true;
    }

    private void Attack()
    {
        _animator.Attack();
    }

    private IEnumerator Attacking()
    {
        _target.TakeDamage(_damage);
        var particle = Instantiate(_attackParticle, _attackParticlePoint.position, Quaternion.identity);
        Destroy(particle, _attackParticleSeconds);
        yield return new WaitForSeconds(_animator.AnimationSeconds);
        _animator.Stay();

        if (_target.IsAlive)
            Attack();
    }
}
