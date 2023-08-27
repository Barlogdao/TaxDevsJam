using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Timer _gameTimer;
    [SerializeField] private EndGameStatistics _endGameStatistics;
    
    private void OnEnable()
    {
        _gameTimer.TimeOutEvent += ShowTotalScore;
    }

    private void ShowTotalScore()
    {
        scoreContainer.SetActive(true);
        _scoreText.text ="СЧЕТ: " + _endGameStatistics.CalculateEndScore().ToString();
        
    }
}
