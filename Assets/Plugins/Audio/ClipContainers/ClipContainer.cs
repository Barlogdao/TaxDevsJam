using UnityEngine;

namespace RB.Services.Audio
{

    public abstract class ClipContainer : ScriptableObject
    {
        public abstract AudioClip  GetClip();
    } 
}
