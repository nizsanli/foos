using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public Transform player;
    public Transform ball;

    public float moveSpeed;
    public float pivotSpeed;

    float setAngle = 0f;

	// Use this for initialization
	void Start () {

	}

    void FixedUpdate()
    {
        if (Input.GetButtonDown("R"))
        {
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.velocity = Vector3.zero;
            ballRB.MovePosition(Vector3.up * 5f + Vector3.forward * 5f);
        }

        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 go = transform.position + move * moveSpeed * Time.fixedDeltaTime;
        body.MovePosition(go);

        if (Input.GetMouseButton(0))
        {
            setAngle += pivotSpeed;
        }
        else
        {
            setAngle = 0f;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 50f))
        {
            Vector3 vecToHit = hitInfo.point - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(vecToHit.x, -setAngle * 2f, vecToHit.z));
            body.MoveRotation(lookRot);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
