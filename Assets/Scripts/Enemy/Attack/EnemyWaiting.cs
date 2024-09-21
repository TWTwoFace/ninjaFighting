using System;
using System.Collections;
using UnityEngine;

public class EnemyWaiting : MonoBehaviour
{
    public event Action Ended;

    [SerializeField] private float _minDuration;
    [SerializeField] private float _maxDuration;

    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
    }

    public void WaitRandomTime()
    {
        StartCoroutine(WaitingRoutine());
    }

    private IEnumerator WaitingRoutine()
    {
        yield return new WaitForSeconds(_minDuration + (float)_random.NextDouble() * (_maxDuration - _minDuration));

        Ended?.Invoke();
    }
}
