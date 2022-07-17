using UnityEngine;

public class SelectorAreaRenderer : MonoBehaviour
{
    [SerializeField] private SelectorArea _selectorArea = null;
    [SerializeField] private GameObject _skin = null;

    private void Awake()
    {
        _skin.SetActive(false);

        _selectorArea.Showing += OnShowing;
        _selectorArea.Using += OnUsing;
    }

    private void OnDestroy()
    {
        _selectorArea.Showing -= OnShowing;
        _selectorArea.Using -= OnUsing;
    }

    private void OnShowing()
    {
        _skin.SetActive(true);
    }

    private void OnUsing(Card card)
    {
        _skin.SetActive(false);
    }
}
