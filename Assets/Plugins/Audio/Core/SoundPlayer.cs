using UnityEngine;

namespace RB.Services.Audio
{
    public class SoundPlayer
    {
        protected readonly AudioService CurrentAudioService;
        protected readonly string VolumeLevel;
        protected readonly string MasterVolumeLevel;
        protected readonly AudioSource SoundChannel;


        public SoundPlayer(AudioService audioService, AudioSource soundChannel)
        {
            CurrentAudioService = audioService;
            VolumeLevel = AudioChannelType.SOUND_CHANNEL.ToString();
            MasterVolumeLevel = AudioChannelType.MASTER_CHANNEL.ToString();
            if (soundChannel == null)
            {
                throw new System.Exception("AudioService doesn't have assigned audiosource for SoundChannel");
            }
            SoundChannel = soundChannel;
        }

        public void UpdateVolumeLevel()
        {
            SoundChannel.volume = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
        }

        public void PlaySound(AudioClip clip) => SoundChannel.PlayOneShot(clip);

        private int MuteModifier()
        {
            return CurrentAudioService.IsMuted ? 0 : 1;
        }
    } 
}
