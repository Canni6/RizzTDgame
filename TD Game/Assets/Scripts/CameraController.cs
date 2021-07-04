using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 camPos;
    public Vector3 camPosOg;
    public Vector3 camPosWorldOg;
    public float scale;
    public float panRate;
    public const float maxY = 70f;
    public const float minY = 20f;
    public Vector2 mousePosOg;
    public int pixelWidth;
    public int pixelHeight;

    private Camera cam;
    public GameManager gameManager; // set in inspector

    void Start()
    {
        camPos = this.transform.position;
        camPosOg = this.transform.position;
        scale = 1f;
        panRate = 10;
        cam = Camera.main;
        pixelWidth = cam.pixelWidth;
        pixelHeight = cam.pixelHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        ResetCam();
    }

    public void Zoom() {
        if((camPos.y -= Input.mouseScrollDelta.y * scale) < maxY &&
            ((camPos.y -= Input.mouseScrollDelta.y * scale) > minY)) {
            camPos.y -= Input.mouseScrollDelta.y * scale;
            camPos.z += Input.mouseScrollDelta.y * scale;
            this.transform.position = camPos;
            print("Camera Zoomed");
        }
    }

    public void ResetCam() {
        if(Input.GetKeyDown(KeyCode.Z)) {
            camPos = camPosOg;
        }
    }

    void OnGUI() {
        /* https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html */
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 200, 250, 120));
        GUILayout.Label("Screen pixels: " + pixelWidth + ":" + pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
        /* end code sample */

        if (gameManager.getPaused() == false) {
            // if mouse position within 10% of window extents
            // PAN LEFT

            if (mousePos.x < (0 + pixelWidth / 20) && point.x > 0) {
                camPos.x -= scale / panRate;
                this.transform.position = camPos;
                print("pan left check: " + (0 + pixelWidth / 20));
                print("Camera panned left");
                //print("x: " + camPos.x + " y: " + camPos.y + " z: " + camPos.z);
            }
            // PAN RIGHT
            if (mousePos.x > (pixelWidth - pixelWidth / 20) && point.x < 80) {
                camPos.x += scale / panRate;
                this.transform.position = camPos;

                print("Camera panned right");
                //print("x: " + camPos.x + " y: " + camPos.y + " z: " + camPos.z);
            }
            // PAN DOWN (y screen axis / z world axis)
            if (mousePos.y < (0 + pixelHeight / 20) && point.z > -80) {
                camPos.z -= scale / panRate;
                this.transform.position = camPos;
                print("Camera panned down");
                //print("x: " + camPos.x + " y: " + camPos.y + " z: " + camPos.z);
            }
            // PAN UP (y screen axis / z world axis)
            if (mousePos.y > (pixelHeight - pixelHeight / 20) && point.z < -20) {
                camPos.z += scale / panRate;
                this.transform.position = camPos;
                print("pan up check: " + (pixelHeight - pixelHeight / 20));
                print("Camera panned up");
                //print("x: " + camPos.x + " y: " + camPos.y + " z: " + camPos.z);
            }
        }
    }
    
}
