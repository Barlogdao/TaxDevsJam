using System.Collections;
using UnityEngine;

namespace RB.Services.Audio
{
    public class TwoChannelMusicPlayer : MusicPlayer
    {
        private readonly AudioSource _firstMusicChannel;
        private readonly AudioSource _secondMusicChannel;
        private bool _isFirstChannelPlaying = false;
        private AudioSource ActiveChannel => _isFirstChannelPlaying ? _firstMusicChannel : _secondMusicChannel;
        private AudioSource InactiveChannel => _isFirstChannelPlaying ? _secondMusicChannel : _firstMusicChannel;

        public TwoChannelMusicPlayer(AudioService audioService, AudioSource firstMusicChannel, AudioSource secondMusicChannel) : base(audioService)
        {
            if (firstMusicChannel == null)
            {
                throw new System.Exception("AudioService doesn't have assigned audiosource for First MusicChannel");
            }
            if (secondMusicChannel == null)
            {
                throw new System.Exception("AudioService doesn't have assigned audiosource for Second MusicChannel");
            }
            _firstMusicChannel = firstMusicChannel;
            _secondMusicChannel = secondMusicChannel;
        }

        public override void PlayMusic(AudioClip clip)
        {
            if (ActiveChannel.clip == clip) return;
            if(CurrentAudioService.FadeMusicClips == false)
            {
                SwitchMusicClips(clip);
                return;
            }
            if (CurrentAudioService.SyncMusicClips == false)
            {
                CurrentAudioService.StartCoroutine(FadeMusic(clip));
            }
            else
            {
                CurrentAudioService.StartCoroutine(FadeSyncMusic(clip));
            }
        }

        public override void PauseMusic()
        {
            ActiveChannel.Pause();
        }
        public override void UnpauseMusic()
        {
            ActiveChannel.UnPause();
        }

        private void SwitchMusicClips(AudioClip clip)
        {
            var time = ActiveChannel.timeSamples;

            ActiveChannel.Stop();
            InactiveChannel.Stop();
            _isFirstChannelPlaying = !_isFirstChannelPlaying;
            ActiveChannel.clip = clip;
            ActiveChannel.Play();
            if (CurrentAudioService.SyncMusicClips == true)
            {
                ActiveChannel.timeSamples = time;
            }
            InactiveChannel.Stop();
            UpdateVolumeLevel();
        }

        private IEnumerator FadeMusic(AudioClip clip)
        {

            float currentvolume = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
            float currentTime = 0;
            float duration = CurrentAudioService.MusicFadeTime;

            InactiveChannel.clip = clip;
            InactiveChannel.Play();

            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                ActiveChannel.volume = Mathf.Lerp(currentvolume, 0f, currentTime / duration);
                InactiveChannel.volume = Mathf.Lerp(0f, currentvolume, currentTime / duration);
                yield return null;
            }
            _isFirstChannelPlaying = !_isFirstChannelPlaying;
            InactiveChannel.Stop();
        }
        private IEnumerator FadeSyncMusic(AudioClip clip)
        {

            float currentvolume = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
            float currentTime = 0;
            float duration = CurrentAudioService.MusicFadeTime;
            var time = ActiveChannel.timeSamples;
            InactiveChannel.clip = clip;
            InactiveChannel.Play();
            InactiveChannel.timeSamples = time;
            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                ActiveChannel.volume = Mathf.Lerp(currentvolume, 0f, currentTime / duration);
                InactiveChannel.volume = Mathf.Lerp(0f, currentvolume, currentTime / duration);
                yield return null;
            }
            _isFirstChannelPlaying = !_isFirstChannelPlaying;
            InactiveChannel.Stop();
        }

        public override void UpdateVolumeLevel()
        {
            ActiveChannel.volume = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
            InactiveChannel.volume = 0f;
        }

    }
}
