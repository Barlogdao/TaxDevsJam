using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private EndGameStatistics _endGameStatistics;
    void Start()
    {
        transform.DORotate(new Vector3(0,360,0),2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
        
    }
    
    private void OnMouseDown()
    {
        _endGameStatistics.Collectibles += 1;
        
        Destroy(gameObject);
        
    }
}
