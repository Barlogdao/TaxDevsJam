using UnityEngine;

namespace RB.Services.Audio
{
    [CreateAssetMenu(fileName = "SingleClipContainer", menuName = "Services/Audio/SingleClip")]
    public class SingleClipContainer : ClipContainer
    {
        [SerializeField] private AudioClip _clip;
        public override AudioClip GetClip()
        {
            return _clip;
        }
    }
}
