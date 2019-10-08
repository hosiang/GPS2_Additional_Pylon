using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;

    public void updateMasterVolume() {
        Global.audioManager.setVolumeMaster(masterVolumeSlider.value);
    }

    public void updateVolumeBGM() {
        Global.audioManager.setVolumeBGM(BGMVolumeSlider.value);
    }

    public void updateVolumeSFX() {
        Global.audioManager.setVolumeSFX(SFXVolumeSlider.value);
    }

}
