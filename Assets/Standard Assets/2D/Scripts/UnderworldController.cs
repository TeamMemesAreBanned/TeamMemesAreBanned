using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class UnderworldController : MonoBehaviour {

	public CameraFollowBetter cameraFollowBetter;
	public GameObject player;
	public float groundLevel = 0;

	public float normalYCameraOffset;
	public float reversedYCameraOffset;

	private NormalControl normalControl;
	private ReverseGravityControl reverseGravityControl;

	// Use this for initialization
	void Start () {
		normalControl = player.GetComponent<NormalControl>();
		reverseGravityControl = player.GetComponent<ReverseGravityControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (normalControl.transform.position.y > groundLevel) {
			normalControl.enabled = true;
			reverseGravityControl.enabled = false;
			Vector3 rotation = player.transform.eulerAngles;
			rotation.z = 0;
			player.transform.eulerAngles = rotation;
			cameraFollowBetter.targetOffset.y = normalYCameraOffset;
		} else {
			normalControl.enabled = false;
			reverseGravityControl.enabled = true;
			Vector3 rotation = player.transform.eulerAngles;
			rotation.z = 180;
			player.transform.eulerAngles = rotation;
			cameraFollowBetter.targetOffset.y = reversedYCameraOffset;
		}
	}
}
