using System;
using UnityEngine;

public class SelectorArea : MonoBehaviour
{
    private Card _showingCard;

    public event Action Showing;

    public event Action<Card> Using;

    public bool CanShow => _showingCard == null;

    public void Show(Card card)
    {
        if (CanShow == false)
            throw new InvalidOperationException(nameof(Show));

        _showingCard = card;

        Showing?.Invoke();
    }

    public void Use(Card card)
    {
        if (_showingCard != card)
            throw new ArgumentException(nameof(Use));

        Using?.Invoke(card);
    }
}
