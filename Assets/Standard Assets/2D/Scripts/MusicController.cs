using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public enum MusicType {
        Level,
        Glitch,
        Title
    };

    public MusicType musicType;

    private void Awake() {
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
        }
    }
}
