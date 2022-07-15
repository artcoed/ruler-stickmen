using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private Fighter _startTarget = null;

    private bool _isHidding;

    public event Action Hidding;

    public event Action<Fighter> Targeted;

    private void Start()
    {
        if (_startTarget != null)
            Targeted?.Invoke(_startTarget);
    }

    public void Hide()
    {
        if (_isHidding)
            throw new InvalidOperationException(nameof(Hide));

        _isHidding = true;

        Hidding?.Invoke();
    }

    public void Die()
    {
        if (_isHidding == false)
            throw new InvalidOperationException(nameof(Die));

        Destroy(gameObject);
    }
}
