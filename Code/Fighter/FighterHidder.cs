using System.Collections;
using UnityEngine;

public class FighterHidder : MonoBehaviour
{
    [SerializeField] private Fighter _fighter = null;
    [SerializeField] private GameObject _fighterSkin = null;
    [SerializeField] private ParticleSystem _hideParticle = null;
    [SerializeField] private Transform _hideParticlePoint = null;

    [SerializeField] private float _hideSeconds = 2f;

    private void Awake()
    {
        _fighter.Hidding += OnHidding;
    }

    private void OnDestroy()
    {
        _fighter.Hidding -= OnHidding;
    }

    private void OnHidding()
    {
        StartCoroutine(Hidding());
    }

    private IEnumerator Hidding()
    {
        Destroy(_fighterSkin);
        Instantiate(_hideParticle, _hideParticlePoint.position, Quaternion.identity);
        yield return new WaitForSeconds(_hideSeconds);
        _fighter.Die();
    }
}
