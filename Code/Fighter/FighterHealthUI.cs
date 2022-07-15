using System.Collections;
using TMPro;
using UnityEngine;

public class FighterHealthUI : MonoBehaviour
{
    [SerializeField] private FighterHealth _health = null;
    [SerializeField] private RectTransform _canvas = null;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private TMP_Text _text = null;

    [SerializeField] private float _changeSeconds = 0.2f; 

    private int _lastValue;

    private void Awake()
    {
        _canvas.rotation = _camera.rotation;

        _health.Changed += OnChanged;

        _lastValue = _health.Value;
        OnChanged();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnChanged;
    }

    private void OnChanged()
    {
        StartCoroutine(Changing(_health.Value));
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
