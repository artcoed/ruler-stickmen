using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FighterMovement : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FighterAnimator _animator = null;
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [SerializeField] private float _stopDistance = 2f;

    private Coroutine _targeting;

    public event Action Reached;

    private void Awake()
    {
        _fighter.Targeted += OnTargeted;
        _fighter.Hidding += StopTargeting;
        _fighter.Winned += OnWinned;
    }

    private void OnDestroy()
    {
        _fighter.Targeted -= OnTargeted;
        _fighter.Hidding -= StopTargeting;
        _fighter.Winned -= OnWinned;
    }

    private void OnTargeted(Fighter target)
    {
        StopTargeting();
        _targeting = StartCoroutine(Targeting(target, _stopDistance));
    }

    private void OnWinned()
    {
        StopTargeting();
        _animator.Stay();
    }

    private void StopTargeting()
    {
        if (_targeting != null)
            StopCoroutine(_targeting);
    }

    private IEnumerator Targeting(Fighter target, float stopDistance = 0)
    {
        _animator.Run();
        _navMeshAgent.SetDestination(target.transform.position);

        while (true)
        {
            while (_navMeshAgent.pathPending)
            {
                yield return null;
            }

            if (_navMeshAgent.remainingDistance <= stopDistance)
            {
                break;
            }

            _navMeshAgent.SetDestination(target.transform.position);
            yield return null;
        }

        _navMeshAgent.ResetPath();
        _animator.Stay();
        Reached?.Invoke();
        yield return null;
    }
}
