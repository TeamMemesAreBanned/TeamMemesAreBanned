using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	protected bool isDead = false;

    public AudioClip death1;
    public AudioClip death2;

	public void Die() {

        if (isDead) {
            return;
        }

		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		if (rigidbody2D != null) {
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}

		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("Death", true);
		}

        GameManager.instance.TrackDeath();

		isDead = true;

        if (Random.Range(0, 100) < 10) {
            SoundManager.instance.PlaySingle(death2);
        } else {
            SoundManager.instance.RandomizeSfx(death1);
        }
	}

	protected virtual void Update() {
		if (isDead && Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}
