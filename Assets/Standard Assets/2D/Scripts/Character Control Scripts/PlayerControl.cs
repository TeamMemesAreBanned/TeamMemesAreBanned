using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	protected bool isDead = false;

	public void Die() {

        if (isDead) {
            return;
        }

		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		if (rigidbody2D != null) {
			rigidbody2D.velocity = Vector2.zero;
		}

		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("Death", true);
		}

        GameManager.instance.TrackDeath();

		isDead = true;
	}

	protected virtual void Update() {
		if (isDead && Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}

        if (Input.GetKeyUp(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
	}
}
