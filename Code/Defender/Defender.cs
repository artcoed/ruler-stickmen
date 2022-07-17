using System;
using UnityEngine;

public class Defender : MonoBehaviour
{
    private bool _isSelected;

    public event Action Selected;

    public event Action Unselected;

    public bool IsSelected => _isSelected;

    public void Select()
    {
        if (_isSelected)
            throw new InvalidOperationException(nameof(Select));

        _isSelected = true;

        Selected?.Invoke();
    }

    public void Unselect()
    {
        if (_isSelected == false)
            throw new InvalidOperationException(nameof(Unselect));

        _isSelected = false;

        Unselected?.Invoke();
    }
}
