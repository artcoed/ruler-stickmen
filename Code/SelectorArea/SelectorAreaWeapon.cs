using System;
using UnityEngine;

public class SelectorAreaWeapon : MonoBehaviour
{
    [SerializeField] private SelectorArea _selectorArea = null;
    [SerializeField] private SelectorAreaDetector _detector = null;

    private void Awake()
    {
        _selectorArea.Using += OnUsing;
    }

    private void OnDestroy()
    {
        _selectorArea.Using -= OnUsing;
    }

    private void OnUsing(Card card)
    {
        switch (card.Type)
        {
            case CardType.Sword:
                foreach (var select in _detector.Selected)
                    if (select.TryGetComponent<FighterSword>(out var sword) && sword.IsShowing == false)
                        sword.Show();
                break;
            case CardType.Protection:
                foreach (var select in _detector.Selected)
                    if (select.TryGetComponent<FighterProtection>(out var protection) && protection.IsShowing == false)
                        protection.Show();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(OnUsing));
        }

        _detector.Stop();
    }
}
