using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Transform _cardParent = null;
    [SerializeField] private Hand _hand = null;
    [SerializeField] private Arc _arc = null;
    [SerializeField] private SelectorArea _selectorArea = null;
    [SerializeField] private Card[] _cardsPrefabs = new Card[0];

    private void Start()
    {
        foreach (var cardPrefab in _cardsPrefabs)
        {
            var card = Instantiate(cardPrefab, _cardParent);
            card.Init(_hand, _selectorArea);
            card.GetComponent<CardMovement>()?.Init(_arc);
            _hand.Take(card);
        }
    }
}
