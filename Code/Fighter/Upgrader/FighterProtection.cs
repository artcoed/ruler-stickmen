using System;
using UnityEngine;

public class FighterProtection : MonoBehaviour
{
    // В этом месте ваще любой предмет можно сделать щитом :3 Для безопасности лучше вывести класс Shield 
    // Или вообще создать иерархию через интерфейс IItem или абстрактный класс Item а потом унаследовать некоторые апгрейды
    // В конечном итоге тебе это даст возможность к дальнейшм расширениям по предметам и абилкам
    [SerializeField] private GameObject _shieldPrefab = null;
    [SerializeField] private Transform _shieldPoint = null;

    [SerializeField] private int _value = 20;

    private bool _isShowing;

    public event Action Showed;

    public int Value => _value;

    public bool IsShowing => _isShowing;

    public void Show()
    {
        if (_isShowing)
            throw new InvalidOperationException(nameof(Show));

        _isShowing = true;

        Instantiate(_shieldPrefab, _shieldPoint);
        Showed?.Invoke();
    }
}
