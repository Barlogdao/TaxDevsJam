using System.Collections;
using UnityEngine;


namespace RB.Services.Audio
{
    public abstract class MusicPlayer
    {
        protected AudioService CurrentAudioService;
        protected readonly string VolumeLevel;
        protected readonly string MasterVolumeLevel;
        public MusicPlayer(AudioService audioService)
        {
            CurrentAudioService = audioService;
            VolumeLevel = AudioChannelType.MUSIC_CHANNEL.ToString();
            MasterVolumeLevel = AudioChannelType.MASTER_CHANNEL.ToString();
        }
        public abstract void PlayMusic(AudioClip clip);


        public abstract void UpdateVolumeLevel();
        public abstract void PauseMusic();
        public abstract void UnpauseMusic();


        protected int MuteModifier()
        {
            return CurrentAudioService.IsMuted ? 0 : 1;
        }
    }
}
