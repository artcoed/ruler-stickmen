using System.Collections.Generic;
using UnityEngine;

public class SelectorAreaDetector : MonoBehaviour
{
    [SerializeField] private SelectorArea _selectorArea = null;

    private List<Defender> _selected;

    private bool _isShowing;

    public List<Defender> Selected => _selected;

    private void Awake()
    {
        _selected = new List<Defender>();

        _selectorArea.Showing += OnShowing;
    }

    private void OnDestroy()
    {
        _selectorArea.Showing -= OnShowing;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_isShowing == false)
            return;

        if (collider.TryGetComponent<Defender>(out var defender))
        {
            defender.Select();
            _selected.Add(defender);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_isShowing == false)
            return;

        if (collider.TryGetComponent<Defender>(out var defender) && defender.IsSelected)
        {
            defender.Unselect();
            _selected.Remove(defender);
        }
    }

    public void Stop()
    {
        _isShowing = false;

        foreach (var defender in _selected)
            if (defender.IsSelected)
                defender.Unselect();

        _selected.Clear();
    }

    private void OnShowing()
    {
        _isShowing = true;
    }
}
