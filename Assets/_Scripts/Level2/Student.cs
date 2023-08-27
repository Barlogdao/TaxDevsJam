using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Student : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hpLossRate;
    [SerializeField] private int hpGainRate;
    [SerializeField] private int experienceGainRate;
    
    private StudentState currentState{get; set;}
    private int Hp { get; set; }
    public int Experience { get; private set; }
    
    public UnityAction<int, int> HealthChanged;
    public UnityAction<int> ExperienceChanged;
    
    private TortureSpot currentTortureSpot;
    private RecreationSpot currentRecreationSpot;
    
    void Start()
    {
        Hp = maxHp;
        Experience = 0;
        currentState = StudentState.Normal;
        HealthChanged?.Invoke(Hp, maxHp);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TortureSpot") && currentState != StudentState.Torture && currentState != StudentState.Dragging)
        {
            currentState = StudentState.Torture;
            StartCoroutine(Torture());
        }
        else if (other.CompareTag("RecreationSpot") && currentState != StudentState.Recreation && currentState != StudentState.Dragging)
        {
            currentState = StudentState.Recreation;
            StartCoroutine(Recreation());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("TortureSpot") && currentState == StudentState.Torture) ||
            (other.CompareTag("RecreationSpot") && currentState == StudentState.Recreation))
        {
            currentState = StudentState.Normal;
        }
    }

    private IEnumerator Torture()
    {
        while (currentState == StudentState.Torture && Hp > 0)
        {
            Hp -= hpLossRate;
            Experience += experienceGainRate;
            HealthChanged?.Invoke(Hp, maxHp);
            ExperienceChanged?.Invoke(Experience);
            
            if (Hp <= 0)
            {
                Die();
            }
            yield return new WaitForSeconds(1f);
        }
        currentState = StudentState.Normal;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Recreation()
    {
        while (currentState == StudentState.Recreation)
        {
            if (Hp < maxHp)
            {
                Hp += hpGainRate;
                
            }
            yield return new WaitForSeconds(1f);
        }
        currentState = StudentState.Normal;
    }
    
}
