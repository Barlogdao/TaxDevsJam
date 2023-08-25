using System;
using UnityEngine;

namespace RB.Services.Audio
{
    public class AudioService : MonoBehaviour
    {
        public static AudioService Instance { get; private set; }

        public const string MUTE_AUDIO = "MUTE_AUDIO";

        private MusicPlayer _musicPlayer;
        private SoundPlayer _soundPlayer;

        [field: SerializeField] private AudioSource SoundChannel;
        [field: SerializeField] private AudioSource FirstMusicChannel;
        [field: SerializeField] private AudioSource SecondMusicChannel;
        [Header("Music Player Options")]
        [Tooltip("OneChannel for simple music without crossfade. TwoChannel for crossfade tracks")]
        [SerializeField] private MusicPlayerType _musicPlayerType;
        [field: SerializeField] public bool SyncMusicClips { get; private set; }
        [field: SerializeField] public bool FadeMusicClips { get; private set; }
        [field: SerializeField] public float MusicFadeTime { get; private set; }
        public bool IsMuted { get; private set; }


        public static event Action<bool> MuteStateChanged;

        protected void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMuteValue();
            switch (_musicPlayerType)
            {
                case MusicPlayerType.OneChannelPlayer:
                    _musicPlayer = new OneChannelMusicPlayer(this, FirstMusicChannel);
                    break;
                case MusicPlayerType.TwoChannelPlayer:
                    _musicPlayer = new TwoChannelMusicPlayer(this, FirstMusicChannel, SecondMusicChannel);
                    break;
            }
            _soundPlayer = new SoundPlayer(this, SoundChannel);

            UpdateAudioPlayers();
        }

        public void PlaySound(AudioClip clip)
        {
            _soundPlayer.PlaySound(clip);
        }
        public void PlayMusic(AudioClip clip)
        {
            _musicPlayer.PlayMusic(clip);
        }

        public void PauseMusic()
        {
            _musicPlayer.PauseMusic();
        }
        public void UnpauseMusic()
        {
            _musicPlayer.UnpauseMusic();
        }
        public bool ToggleMuteAudio()
        {
            IsMuted = !IsMuted;
            SaveMuteValue();
            Debug.Log(IsMuted);
            UpdateAudioPlayers();
            MuteStateChanged?.Invoke(IsMuted);
            return IsMuted;
        }

        public void ChangeVolume(float volume, AudioChannelType channelType)
        {
            PlayerPrefs.SetFloat(channelType.ToString(), volume);
            UpdateAudioPlayers();
        }


        private void LoadMuteValue()
        {
            int value = PlayerPrefs.GetInt(MUTE_AUDIO, 0);
            IsMuted = value != 0;
        }
        private void SaveMuteValue()
        {
            PlayerPrefs.SetInt(MUTE_AUDIO, IsMuted ? 1 : 0);
        }

        private void UpdateAudioPlayers()
        {
            _musicPlayer.UpdateVolumeLevel();
            _soundPlayer.UpdateVolumeLevel();
        }

    }

    public enum AudioChannelType
    {
        MASTER_CHANNEL,
        MUSIC_CHANNEL,
        SOUND_CHANNEL,
    }
    public enum MusicPlayerType
    {
        OneChannelPlayer,
        TwoChannelPlayer,
    }

}