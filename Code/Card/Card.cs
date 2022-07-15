using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardPointerInput _input = null;

    private Hand _hand;

    private bool _isShowing;
    private bool _isRelaxing;
    private bool _isSelecting;
    private bool _isUsing;

    public event Action<ArcPosition> ShowingIn;

    public event Action Selecting;

    public event Action Using;

    public void Init(Hand hand)
    {
        _hand = hand;

        gameObject.SetActive(false);

        _input.Pressed += OnPressed;
        _input.Released += OnReleased;
    }

    private void OnDestroy()
    {
        _input.Pressed -= OnPressed;
        _input.Released -= OnReleased;
    }

    public void ShowIn(ArcPosition position)
    {
        if (_isShowing)
            throw new InvalidOperationException(nameof(ShowIn));

        _isShowing = true;

        gameObject.SetActive(true);

        ShowingIn?.Invoke(position);
    }

    public void Relax()
    {
        if (_isShowing == false)
            throw new InvalidOperationException(nameof(Relax));

        if (_isRelaxing)
            throw new InvalidOperationException(nameof(Relax));

        _isRelaxing = true;

        _hand.Relax(this);
    }

    public void Select()
    {
        if (_isRelaxing == false)
            throw new InvalidOperationException(nameof(Select));

        if (_isSelecting)
            throw new InvalidOperationException(nameof(Select));

        _isSelecting = true;

        Selecting?.Invoke();
    }

    public void Use()
    {
        if (_isSelecting == false)
            throw new InvalidOperationException(nameof(Use));

        if (_isUsing)
            throw new InvalidOperationException(nameof(Use));

        _isUsing = true;

        Using?.Invoke();
    }

    public void Die()
    {
        _hand.Drop(this);
        Destroy(gameObject);
    }

    private void OnPressed()
    {
        if (_isSelecting == false && _isRelaxing)
            Select();
    }

    private void OnReleased()
    {
        if (_isUsing == false && _isSelecting)
            Use();
    }
}
