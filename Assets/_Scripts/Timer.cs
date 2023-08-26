using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 60f;
    public delegate void TimeOutHandler();
    public event TimeOutHandler TimeOutEvent;

    private float currentTime;

    private void Start()
    {
        currentTime = countdownDuration;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
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