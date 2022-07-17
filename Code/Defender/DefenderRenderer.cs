using UnityEngine;

public class DefenderRenderer : MonoBehaviour
{
    [SerializeField] private Defender _defender = null;
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Material _startMaterial = null;
    [SerializeField] private Material _selectMaterial = null;

    private void Awake()
    {
        _renderer.material = _startMaterial;

        _defender.Selected += OnSelected;
        _defender.Unselected += OnUnselected;
    }

    private void OnDestroy()
    {
        _defender.Selected -= OnSelected;
        _defender.Unselected -= OnUnselected;
    }

    private void OnSelected()
    {
        _renderer.material = _selectMaterial;
    }

    private void OnUnselected()
    {
        _renderer.material = _startMaterial;
    }
}
