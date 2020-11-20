using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform player;
    Vector3 anchor;

	// Use this for initialization
	void Start () {
        anchor = Vector3.zero;
        Camera.main.transform.position = new Vector3(0f, 8f, -8f);
        Camera.main.transform.LookAt(anchor);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newAnchor = Vector3.Lerp(anchor, new Vector3(player.position.x, 0f, player.position.z), 0.05f);
        Vector3 change = (newAnchor - anchor);
        //Debug.Log(change);
        Camera.main.transform.Translate(change, Space.World);
        anchor = newAnchor;
	}
}
