using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource fxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public AudioClip titleMusic;
    public AudioClip[] levelMusic;
    public AudioClip glitchMusic;

    private bool isPlayingLevelMusic = false;

    // Use this for initialization
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip) {
        fxSource.clip = clip;
        fxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips) {
        int idx = Random.Range(0, clips.Length);
        float pitch = Random.Range(lowPitchRange, highPitchRange);

        fxSource.pitch = pitch;
        fxSource.clip = clips[idx];
        fxSource.Play();
    }

    public void PlayTitleMusic() {
        musicSource.clip = titleMusic;
        musicSource.Play();
        musicSource.loop = true;
        isPlayingLevelMusic = false;
    }

    public void PlayRandomLevelMusic() {
        if (isPlayingLevelMusic && musicSource.isPlaying) {
            return;
        }
        int idx = Random.Range(0, levelMusic.Length);
        musicSource.clip = levelMusic[idx];
        musicSource.Play();
        musicSource.loop = false;
        isPlayingLevelMusic = true;
    }

    public void PlayGlitchMusic() {
        musicSource.clip = glitchMusic;
        musicSource.Play();
        musicSource.loop = true;
        isPlayingLevelMusic = false;
    }

    private void Update() {
        if (! musicSource.isPlaying && isPlayingLevelMusic) {
            PlayRandomLevelMusic();
        }
    }
}
