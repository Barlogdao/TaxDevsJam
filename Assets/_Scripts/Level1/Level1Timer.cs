using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1Timer : MonoBehaviour
{
    public UnityEvent Elapsed;
    public UnityEvent<float> TimeChanged;

    [SerializeField] private float _timeToFinishLevel;

    private bool _timerEnabled;

    private void Start()
    {
        _timerEnabled = false;
    }

    private void Update()
    {
        if (_timerEnabled == false)
            return;

        _timeToFinishLevel -= Time.deltaTime;
        TimeChanged?.Invoke(_timeToFinishLevel);

    }

    public void StartTimer()
    {
        _timerEnabled = true;
    }
}
