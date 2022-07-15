using System;
using System.Collections;
using UnityEngine;

public class FighterSword : MonoBehaviour
{
    [SerializeField] private GameObject _swordPrefab = null;
    [SerializeField] private Transform _swordPoint = null;

    [SerializeField] private int _damage = 20;

    private bool _isShowing;

    public event Action Showed;

    public int Damage => _damage;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        Show();
    }

    private void Show()
    {
        if (_isShowing)
            throw new InvalidOperationException(nameof(Show));

        _isShowing = true;

        Instantiate(_swordPrefab, _swordPoint);
        Showed?.Invoke();
    }
}
