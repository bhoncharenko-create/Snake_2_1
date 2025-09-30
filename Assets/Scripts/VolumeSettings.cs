using DerailedTools;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    private void Start()
    {
        musicSlider.value = SettingsManager.MusicVolume;
        sfxSlider.value = SettingsManager.FxVolume;
    }
    public void SetMusicVolume(bool fx )
    {
        if (fx)
            SettingsManager.FxVolume = sfxSlider.value;
        else
            SettingsManager.MusicVolume = musicSlider.value;
    }
}
