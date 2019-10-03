using UnityEngine;

[System.Serializable]
public class Audio {

    private AudioSource controller;

    [SerializeField] private AudioClip clip;

    [SerializeField] private string name;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;

    public bool loop;
    public bool mute;

    public void init(AudioSource controller, float masterVolume) {

        this.controller = controller;

        this.controller.clip = clip;

        this.controller.volume = volume * masterVolume;
        this.controller.pitch = pitch;

        this.controller.loop = loop;

    }

    public void play() {

        if (!controller.isPlaying) {
            controller.Play();
        }

    }

    public void play_IgnoreCurrent() {

        controller.Play();

    }

    public void pause() {

        if (controller.time != 0) {
            controller.Pause();
        }

    }

    public void unpause() {

        if (!controller.isPlaying && controller.time != 0) {
            controller.UnPause();
        }

    }

    public void stop() {

        if (controller.isPlaying || controller.time != 0) {
            controller.Stop();
        }

    }

    public void setVolume(float masterVolume) {
        controller.volume = volume * masterVolume;
    }

    public string getName() { return name; }

    public bool getIsPlaying() { return controller.isPlaying; }

}
