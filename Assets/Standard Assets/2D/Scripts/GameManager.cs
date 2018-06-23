using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private int level = -1;
    public int deaths = 0;
    public bool itemCollected = false;

    private readonly string[] levelNames = {
        "Stage1",
        "Stage2",
        "Stage3",
        "Stage4",
        "Stage5",
        "Stage6",
        "Stage7",
        "Stage8",
        "Stage9",
        "Stage10"
    };

    void Awake() {
        if (instance == null) {
			instance = this;
        } else if (instance != this) {
			Destroy(gameObject);         
        }

        DontDestroyOnLoad(gameObject);

        InitGame();	
	}
	
    public void InitGame() {
        level = -1;
        deaths = 0;
        itemCollected = false;
    }

    public void LoadNextLevel() {
        level++;
        itemCollected = false;

        if (level >= levelNames.Length) {
            SceneManager.LoadScene("End");
        } else {
            SceneManager.LoadScene(levelNames[level]);
        }
    }

    public void TrackDeath() {
        deaths++;
    }
}
