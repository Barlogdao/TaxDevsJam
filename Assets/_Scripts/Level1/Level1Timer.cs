using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1Timer : MonoBehaviour
{
    public UnityAction Elapsed;
    public UnityAction<float> TimeChanged;

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

        if(_timeToFinishLevel <= 0)
        {
            Elapsed?.Invoke();
            _timerEnabled = false;
            SoundBus.TimeEnded?.Invoke();
            Debug.Log(1);
            return;
        }

        _timeToFinishLevel -= Time.deltaTime;
        TimeChanged?.Invoke(_timeToFinishLevel);
    }

    public void StartTimer()
    {
        _timerEnabled = true;
    }
}
