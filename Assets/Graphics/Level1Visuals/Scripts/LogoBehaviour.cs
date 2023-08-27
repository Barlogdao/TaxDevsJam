using UnityEngine;
using DG.Tweening;

namespace DTJ.Visual
{
    public class LogoBehaviour : MonoBehaviour
    {
        private void Start()
        {
            transform.DOScale(1.3f,1f).SetEase(Ease.OutSine).SetLoops(-1,LoopType.Yoyo).SetUpdate(true);
        }
    }
}