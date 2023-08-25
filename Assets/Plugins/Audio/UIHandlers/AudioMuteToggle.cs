using System;
using UnityEngine;
using UnityEngine.UI;

namespace RB.Services.Audio
{
    [RequireComponent(typeof(Toggle))]
	public class AudioMuteToggle : MonoBehaviour
	{
		private Toggle _toggle;
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {

            _toggle.isOn = PlayerPrefs.GetInt(AudioService.MUTE_AUDIO, 0) != 0;
            _toggle.onValueChanged.AddListener(ToggleMute);
        }

        private void ToggleMute(bool mute)
        {
            AudioService.Instance.ToggleMuteAudio();
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ToggleMute);
        }

    } 
}
