using UnityEngine;

public class FighterUpgraderRenderer : MonoBehaviour
{
    [SerializeField] private FighterUpgrader _upgrader = null;
    [SerializeField] private ParticleSystem _particle = null;
    [SerializeField] private Transform _particlePoint = null;

    [SerializeField] private float _particleSeconds = 1f;

    private void Awake()
    {
        _upgrader.Upgraded += OnUpgraded;
    }

    private void OnDestroy()
    {
        _upgrader.Upgraded -= OnUpgraded;
    }

    private void OnUpgraded()
    {
        var particle = Instantiate(_particle, _particlePoint.position, _particle.transform.rotation);
        Destroy(particle, _particleSeconds);
    }
}
