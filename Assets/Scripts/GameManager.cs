using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private int level = -1;
    public int deaths = 0;

    private readonly string[] levelNames = {
        "Level1",
        "MainLevel"
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
        SceneManager.LoadScene("Title");
    }

    public void LoadNextLevel() {
        level++;

        if (level >= levelNames.Length) {
            SceneManager.LoadScene("End");
        } else {
            SceneManager.LoadScene(levelNames[level]);
        }
    }

    public void trackDeath() {
        deaths++;
    }
}
