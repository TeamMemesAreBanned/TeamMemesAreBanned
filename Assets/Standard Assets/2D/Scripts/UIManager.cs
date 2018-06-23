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

        if (settingsPanel != null) {
            Button continueButton = settingsPanel.transform.Find("btn_continue").gameObject.GetComponent<Button>();
            continueButton.onClick.AddListener(ToggleSettings);

            Button quitButton = settingsPanel.transform.Find("btn_exitGame").gameObject.GetComponent<Button>();
            quitButton.onClick.AddListener(RestartGame);

            Text popupKeysLabel = settingsPanel.transform.Find("txt_collectKeys").gameObject.GetComponent<Text>();
            popupKeysLabel.text = "0";

            Text popupKilledLabel = settingsPanel.transform.Find("txt_killed").gameObject.GetComponent<Text>();
            popupKilledLabel.text = GameManager.instance.deaths.ToString();

            Text popupTimerLabel = settingsPanel.transform.Find("txt_timer").gameObject.GetComponent<Text>();
            popupTimerLabel.text = "23h";

            Text popupLevelLabel = settingsPanel.transform.Find("txt_levelHeader").gameObject.GetComponent<Text>();
            popupLevelLabel.text = "LEVEL 1." + levelInfo.index.ToString();

            Text popupHintLabel = settingsPanel.transform.Find("txt_environmentHeader").gameObject.GetComponent<Text>();
            popupHintLabel.text = levelInfo.hint;
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

        if (settingsPanel != null && settingsPanel.activeSelf) {
            float currentMillis = (float)GameManager.instance.GetTimePlayed();
            float minutes = Mathf.Floor(currentMillis / 60000);
            float seconds = Mathf.Floor((currentMillis / 1000) % 60);
            float millis = Mathf.Floor(currentMillis % 1000);

            string timeSpent = "";

            if (minutes < 10) {
                timeSpent += "0";
            }
            timeSpent += minutes.ToString() + ":";

            if (seconds < 10)
            {
                timeSpent += "0";
            }
            timeSpent += seconds.ToString() + ".";

            if (millis < 10)
            {
                timeSpent += "0";
            }
            if (millis < 100) {
                timeSpent += "0";
            }
            timeSpent += millis.ToString();

            Text popupTimerLabel = settingsPanel.transform.Find("txt_timer").gameObject.GetComponent<Text>();
            popupTimerLabel.text = timeSpent;
        }
	}
}
