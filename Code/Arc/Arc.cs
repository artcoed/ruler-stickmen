using UnityEngine;

public class Arc : MonoBehaviour
{
    [SerializeField] private Vector2 _center;
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _rotationHardness;

    public Vector2 ConvertPosition(ArcPosition position)
    {
        var oneRadiusResult = new Vector2(Mathf.Cos(position.GetNormalizedByPI()), Mathf.Sin(position.GetNormalizedByPI()));
        return _center + oneRadiusResult * (_radius + position.ArcRadiusOffset);
    }

    public Quaternion GetRotationFrom(ArcPosition position)
    {
        return GetRotationFrom(ConvertPosition(position));
    }

    public Quaternion GetRotationFrom(Vector2 position)
    {
        Vector2 direction = position - _rotationHardness;
        return Quaternion.Euler(0, 0, Vector2.Angle(direction, Vector2.right) - 90);
    }
}
