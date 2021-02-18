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
    public Event initialEvent = Event.current;
    public Vector2 mousePosOg;

    private Camera cam;

    void Start()
    {
        camPos = this.transform.position;
        camPosOg = this.transform.position;
        scale = 1f;
        panRate = 10;

        cam = Camera.main;
        mousePosOg = new Vector2();
        mousePosOg.x = initialEvent.mousePosition.x;
        mousePosOg.y = cam.pixelHeight - initialEvent.mousePosition.y;
        camPosWorldOg = cam.ScreenToWorldPoint(new Vector3(mousePosOg.x, mousePosOg.y, cam.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        //Pan();
        ResetCam();
    }

    public void Zoom() {
        if((camPos.y -= Input.mouseScrollDelta.y * scale) < maxY &&
            ((camPos.y -= Input.mouseScrollDelta.y * scale) > minY)) {
            camPos.y -= Input.mouseScrollDelta.y * scale;
            camPos.z += Input.mouseScrollDelta.y * scale;
            this.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }
    }

    public void ResetCam() {
        if(Input.GetKeyDown(KeyCode.Z)) {
            camPos = camPosOg;
        }
    }

    // TEST CODE BEGIN
    void OnGUI() {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        /**
         * UN-COMMENT TO SHOW SCREEN / MOUSE / WORLD POSITIONS
            //GUILayout.BeginArea(new Rect(20, 20, 250, 120));
            //GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
            //GUILayout.Label("Mouse position: " + mousePos);
            //GUILayout.Label("World position: " + point.ToString("F3"));
            //GUILayout.EndArea();
        */
        
        // if mouse position within 10% of extents
        // PAN LEFT
        if(mousePos.x < (0 + cam.pixelWidth / 10) 
            && point.x > camPosWorldOg.x ) {
            camPos.x -= scale / panRate;
            this.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }
        // PAN RIGHT
        if(mousePos.x > (cam.pixelWidth - cam.pixelWidth / 10)
            && point.x < (100)) {
            camPos.x += scale / panRate;
            this.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }
        // PAN DOWN
        if (mousePos.y < (0 + cam.pixelHeight / 10)
            && point.z > -100) {
            camPos.z -= scale / panRate;
            this.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }
        // PAN UP
        if (mousePos.y > (cam.pixelHeight - cam.pixelWidth / 10)
            && point.z < -20) {
            camPos.z += scale / panRate;
            this.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }
    }
    
}
