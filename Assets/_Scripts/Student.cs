using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hpLossRate;
    [SerializeField] private int hpGainRate;
    [SerializeField] private int experienceGainRate;
    
    private StudentState currentState = StudentState.Normal;
    private int Hp { get; set; }
    private int Experience { get; set; }
    private bool isTorturing = false;
    private bool isRecreating  = false;
    private TortureSpot currentTortureSpot;
    private RecreationSpot currentRecreationSpot;
    
    void Start()
    {
        Hp = maxHp;
        Experience = 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TortureSpot") && currentState != StudentState.Torture)
        {
            currentState = StudentState.Torture;
            StartCoroutine(Torture());
        }
        else if (other.CompareTag("RecreationSpot") && currentState != StudentState.Recreation)
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
