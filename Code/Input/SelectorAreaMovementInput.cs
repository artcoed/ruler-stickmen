using System;
using UnityEngine;

public class SelectorAreaMovementInput : MonoBehaviour, ISelectorAreaMovementInput
{
    [SerializeField] private Camera _camera = null;

    [SerializeField] private float _distance = 50f;
    [SerializeField] private LayerMask _floorMask = new LayerMask();

    private Vector3 _position;

    public event Action Moved;

    public Vector3 Position => _position;

    public void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, _distance, _floorMask))
        {
            _position = hit.point;
            Moved?.Invoke();
        }
    }
}
