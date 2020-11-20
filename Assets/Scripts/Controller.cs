using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public Transform player;
    public Transform ball;

    float yRot = 0f;

    float lastT = 0f;
    public float twist;

    bool shot;

	// Use this for initialization
	void Start () {
        shot = false;
	}

    void FixedUpdate()
    {
        Vector3 RS_joy = new Vector3(Input.GetAxisRaw("RSX"), Input.GetAxisRaw("RSY"), 0f);
        float LT_joy = Input.GetAxisRaw("LT");
        float RT_joy = Input.GetAxisRaw("RT");
        bool A_joy = Input.GetButton("A");
        bool B_joy = Input.GetButton("B");
        bool X_joy = Input.GetButton("X");
        bool Y_joy = Input.GetButton("Y");

        if (A_joy)
        {
            ball.GetComponent<Rigidbody>().MovePosition(Vector3.up * 5f);
        }

        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        float moveSpeed = 0.15f;
        Vector3 go = transform.position + move * moveSpeed;
        go.x = Mathf.Clamp(go.x, -19f, 19f);
        go.z = Mathf.Clamp(go.z, -19f, 19f);
        body.MovePosition(go);
        //Quaternion toRot = Quaternion.FromToRotation(Vector3.forward, move.normalized);
        //if (Quaternion.Dot(transform.rotation, toRot) < 0f)
        //{
            //Debug.Log("yo");
        //}
        //player.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(player.rotation.eulerAngles.x, 0f, player.rotation.eulerAngles.z));
        float rotSpeed = 5f;
        //Quaternion toRot = Quaternion.Euler(0f, RS_joy.x * rotSpeed, 0f) * transform.rotation;
        //body.MoveRotation(toRot);
        
        Vector3 piv = new Vector3(Input.GetAxisRaw("RSY"), 0f, Input.GetAxisRaw("RSX"));
        Quaternion rot = Quaternion.identity;
        float idle = -5f;
        float xPiv = idle;
        if (RT_joy > 0.0001f)
        {
            float maxPiv = 10f;
            //xPiv += RT_joy * maxPiv;
            xPiv += Mathf.Pow(1f + RT_joy, 4) * maxPiv;

            float changeT = RT_joy - lastT;
            if (changeT > 0f && !shot)
            {
                rot = Quaternion.Euler(xPiv, 0f, 0f) * rot;

                //Vector3 forceVec = Vector3.right * twist * Mathf.Pow(1f + RT_joy, 8);
                //player.GetComponent<Rigidbody>().AddRelativeTorque(forceVec, ForceMode.Force);
                //player.GetComponent<Rigidbody>().AddForceAtPosition(transform.TransformVector(Vector3.back * twist * Mathf.Pow(1f + RT_joy, 8)),
                //transform.TransformPoint(Vector3.down), ForceMode.Force);
                //player.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * twist * changeT, ForceMode.Force);
                lastT = RT_joy;
                //Debug.Log(Mathf.Pow(1f + changeT, 16));
            }
            else
            {
                shot = true;
            }
        }
        else
        {
            lastT = 0f;
            shot = false;
        }
        //rot = Quaternion.Euler(xPiv, 0f, 0f) * rot;

        //Quaternion rot = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        //Quaternion rot = Quaternion.identity;
        if (Mathf.Abs(RS_joy.x) > 0.01f)
        {
            yRot += RS_joy.x * rotSpeed;
        }
        rot = Quaternion.Euler(0f, yRot, 0f) * rot;

        body.MoveRotation(rot);

        /*
        if (RT_joy > 0.01f && !A_joy)
        {
            //player.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(piv.x * 45f, 0f, piv.z * 45f));
            float maxPiv = 90f;
            body.MoveRotation(Quaternion.Euler(0f, RS_joy.x * rotSpeed, 0f) * transform.rotation * Quaternion.Euler(RT_joy * maxPiv, 0f, 0f));
        }
        else
        {
            body.MoveRotation(Quaternion.identity);
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
