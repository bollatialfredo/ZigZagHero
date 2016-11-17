using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    
	public float speed;
	public float jumpVelocity;
	public float turnSpeed;

	private Rigidbody rb;
	private bool onGround = true;
    private float playerRotation = 0;
	private Vector3 posToAlign;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		Quaternion target = Quaternion.Euler (0, playerRotation, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * turnSpeed);
	}

	float i = 0;

	IEnumerator Align()
	{
		while(i < 1)
		{
			i = i + Time.deltaTime * 2;
			transform.position = Vector3.Lerp(transform.position, posToAlign, i);
			posToAlign = new Vector3 (posToAlign.x, transform.position.y, transform.position.z + Time.deltaTime * speed);
			yield return null;
		}
	}

	public void ResetPlayer()
	{
		transform.position = new Vector3 (0, 0, 0);
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		playerRotation = 0;
	}

    public void Jump()
    {
        if (onGround)
        {
            onGround = false;
			rb.velocity = new Vector3(0, jumpVelocity, 0);
        }
    }

    public void TurnRight()
    {
		playerRotation += 90;
	}

    public void TurnLeft()
    {
		playerRotation -= 90;
    }

	public void SetOnGorund(bool value)
	{
		onGround = value;
	}

	public void SetAlignPosition(GameObject level)	
	{
		if (level != null) 
		{
			foreach (Transform t in level.transform) 
			{
				if (t.name == "StartMaze(Clone)") {
					posToAlign = new Vector3 (t.transform.position.x, transform.position.y, transform.position.z);
					i = 0;
					StartCoroutine(Align ());
					break;
				}
			}
		}
	}
}
