using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public enum MusicType {
        Level,
        Glitch,
        Title,
        EndScreen
    };

    public MusicType musicType;
    public GameObject soundManagerPrefab;
    public bool isUnderwater = false;

    private void Awake() {
        EnsureSoundManagerPresent();
        switch (musicType) {
            case MusicType.Level:
                SoundManager.instance.PlayRandomLevelMusic();
                break;
            case MusicType.Glitch:
                SoundManager.instance.PlayGlitchMusic();
                break;
            case MusicType.Title:
                SoundManager.instance.PlayTitleMusic();
                break;
            case MusicType.EndScreen:
                SoundManager.instance.PlayEndScreenMusic();
                break;
        }
        SoundManager.instance.SetUnderwater(isUnderwater);
    }

    private void EnsureSoundManagerPresent() {
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm == null) {
            Instantiate(soundManagerPrefab);
        }
    }
}
