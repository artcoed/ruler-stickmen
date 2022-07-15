using System;
using UnityEngine;

[Serializable]
public class ArcPosition
{
    [SerializeField] private float _side = 0f;
    [SerializeField] private float _arcRadiusOffset = 0f;

    public float Side => _side;
    public float ArcRadiusOffset => _arcRadiusOffset;

    public ArcPosition() : this(0, 0) { }

    public ArcPosition(float side) : this(side, 0) { }

    public ArcPosition(float side, float arcRadiusOffset)
    {
        _side = side;
        _arcRadiusOffset = arcRadiusOffset;
    }

    public float GetNormalizedByPI()
    {
        return Mathf.PI - _side / 100 * Mathf.PI;
    }

    public bool Equals(ArcPosition b)
    {
        return Side == b.Side && ArcRadiusOffset == b.ArcRadiusOffset;
    }

    public static ArcPosition operator -(ArcPosition a, ArcPosition b)
    {
        return new ArcPosition(a.Side - b.Side, a.ArcRadiusOffset - b.ArcRadiusOffset);
    }

    public static ArcPosition operator +(ArcPosition a, ArcPosition b)
    {
        return new ArcPosition(a.Side + b.Side, a.ArcRadiusOffset + b.ArcRadiusOffset);
    }
}
