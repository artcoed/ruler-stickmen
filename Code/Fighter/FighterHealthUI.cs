using System.Collections;
using TMPro;
using UnityEngine;

public class FighterHealthUI : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FighterHealth _health = null;
    [SerializeField] private RectTransform _canvas = null;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private TMP_Text _text = null;

    [SerializeField] private float _changeSeconds = 0.2f; 

    private int _lastValue;
    private bool _isHidding;

    private void Awake()
    {
        _lastValue = _health.Value;
        RoatateCanvas();
        OnChanged();

        _health.Changed += OnChanged;
        _fighter.Hidding += OnHidding;
    }

    private void OnDestroy()
    {
        _health.Changed -= OnChanged;
        _fighter.Hidding -= OnHidding;
    }

    private void LateUpdate()
    {
        if (_isHidding)
            return;

        RoatateCanvas();
    }

    private void OnChanged()
    {
        StartCoroutine(Changing(_health.Value));
    }

    private void OnHidding()
    {
        _isHidding = true;
    }

    private void RoatateCanvas()
    {
        _canvas.rotation = _camera.rotation;
    }

    private IEnumerator Changing(int value)
    {
        var startValue = _lastValue;
        var offset = _health.Value - _lastValue;

        var elapsedSeconds = 0f;
        var progress = 0f;

        while (progress < 1)
        {
            elapsedSeconds += Time.deltaTime;
            progress = elapsedSeconds / _changeSeconds;
            _lastValue = (int)(progress * offset) + startValue;
            _text.text = _lastValue.ToString();
            yield return null;
        }
    }
}
