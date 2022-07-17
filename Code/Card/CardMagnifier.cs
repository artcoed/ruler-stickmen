using DG.Tweening;
using UnityEngine;

public class CardMagnifier : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform = null;
    [SerializeField] private Card _card = null;

    [SerializeField] private Vector2 _startScale = Vector2.one;
    [SerializeField] private float _startScaleSeconds = 0.2f;

    [SerializeField] private Vector2 _selectScale = new Vector2(0.8f, 0.8f);
    [SerializeField] private float _selectScaleSeconds = 0.2f;

    private Sequence _selecting;

    private void Awake()
    {
        _rectTransform.localScale = _startScale;

        _card.Selecting += OnSelecting;
        _card.Using += OnUsing;
    }

    private void OnDestroy()
    {
        _card.Selecting -= OnUsing;
    }

    private void OnSelecting()
    {
        _selecting = DOTween.Sequence()
            .Append(_rectTransform
                .DOScale(_selectScale, _selectScaleSeconds))
            .Append(_rectTransform
                .DOScale(_startScale, _startScaleSeconds));
    }

    private void OnUsing()
    {
        _selecting?.Kill();
    }
}
