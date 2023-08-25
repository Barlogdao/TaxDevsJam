using System.Collections;
using UnityEngine;

namespace RB.Services.Audio
{

    public class OneChannelMusicPlayer : MusicPlayer
    {
        private AudioSource _musicChannel;
        public OneChannelMusicPlayer(AudioService audioService,AudioSource musicChannel) : base (audioService)
        {
            if(musicChannel == null)
            {
                throw new System.Exception("AudioService doesn't have assigned audiosource for First MusicChannel");
            }
            _musicChannel = musicChannel;
        }

        public override void PlayMusic(AudioClip clip)
        {
            if (_musicChannel.clip == clip) return;
            if(CurrentAudioService.FadeMusicClips == true)
            {
                CurrentAudioService.StartCoroutine(FadeMusic(clip));
            }
            else
            {
                SwitchAudioClip(clip);
            }
            
        }

        private IEnumerator FadeMusic(AudioClip clip)
        {
            float currentTime = 0;
            float start = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
            float duration = CurrentAudioService.MusicFadeTime;
            while (currentTime < duration / 2)
            {
                currentTime += Time.unscaledDeltaTime;
                _musicChannel.volume = Mathf.Lerp(start, 0f, currentTime / (duration / 2));
                yield return null;
            }
            SwitchAudioClip(clip);
            currentTime = 0;
            while (currentTime < duration / 2)
            {
                currentTime += Time.unscaledDeltaTime;
                _musicChannel.volume = Mathf.Lerp(0f, start, currentTime / (duration / 2));
                yield return null;
            }
        }


        private void SwitchAudioClip(AudioClip clip)
        {
            var time = _musicChannel.timeSamples;
            
            _musicChannel.Stop();
            _musicChannel.clip = clip;
            _musicChannel.Play();
            if (CurrentAudioService.SyncMusicClips == true)
            {
                _musicChannel.timeSamples = time;
            }
        }


        public override void UpdateVolumeLevel()
        {
            _musicChannel.volume = PlayerPrefs.GetFloat(VolumeLevel, 1f) * PlayerPrefs.GetFloat(MasterVolumeLevel, 1f) * MuteModifier();
        }

        public override void PauseMusic()
        {
            _musicChannel.Pause();
        }

        public override void UnpauseMusic()
        {
            _musicChannel.UnPause();
        }
    }
}
