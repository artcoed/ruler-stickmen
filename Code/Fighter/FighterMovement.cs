using System;
using System.Collections;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FighterAnimator _animator = null;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotateSpeed = 2f;

    [SerializeField] private float _stopDistance = 2f;

    public event Action Reached;

    private void Awake()
    {
        _fighter.Targeted += OnTargeted;
    }

    private void OnDestroy()
    {
        _fighter.Targeted -= OnTargeted;
    }

    private void OnTargeted(Fighter target)
    {
        StartCoroutine(Targeting(target));
    }

    private IEnumerator Targeting(Fighter target)
    {
        var rotating = StartCoroutine(Rotating(target));
        yield return StartCoroutine(Moving(target, _stopDistance));
        yield return rotating;
        Reached?.Invoke();
    }

    private IEnumerator Moving(Fighter target, float stopDistance = 0)
    {
        _animator.Run();

        while (Vector3.Distance(target.transform.position, transform.position) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        _animator.Stay();
    }

    private IEnumerator Rotating(Fighter target)
    {
        var direction = (target.transform.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(direction);

        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
