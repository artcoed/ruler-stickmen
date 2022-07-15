using System;
using UnityEngine;

[Serializable]
public class CardPositions
{
    [SerializeField] private ArcPosition[] _positions = new ArcPosition[0];

    public ArcPosition[] Positions => _positions;
}
