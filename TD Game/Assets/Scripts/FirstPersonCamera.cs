using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform fpCam;
    public float moveSpeed;
    public float rotSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        fpCam = this.gameObject.transform;
        moveSpeed = 10.0f;
        rotSpeed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // forward
        if(Input.GetKey(KeyCode.W)) {
            fpCam.position += fpCam.forward * moveSpeed * Time.deltaTime;
        }
        // backward
        if (Input.GetKey(KeyCode.S)) {
            fpCam.position += -fpCam.forward * moveSpeed * Time.deltaTime;
        }
        // strafe left
        if (Input.GetKey(KeyCode.Q)) {
            fpCam.position += -fpCam.right * moveSpeed * Time.deltaTime;
        }
        // strafe right
        if (Input.GetKey(KeyCode.E)) {
            fpCam.position += fpCam.right * moveSpeed * Time.deltaTime;
        }
        // move up
        if (Input.GetKey(KeyCode.Z)) {
            fpCam.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        // move down
        if (Input.GetKey(KeyCode.X)) {
            fpCam.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        // pan left
        if (Input.GetKey(KeyCode.A)) {
            //fpCam.Rotate(fpCam.position, Vector3.up, -rotSpeed * Time.deltaTime);
            fpCam.Rotate(0.0f, -rotSpeed * Time.deltaTime, 0.0f, Space.Self);
        }
        // pan right
        if (Input.GetKey(KeyCode.D)) {
            fpCam.Rotate(0.0f, rotSpeed * Time.deltaTime, 0.0f, Space.Self);
        }
        // pan up
        if (Input.GetKey(KeyCode.C)) {
            fpCam.Rotate(rotSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        // pan down
        if (Input.GetKey(KeyCode.V)) {
            fpCam.Rotate(-rotSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }

    }
}
