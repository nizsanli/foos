using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

    public Transform engine;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    void FixedUpdate()
    {
        Vector3 LS_joy = new Vector3(Input.GetAxisRaw("LSX"), Input.GetAxisRaw("LSY"), 0f);
        Vector3 RS_joy = new Vector3(Input.GetAxisRaw("RSX"), Input.GetAxisRaw("RSY"), 0f);
        float LT_joy = Input.GetAxisRaw("LT");
        float RT_joy = Input.GetAxisRaw("RT");
        bool A_joy = Input.GetButton("A");
        bool B_joy = Input.GetButton("B");
        bool X_joy = Input.GetButton("X");
        bool Y_joy = Input.GetButton("Y");

        //DebugInput();

        float engineMaxPiv = 25f;
        Quaternion engineRot = Quaternion.Euler(-LS_joy.y * engineMaxPiv, 0f, -LS_joy.x * engineMaxPiv);
        engine.rotation = engineRot;

        LineRenderer engineLine = engine.GetComponent<LineRenderer>();
        engineLine.SetPosition(0, engine.position);
        engineLine.SetPosition(1, engine.position - engine.up * 10000f);

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        float thrust = 750f;
        rigidBody.AddForceAtPosition(engine.TransformVector(Vector3.up * RT_joy * Time.deltaTime * thrust), 
            transform.TransformPoint(Vector3.down), ForceMode.Force);

        if (A_joy)
        {
            rigidBody.MovePosition(Vector3.zero + Vector3.up * 2f);
            rigidBody.MoveRotation(Quaternion.identity);
        }
    }

    void DebugInput()
    {
        Vector3 LS_joy = new Vector3(Input.GetAxisRaw("LSX"), Input.GetAxisRaw("LSY"), 0f);
        Vector3 RS_joy = new Vector3(Input.GetAxisRaw("RSX"), Input.GetAxisRaw("RSY"), 0f);
        float LT_joy = Input.GetAxisRaw("LT");
        float RT_joy = Input.GetAxisRaw("RT");
        bool A_joy = Input.GetButton("A");
        bool B_joy = Input.GetButton("B");
        bool X_joy = Input.GetButton("X");
        bool Y_joy = Input.GetButton("Y");

        if (LS_joy.magnitude > 0f) { Debug.Log(LS_joy); }
        if (RS_joy.magnitude > 0f) { Debug.Log(RS_joy); }
        if (LT_joy > 0f) { Debug.Log(LT_joy); }
        if (RT_joy > 0f) { Debug.Log(RT_joy); }
        if (A_joy) { Debug.Log("A"); }
        if (B_joy) { Debug.Log("B"); }
        if (X_joy) { Debug.Log("X"); }
        if (Y_joy) { Debug.Log("Y"); }
    }
}
