using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Game _game = null;
    [SerializeField] private CardPositions[] _cardPositions = new CardPositions[0];
    [SerializeField] private ArcPosition[] _showPositions = new ArcPosition[0];

    [SerializeField] private int _amountDropCardToStartGame = 5;

    private Queue<Card> _takingCards;
    private List<Card> _showedCards;

    private Card _showingCard;
    private int _amountDropCard;

    private void Awake()
    {
        _takingCards = new Queue<Card>();
        _showedCards = new List<Card>();
    }

    public void Take(Card card)
    {
        if (_showingCard == null)
            Show(card);
        else
            _takingCards.Enqueue(card);
    }

    public void Relax(Card card)
    {
        if (_showingCard != card)
            throw new ArgumentException(nameof(card));

        _showedCards.Add(_showingCard);
        _showingCard = null;

        if (_takingCards.Count > 0)
            Show(_takingCards.Dequeue());
    }

    public void Drop(Card card)
    {
        _showedCards.Remove(card);
        MoveToPlaces();
        _amountDropCard++;

        if (_amountDropCard == _amountDropCardToStartGame)
            _game.Play();
    }

    private void Show(Card card)
    {
        _showingCard = card;
        _showingCard.ShowIn(_showPositions[_showedCards.Count]);
        MoveToPlaces();
    }

    private void MoveToPlaces()
    {
        var amountCards = _showedCards.Count;
        if (_showingCard != null)
        {
            amountCards++;
            Move(_showingCard, _cardPositions[amountCards - 1].Positions[amountCards - 1]);
        }

        for (var i = 0; i < _showedCards.Count; i++)
            Move(_showedCards[i], _cardPositions[amountCards - 1].Positions[i]);
    }

    private void Move(Card card, ArcPosition position)
    {
        card.GetComponent<CardMovement>()?.MoveTo(position);
    }
}
