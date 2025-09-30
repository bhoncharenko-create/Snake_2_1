using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DerailedTools.UI
{
    public class SettingsPanel : PanelUI<bool>
    {
        [Header("Settings panel")]
        [SerializeField] private Toggle vibrationToggle;
        [SerializeField] private Slider musicSlider, fxSlider;

        public override void ShowPanel(bool data)
        {
            base.ShowPanel(data);

            vibrationToggle.isOn = SettingsManager.IsVivrationActive;
            musicSlider.value = SettingsManager.MusicVolume;
            fxSlider.value = SettingsManager.FxVolume;
        }

        public void OnSliderValueChanged(string key)
        {
            if (key == "fx")
                SettingsManager.FxVolume = fxSlider.value;
            else
                SettingsManager.MusicVolume = musicSlider.value;
        }

        public void OnToggleValueChanged()
        {
            SettingsManager.IsVivrationActive = vibrationToggle.isOn;
        }
    }
}