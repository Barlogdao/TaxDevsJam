using UnityEngine;
namespace RB.Services.Audio
{
    [CreateAssetMenu(fileName = "MultipleClipContainer", menuName = "Services/Audio/MultipleClip")]
    public class MultipleClipContainer : ClipContainer
    {
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private bool _randomPlay = true;
        private int _currentClipIndex = 0;
        public override AudioClip GetClip()
        {
            if (_randomPlay)
            {
                return _clips[Random.Range(0, _clips.Length)];
            }
            else
            {
                _currentClipIndex = (_currentClipIndex + 1)%_clips.Length;
                return _clips[_currentClipIndex];
            }
        }
    }
}