using UnityEngine;

public class SelectorAreaMovement : MonoBehaviour
{
    [SerializeField] private SelectorAreaMovementInput _input = null;
    [SerializeField] private Rigidbody _rigidbody = null;

    private Vector3 _desiredPosition;
    private Vector3 _lastPosition;

    private void Awake()
    {
        _input.Moved += OnMoved;
    }

    private void OnDestroy()
    {
        _input.Moved -= OnMoved;
    }

    private void FixedUpdate()
    {
        if (_desiredPosition == _lastPosition)
            return;

        _lastPosition = _desiredPosition;
        _rigidbody.MovePosition(_desiredPosition);
    }

    private void OnMoved()
    {
        _desiredPosition = _input.Position;
    }
}
