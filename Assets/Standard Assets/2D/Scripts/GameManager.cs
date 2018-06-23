using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public struct LevelInfo {
        public string name;
        public string hint;
        public int index;

        public LevelInfo(string p1, string p2, int p3) {
            name = p1;
            hint = p2;
            index = p3;
        }
    }

    public static GameManager instance = null;
    public int level = -1;
    public int deaths = 0;
    public bool itemCollected = false;
    public DateTime timeStarted;
    public DateTime timeFinished;
    public bool isFinished = false;

    private readonly LevelInfo[] levelInfos = {
        new LevelInfo("Stage1", "A nice little trot", 0),
		new LevelInfo("Stage7", "tort elttil ecin A", 1),
        new LevelInfo("Stage2", "Head in the clouds", 2),
        new LevelInfo("Stage9", "Everyone's favourite level", 3),
		new LevelInfo("Stage5", "Leap of faith", 4),
		new LevelInfo("Stage8", "Is it hot in here or is it just you?", 5),
        new LevelInfo("Stage10", "snek", 6),
		new LevelInfo("Stage6", "Where'd you go?", 7),
		new LevelInfo("Stage4", "Made of rubber", 8),
		new LevelInfo("Stage3", "Flappy Gob", 9),
        new LevelInfo("Stage11", "TEST PROTO PLS DO NOT JUDGE", 10)
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

	private void Update() {
		if (Input.GetKeyDown(KeyCode.J)) {
			LoadNextLevel();
		}
	}

	public void InitGame() {
        level = -1;
        deaths = 0;
        itemCollected = false;
        timeStarted = DateTime.Now;
        isFinished = false;
    }

    public void LoadNextLevel() {
        level++;
        itemCollected = false;
        isFinished = false;

        if (level == 0) {
            timeStarted = DateTime.Now;
        }

        if (level >= levelInfos.Length) {
            timeFinished = DateTime.Now;
            isFinished = true;
            SceneManager.LoadScene("End");
        } else {
            SceneManager.LoadScene(levelInfos[level].name);
        }
    }

    public void TrackDeath() {
        deaths++;
    }

    public LevelInfo GetLevelInfo() {
        if (level < 0 || level >= levelInfos.Length) {
            return new LevelInfo("", "", 0);
        }

        return levelInfos[level];
    }

    public double GetTimePlayed() {
        if (isFinished) {
            return (timeFinished - timeStarted).TotalMilliseconds;
        }
        return (DateTime.Now - timeStarted).TotalMilliseconds;
    }
}
