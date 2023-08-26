using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Logic : MonoBehaviour
{
    [SerializeField] private int _studentsForFinish;
    [SerializeField] private TMP_Text _questInfo;
    [SerializeField] private RomanSnake _roman;
    [SerializeField] private Level1Timer _timer;
    [SerializeField] private TMP_Text _timerLabel;

    [SerializeField] private GameObject _welcomePanel;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;

    private bool _isTimerElapced = false;

    private void OnEnable()
    {
        _roman.FollowersChanged += OnFollowersChanged;
        _roman.ReturnedToOffice += OnReturnToOffice;
        _timer.Elapsed += OnTimerElapsed;
        _timer.TimeChanged += OnTimerChanged;
    }

    private void OnDisable()
    {
        _roman.FollowersChanged -= OnFollowersChanged;
        _roman.ReturnedToOffice -= OnReturnToOffice;
        _timer.Elapsed -= OnTimerElapsed;
        _timer.TimeChanged -= OnTimerChanged;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnFollowersChanged(int followers)
    {
        int studentsToFind = _studentsForFinish - followers;

        if (studentsToFind > 0)
            _questInfo.text = string.Format($"Осталось найти учеников: {studentsToFind}");
        else
            _questInfo.text = string.Format($"Веди учеников в офис");
    }

    private void OnReturnToOffice(int followers)
    {
        //if (followers >= _studentsForFinish)
        //    Win();
        if(_isTimerElapced)
            Win();
    }

    private void OnTimerElapsed()
    {
        // Fail();
        _isTimerElapced = true;
    }

    private void OnTimerChanged(float time)
    {
        _timerLabel.text = string.Format($"{(int)time}");
    }

    public void StartGame()
    {
        _welcomePanel.SetActive(false);
        _uiPanel.SetActive(true);
        _timer.StartTimer();
        Time.timeScale = 1.0f;
    }

    private void Win()
    {
        Time.timeScale = 0;
        _uiPanel.SetActive(false);
        _winPanel.SetActive(true);
    }

    private void Fail()
    {
        Time.timeScale = 0;
        _uiPanel.SetActive(false);
        _failPanel.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("students", _roman.GetFollowersCount());
        SceneManager.LoadScene(2);
    }
}
