using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1Logic : MonoBehaviour
{
    [SerializeField] private int _studentsForFinish;
    [SerializeField] private TMP_Text _questInfo;
    [SerializeField] private RomanSnake _roman;
    [SerializeField] private GameObject _welcomePanel;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;

    private void OnEnable()
    {
        _roman.FollowersChanged += OnFollowersChanged;
        _roman.ReturnedToOffice += OnReturnToOffice;
    }

    private void OnDisable()
    {
        _roman.FollowersChanged -= OnFollowersChanged;
        _roman.ReturnedToOffice -= OnReturnToOffice;
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
        if (followers >= _studentsForFinish)
            Win();
    }

    public void StartGame()
    {
        _welcomePanel.SetActive(false);
        _uiPanel.SetActive(true);
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
}
