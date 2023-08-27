using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 60f;
    [SerializeField] private TextMeshProUGUI _timerText;
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
            int seconds = Mathf.RoundToInt(_currentTime);
            _timerText.text = seconds.ToString();
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                if (TimeOutEvent != null) TimeOutEvent.Invoke();
            }
        }
    }

    private void HandleTimeOut()
    {
            
    }
}