using System;
using System.Collections;
using UnityEngine;

public class FighterProtection : MonoBehaviour
{
    [SerializeField] private GameObject _shieldPrefab = null;
    [SerializeField] private Transform _shieldPoint = null;

    [SerializeField] private int _value = 20;

    private bool _isShowing;

    public event Action Showed;

    public int Value => _value;

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

        Instantiate(_shieldPrefab, _shieldPoint);
        Showed?.Invoke();
    }
}
