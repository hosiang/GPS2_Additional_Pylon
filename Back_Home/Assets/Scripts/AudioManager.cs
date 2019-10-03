using UnityEngine;

using System;

public class AudioManager : MonoBehaviour {

    [SerializeField] private float bgmVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float masterVolume = 1f;

    [SerializeField] private Audio[] sfx;
    [SerializeField] private Audio[] bgm;

    private void Awake() {

        if (Global.audioManager == null) { Global.audioManager = this; }
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        for(int i = 0; i < sfx.Length; i++) {

            sfx[i].init(gameObject.AddComponent<AudioSource>(), masterVolume);

        }

        for (int i = 0; i < bgm.Length; i++) {

            bgm[i].init(gameObject.AddComponent<AudioSource>(), masterVolume);

        }

    }

    private void updateVolume() {

        for (int i = 0; i < sfx.Length; i++) {

            sfx[i].setVolume(sfxVolume * masterVolume);

        }

        for (int i = 0; i < bgm.Length; i++) {

            bgm[i].setVolume(bgmVolume * masterVolume);

        }

    }

    public void stopAllSFX() {

        for(int i = 0; i < sfx.Length; i++) {

            sfx[i].stop();

        }

    }

    public void stopAllBGM() {

        for (int i = 0; i < bgm.Length; i++) {

            bgm[i].stop();

        }

    }

    public Audio getSFX(string name) {

        int counter = 0;

        for ( ; counter < sfx.Length; counter++) {

            if (sfx[counter].getName() == name) {
                break;
            }

        }

        if (counter == sfx.Length) {
            Debug.LogWarning("sfx: " + name + " not found!");
            return null;
        }

        return sfx[counter];

    }

    public Audio getBGM(String name) {

        int counter = 0;

        for ( ; counter < bgm.Length; counter++) {

            if (bgm[counter].getName() == name) {
                break;
            }

        }

        if (counter == bgm.Length) {
            Debug.LogWarning("bgm: " + name + " not found!");
            return null;
        }

        return bgm[counter];

    }

    public void setVolumeBGM(float bgmVolume) {

        this.bgmVolume = bgmVolume;

        updateVolume();

    }

    public void setVolumeSFX(float sfxVolume) {

        this.sfxVolume = sfxVolume;

        updateVolume();

    }

    public void setVolumeMaster(float masterVolume) {

        this.masterVolume = masterVolume;

        updateVolume();

    }

}
