using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private FightersList _fightersList = null;
    
    private bool _isPlaying;
    private bool _isHidding;
    private bool _isTarget;
    private bool _isDead;
    private bool _isWinned;

    public event Action Playing;

    public event Action<Fighter> Targeted;

    public event Action Untargeted;

    public event Action Hidding;

    public event Action Winned;

    public Vector3 Position => transform.position;

    public bool CanTarget => _isHidding == false && _isTarget == false;
    public bool CanUntarget => _isHidding == false && _isTarget;

    public void Play()
    {
        if (_isPlaying)
            throw new InvalidOperationException(nameof(Play));

        _isPlaying = true;

        Playing?.Invoke();
    }

    public void Target(Fighter fighter)
    {
        if (CanTarget == false)
            throw new InvalidOperationException(nameof(Target));

        _isTarget = true;

        Targeted?.Invoke(fighter);
    }

    public void Untarget()
    {
        if (_isTarget == false)
            throw new InvalidOperationException(nameof(Untarget));

        if (_isHidding)
            throw new InvalidOperationException(nameof(Untarget));

        _isTarget = false;

        Untargeted?.Invoke();
    }

    public void Hide()
    {
        if (_isPlaying == false)
            throw new InvalidOperationException(nameof(Hide));

        if (_isHidding)
            throw new InvalidOperationException(nameof(Hide));

        _isHidding = true;

        _fightersList.Remove(this);
        Hidding?.Invoke();
    }

    public void Die()
    {
        if (_isHidding == false)
            throw new InvalidOperationException(nameof(Die));
        
        Destroy(gameObject);
    }

    public void Win()
    {
        if (_isWinned)
            throw new InvalidOperationException(nameof(Win));

        _isWinned = true;

        Winned?.Invoke();
    }
}
