using System.Collections;
using UnityEngine;

public class GameText : MonoBehaviour
{
    [SerializeField] private RectTransform _text = null;

    [SerializeField] private Vector3 _startScale = Vector3.zero;
    [SerializeField] private float _startSeconds = 1f;

    [SerializeField] private Vector3 _middleScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float _middleSeconds = 1f;

    [SerializeField] private Vector3 _endScale = Vector3.one;
    [SerializeField] private float _endSeconds = 1f;
    [SerializeField] private float _endWaitSeconds = 1f;

    public void Show()
    {
        _text.gameObject.SetActive(true);
        StartCoroutine(Showing());
    }

    private IEnumerator Showing()
    {
        yield return StartCoroutine(Scaling(_middleScale, _middleSeconds));
        yield return StartCoroutine(Scaling(_endScale, _endSeconds));
        yield return new WaitForSeconds(_endWaitSeconds);
        yield return StartCoroutine(Scaling(_startScale, _startSeconds));
        _text.gameObject.SetActive(false);
    }

    private IEnumerator Scaling(Vector3 to, float seconds)
    {
        var startSacle = _text.localScale;
        var startOffset = to - startSacle;

        var elapsedSeconds = 0f;
        var progress = 0f;

        while (progress < 1)
        {
            elapsedSeconds += Time.deltaTime;
            progress = elapsedSeconds / seconds;
            _text.localScale = startSacle + progress * startOffset;
            yield return null;
        }
    }
}
