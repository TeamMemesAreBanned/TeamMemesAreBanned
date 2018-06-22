using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public void LoadNextLevel() {
        GameManager.instance.LoadNextLevel();
    }

    public void RestartGame() {
        GameManager.instance.InitGame();
    }
}
