using UnityEngine;
using RB.Services.Audio;

public class AudioProvider : MonoBehaviour
{
    
    private AudioService _ad;


    [SerializeField] private ClipContainer _explode;
    [SerializeField] private ClipContainer _studentTakern;
    [SerializeField] private ClipContainer _timeEnded;


    [SerializeField] private ClipContainer _menuMusic;
    [SerializeField] private ClipContainer _levelONEMusic;
    [SerializeField] private ClipContainer _levelTWOMusic;
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        _ad = AudioService.Instance;
    }
    private void OnEnable()
    {
        SoundBus.WeipExploded += OnWeipExploded;
        SoundBus.StudentTaken += OnStudentTaken;
        SoundBus.TimeEnded += OnTimeEnded;
        SoundBus.MenuStarted += OnMenuStarted;
        SoundBus.Level1Started += OnLevelOneStarted;
        SoundBus.Level2Started += OnLevelTWOStarted;

    }
    private void OnDisable()
    {
        SoundBus.WeipExploded -= OnWeipExploded;
        SoundBus.StudentTaken -= OnStudentTaken;
        SoundBus.StudentTaken -= OnTimeEnded;
        SoundBus.MenuStarted -= OnMenuStarted;
        SoundBus.Level1Started -= OnLevelOneStarted;
        SoundBus.Level2Started -= OnLevelTWOStarted;

    }

    private void OnLevelTWOStarted()
    {
        _ad.PlayMusic(_levelTWOMusic.GetClip());
    }

    private void OnLevelOneStarted()
    {
        _ad.PlayMusic(_levelONEMusic.GetClip());
    }

    private void OnMenuStarted()
    {
        _ad.PlayMusic(_menuMusic.GetClip());
    }

    private void OnTimeEnded()
    {
        _ad.PlaySound(_timeEnded.GetClip());
    }

    private void OnStudentTaken()
    {
        _ad.PlaySound(_studentTakern.GetClip());
    }

    private void OnWeipExploded()
    {
        _ad.PlaySound(_explode.GetClip());
    }
}
