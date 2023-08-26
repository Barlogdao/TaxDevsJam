using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 60f;
    public delegate void TimeOutHandler();
    public event TimeOutHandler TimeOutEvent;

    private float _currentTime;

    private void Start()
    {
        _currentTime = countdownDuration;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _currentTime = 0;
                HandleTimeOut();
            }
        }
    }

    private void HandleTimeOut()
    {
        if (TimeOutEvent != null)
        {
            TimeOutEvent();
        }
    }
}