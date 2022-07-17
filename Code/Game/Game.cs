using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameText _text = null;
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();

    [SerializeField] private float _pauseSeconds = 2f;

    private bool _isPlaying;

    public void Play()
    {
        if (_isPlaying)
            throw new InvalidOperationException(nameof(Play));

        _isPlaying = true;

        StartCoroutine(Playing());
    }

    private IEnumerator Playing()
    {
        _text.Show();
        yield return new WaitForSeconds(_pauseSeconds);

        foreach (var fighter in _fighters)
            fighter.Play();
    }
}
