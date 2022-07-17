using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardRenderer : MonoBehaviour
{
    [SerializeField] private Card _card = null;
    [SerializeField] private Outline _outline = null;
    [SerializeField] private Image _image = null;
    [SerializeField] private Image _subimage = null;

    [SerializeField] private Color _startColor = Color.red;

    [SerializeField] private Color _selectOutline = Color.green;
    [SerializeField] private Color _selectImage = Color.cyan;
    [SerializeField] private float _selectSeconds = 0.2f;

    [SerializeField] private float _useAlpha = 0f;
    [SerializeField] private float _useSeconds = 1f;

    private Tween _imageSelecting;
    private Tween _outlineSelecting;

    private void Awake()
    {
        _image.color = _startColor;

        _card.Selecting += OnSelecting;
        _card.Using += OnUsing;
    }

    private void OnDestroy()
    {
        _card.Selecting += OnSelecting;
        _card.Using -= OnUsing;
    }

    private void OnSelecting()
    {
        _outlineSelecting = _outline
            .DOColor(_selectOutline, _selectSeconds);
        _imageSelecting = _image
            .DOColor(_selectImage, _selectSeconds);
    }

    private void OnUsing()
    {
        _outlineSelecting?.Kill();
        _imageSelecting?.Kill();

        StartCoroutine(Using());
    }

    private IEnumerator Using()
    {
        _subimage
            .DOFade(_useAlpha, _useSeconds).WaitForCompletion();
        var outlineUsing = _outline
            .DOFade(_useAlpha, _useSeconds).WaitForCompletion();
        yield return _image
            .DOFade(_useAlpha, _useSeconds).WaitForCompletion();
        yield return outlineUsing;

        _card.Die();
    }
}
