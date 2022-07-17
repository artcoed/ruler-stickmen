using System;
using UnityEngine;

public interface ISelectorAreaMovementInput
{
    event Action Moved;

    Vector3 Position { get; }
}
