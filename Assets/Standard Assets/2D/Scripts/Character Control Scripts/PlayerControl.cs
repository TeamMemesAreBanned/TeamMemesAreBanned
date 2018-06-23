using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	private static Vector3 deathOffset = new Vector3(-0.54f, -0.13f, 0);

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
			rigidbody2D.isKinematic = true;
		}

		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("Death", true);
			//transform.localPosition = transform.localPosition + deathOffset;
		}

        GameManager.instance.TrackDeath();

		isDead = true;

        AudioClip death = (Random.Range(0, 100) < 10) ? death2 : death1;
        SoundManager.instance.PlaySingle(death);
	}

	protected virtual void Update() {
		if (isDead && Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}
