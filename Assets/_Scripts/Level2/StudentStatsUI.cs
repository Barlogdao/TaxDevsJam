using System;
using System.Collections;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StudentStatsUI : MonoBehaviour
{
    [SerializeField] private Student _student;
    [SerializeField] protected Image _bar;
    [SerializeField] protected TextMeshProUGUI _experienceText;
    private void OnEnable()
    {
        _student.HealthChanged += OnChangeHealth;
        _student.ExperienceChanged += OnChangeExperience;
    }

    private void OnDisable()
    {
        _student.HealthChanged -= OnChangeHealth;
        _student.ExperienceChanged -= OnChangeExperience;

    }

    protected void OnChangeHealth(int health, int maxHealth)
    {
        if(health <= 0)
        {
            _bar.fillAmount = 0;
        }
        else
        {
            
            float value = (float)health / (float)maxHealth;
            _bar.fillAmount = value;
        }
        
    }
    protected void OnChangeExperience(int experience)
    {
        _experienceText.text = experience.ToString();
    }
}