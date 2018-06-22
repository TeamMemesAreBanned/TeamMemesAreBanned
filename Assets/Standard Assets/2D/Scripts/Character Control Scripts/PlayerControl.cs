using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public void Die() {

		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		if (rigidbody2D != null) {
			rigidbody2D.velocity = Vector2.zero;
		}

		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("Death", true);
		}

		this.enabled = false;
	}
}
