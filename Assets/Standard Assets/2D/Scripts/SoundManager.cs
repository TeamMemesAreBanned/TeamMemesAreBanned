﻿using System.Collections;
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
    public AudioClip endScreenMusic;

    private enum MusicType {
        Title,
        Level,
        Glitch,
        EndScreen
    };

    private MusicType currentlyPlayingMusicType;

    private AudioLowPassFilter lowPassFilter;
    private AudioReverbFilter reverbFilter;

    // Use this for initialization
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        reverbFilter = GetComponent<AudioReverbFilter>();
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
        if (currentlyPlayingMusicType == MusicType.Title && musicSource.isPlaying) {
            return;
        }
        musicSource.clip = titleMusic;
        musicSource.Play();
        musicSource.loop = true;
        currentlyPlayingMusicType = MusicType.Title;
    }

    public void PlayRandomLevelMusic() {
        if (currentlyPlayingMusicType == MusicType.Level && musicSource.isPlaying) {
            return;
        }
        int idx = Random.Range(0, levelMusic.Length);
        musicSource.clip = levelMusic[idx];
        musicSource.Play();
        musicSource.loop = false;
        currentlyPlayingMusicType = MusicType.Level;
    }

    public void PlayGlitchMusic() {
        if (currentlyPlayingMusicType == MusicType.Glitch && musicSource.isPlaying) {
            return;
        }
        musicSource.clip = glitchMusic;
        musicSource.Play();
        musicSource.loop = true;
        currentlyPlayingMusicType = MusicType.Glitch;
    }

    public void PlayEndScreenMusic() {
        if (currentlyPlayingMusicType == MusicType.EndScreen && musicSource.isPlaying) {
            return;
        }
        musicSource.clip = endScreenMusic;
        musicSource.Play();
        musicSource.loop = true;
        currentlyPlayingMusicType = MusicType.EndScreen;
    }

    public void SetUnderwater(bool shouldBeUnderwater) {
        lowPassFilter.enabled = shouldBeUnderwater;
        reverbFilter.enabled = shouldBeUnderwater;
    }

    private void Update() {
        if (! musicSource.isPlaying && currentlyPlayingMusicType == MusicType.Level) {
            PlayRandomLevelMusic();
        }
    }
}
