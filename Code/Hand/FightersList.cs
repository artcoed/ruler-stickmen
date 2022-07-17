using System;
using System.Collections.Generic;
using UnityEngine;

public class FightersList : MonoBehaviour
{
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();

    private Dictionary<Fighter, List<Fighter>> _recipients;

    public bool HasNearby => _fighters.Count > 0;

    private void Awake()
    {
        _recipients = new Dictionary<Fighter, List<Fighter>>();

        foreach (var fighter in _fighters)
            _recipients.Add(fighter, new List<Fighter>());
    }

    public void Remove(Fighter fighter)
    {
        if (_fighters.Remove(fighter) == false)
            throw new ArgumentOutOfRangeException(nameof(Remove));

        if (_recipients.TryGetValue(fighter, out var recipients))
            foreach (var recipient in recipients)
                if (recipient.CanUntarget)
                    recipient.Untarget();
    }

    public Fighter GetNearby(Fighter to)
    {
        if (HasNearby == false)
            throw new InvalidOperationException(nameof(GetNearby));

        var nearbyDistance = Mathf.Infinity;
        var nearbyFighter = _fighters[0];

        foreach (var fighter in _fighters)
        {
            var distance = Vector3.Distance(to.Position, fighter.Position);

            if (distance <= nearbyDistance)
            {
                nearbyDistance = distance;
                nearbyFighter = fighter;
            }
        }

        if (_recipients.TryGetValue(nearbyFighter, out var recipients))
            recipients.Add(to);

        return nearbyFighter;
    }
}
