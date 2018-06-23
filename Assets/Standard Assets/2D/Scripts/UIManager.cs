using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text deathLabel = null;

	private void Awake()
	{
        if (deathLabel != null) {
            deathLabel.text = GameManager.instance.deaths.ToString();
        }
	}

	public void LoadNextLevel() {
        GameManager.instance.LoadNextLevel();
    }

    public void RestartGame() {
        GameManager.instance.InitGame();
        SceneManager.LoadScene("Title");
    }
}
