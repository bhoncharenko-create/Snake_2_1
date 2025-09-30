using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DerailedTools
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        private static SettingsManager instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static float FxVolume
        {
            get => PlayerPrefs.GetFloat("fxVolume", 0);
            set 
            {
                instance.audioMixer.SetFloat("fxVolume", FxVolume);
                PlayerPrefs.SetFloat("fxVolume", value);
            }
        }
        public static float MusicVolume
        {
            get => PlayerPrefs.GetFloat("musicVolume", 0);
            set
            {
                instance.audioMixer.SetFloat("musicVolume", MusicVolume);
                PlayerPrefs.SetFloat("musicVolume", value);
            }
        }
        public static bool IsVivrationActive
        {
            get => _isVivrationActive;
            set
            {
                _isVivrationActive = value;
                PlayerPrefs.SetInt("vibrationActive", value ? 1 : 0);
            }
        }
        private static bool _isVivrationActive = true;

        private void Start()
        {
            audioMixer.SetFloat("fxVolume", FxVolume);
            audioMixer.SetFloat("musicVolume", MusicVolume);
            IsVivrationActive = PlayerPrefs.GetInt("vibrationActive", 1) == 1 ? true : false;
        }
    }
}