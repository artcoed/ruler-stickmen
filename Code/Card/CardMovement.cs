using System;
using System.Collections;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    [SerializeField] private Card _card = null;
    [SerializeField] private RectTransform _rectTransform = null;

    [SerializeField] private ArcPosition _startPosition = new ArcPosition();

    [SerializeField] private ArcSpeed _showSpeed = new ArcSpeed(2000, 2000);

    [SerializeField] private ArcSpeed _relaxSpeed = new ArcSpeed(20, 1000);

    [SerializeField] private float _selectHeight = 400f;
    [SerializeField] private float _selectSpeed = 10f;

    [SerializeField] private Quaternion _clearRotation = Quaternion.identity;
    [SerializeField] private float _clearRotationSpeed = 0.2f;

    private Arc _arc;
    private ArcPosition _position;
    private ArcPosition _relaxPosition;

    private bool _isShowing;

    private Coroutine _relaxing;
    private Coroutine _raising;

    public void Init(Arc arc)
    {
        _arc = arc;
        _relaxPosition = new ArcPosition(50);

        SetArcPosition(_startPosition);

        _card.ShowingIn += OnShowingIn;
        _card.Selecting += OnSelecting;
    }

    private void OnDestroy()
    {
        _card.ShowingIn -= OnShowingIn;
        _card.Selecting -= OnSelecting;
    }

    public void MoveTo(ArcPosition position)
    {
        _relaxPosition = position;

        if (_isShowing)
            return;

        Relax();
    }

    private void OnShowingIn(ArcPosition position)
    {
        StartCoroutine(Showing(position));
    }

    private void OnSelecting()
    {
        if (_relaxing != null)
            StopCoroutine(_relaxing);

        _raising = StartCoroutine(Raising(_selectHeight, _selectSpeed));
    }

    private void Relax()
    {
        if (_raising != null)
            return;

        if (_relaxing != null)
            StopCoroutine(_relaxing);

        _relaxing = StartCoroutine(ArcMoving(_relaxPosition, _relaxSpeed));
    }

    private IEnumerator Showing(ArcPosition position)
    {
        _isShowing = true;
        yield return StartCoroutine(ArcMoving(position, _showSpeed));
        _isShowing = false;

        _card.Relax();
        Relax();
    }

    private IEnumerator ArcMoving(ArcPosition position, ArcSpeed speed)
    {
        var startPosition = _position;
        var desiredOffset = position - _position;

        var sideSeconds = Mathf.Abs(desiredOffset.Side) / speed.Side;
        var radiusSeconds = Mathf.Abs(desiredOffset.ArcRadiusOffset) / speed.Radius;

        var elapsedSeconds = 0f;
        var progress = 0f;

        while (progress < 1)
        {
            elapsedSeconds += Time.deltaTime;
            var sideProgress = Mathf.Min(elapsedSeconds / sideSeconds, 1);
            var radiusProgress = Mathf.Min(elapsedSeconds / radiusSeconds, 1);
            progress = Mathf.Min(sideProgress, radiusProgress);
            var currentOffset = new ArcPosition(desiredOffset.Side * sideProgress, desiredOffset.ArcRadiusOffset * radiusProgress);
            SetArcPosition(startPosition + currentOffset);
            yield return null;
        }
    }

    private IEnumerator Raising(float height, float speed)
    {
        var clearingRotation = StartCoroutine(ClearingRotation());

        var startPosition = _rectTransform.anchoredPosition;
        var desiredPosition = new Vector2(_rectTransform.anchoredPosition.x, height);
        var desiredOffset = desiredPosition - startPosition;
        var seconds = height / speed;

        var elapsedSeconds = 0f;
        var progress = 0f;

        while (progress < 1)
        {
            elapsedSeconds += Time.deltaTime;
            progress = Mathf.Min(elapsedSeconds / seconds, 1);
            var currentOffset = progress * desiredOffset;
            _rectTransform.anchoredPosition = startPosition + currentOffset;
            yield return null;
        }

        yield return clearingRotation;
    }

    private IEnumerator ClearingRotation()
    {
        var targetRotation = Quaternion.identity;

        while (_rectTransform.localRotation != _clearRotation)
        {
            _rectTransform.localRotation = Quaternion.RotateTowards(_rectTransform.localRotation, targetRotation, _clearRotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void SetArcPosition(ArcPosition position)
    {
        _position = position;
        _rectTransform.anchoredPosition = _arc.ConvertPosition(position);
        _rectTransform.localRotation = _arc.GetRotationFrom(position);
    }
}
