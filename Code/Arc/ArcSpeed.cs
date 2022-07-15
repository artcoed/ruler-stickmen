using System;
using UnityEngine;

[Serializable]
public class ArcSpeed
{
    [SerializeField] private float _side = 0f;
    [SerializeField] private float _radius = 0f;

    public float Side => _side;
    public float Radius => _radius;

    public ArcSpeed() : this(0, 0) {}

    public ArcSpeed(float sideSpeed, float radiusSpeed)
    {
        _side = sideSpeed;
        _radius = radiusSpeed;
    }
}
