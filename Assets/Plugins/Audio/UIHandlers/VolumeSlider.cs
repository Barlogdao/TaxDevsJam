using UnityEngine;
using UnityEngine.UI;

namespace RB.Services.Audio
{
    [RequireComponent(typeof(Slider))]  
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioChannelType _channelType;
        private Slider _slider;
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _slider.value = PlayerPrefs.GetFloat(_channelType.ToString(), 1f);
            _slider.onValueChanged.AddListener(ChangeSliderVolume);
        }

        private void ChangeSliderVolume(float volume)
        {
            AudioService.Instance.ChangeVolume(volume, _channelType);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeSliderVolume);
        }


    } 
}
