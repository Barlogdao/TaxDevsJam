using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        Color startColor = _text.color;
        Color transperentColor = startColor;
        transperentColor.a = 1f;

        _text.DOColor(transperentColor, 1f).SetLoops(100, LoopType.Yoyo);
    }
}
