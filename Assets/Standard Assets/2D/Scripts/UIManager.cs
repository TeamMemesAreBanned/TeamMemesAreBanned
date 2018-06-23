using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text deathLabel = null;
    public Text levelLabel = null;
    public Text hintLabel = null;
    public Text collectionLabel = null;
    public Button resetButton = null;
    public Button exitButton = null;
    public GameObject settingsPanel = null;

	private void Awake() {
        if (deathLabel != null) {
            deathLabel.text = GameManager.instance.deaths.ToString();
        }

        GameManager.LevelInfo levelInfo = GameManager.instance.GetLevelInfo();

        if (levelLabel != null) {
            levelLabel.text = "LEVEL 1." + levelInfo.index.ToString();
        }

        if (hintLabel != null) {
            hintLabel.text = levelInfo.hint;
        }

        if (collectionLabel != null) {
            collectionLabel.text = "0/1";
        }

        if (resetButton != null) {
            resetButton.onClick.AddListener(RestartLevel);
        }

        if (exitButton != null) {
            exitButton.onClick.AddListener(ToggleSettings);
        }
	}

    public void CollectItem() {
        collectionLabel.text = "1/1";
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }

	public void LoadNextLevel() {
        GameManager.instance.LoadNextLevel();
    }

    public void RestartGame() {
        GameManager.instance.InitGame();
        SceneManager.LoadScene("Title");
    }

    public void ToggleSettings() {
        if (settingsPanel != null) {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

	public void Update()
	{
        if (Input.GetKeyUp(KeyCode.Escape)) {
            ToggleSettings();
        }

        if (Input.GetKeyUp(KeyCode.R)) {
            RestartLevel();
        }
	}
}
