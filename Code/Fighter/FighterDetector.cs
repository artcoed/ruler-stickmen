using UnityEngine;

public class FighterDetector : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private FightersList _detectList = null;

    private void Awake()
    {
        _fighter.Playing += Search;
        _fighter.Untargeted += Search;
    }

    private void OnDestroy()
    {
        _fighter.Playing -= Search;
        _fighter.Untargeted -= Search;
    }

    private void Search()
    {
        if (_detectList.HasNearby && _fighter.CanTarget)
            _fighter.Target(_detectList.GetNearby(_fighter));
        else
            _fighter.Win();
    }
}
