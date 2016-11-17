using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float initY;
	// Use this for initialization
	void Start () {
		initY = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, initY, transform.position.z);
	}
}
