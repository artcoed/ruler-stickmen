using System;
using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;

    [SerializeField] private int _value = 60;

    public event Action Changed;

    public event Action Damaged;

    public bool IsAlive => _value > 0;

    public int Value => _value;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        if (IsAlive == false)
            throw new InvalidOperationException(nameof(TakeDamage));

        _value -= damage;

        if (_value <= 0)
        {
            _value = 0;
            _fighter.Hide();
        }

        Changed?.Invoke();
        Damaged?.Invoke();
    }

    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (IsAlive == false)
            throw new InvalidOperationException(nameof(Add));

        _value += amount;
        Changed?.Invoke();
    }
}
